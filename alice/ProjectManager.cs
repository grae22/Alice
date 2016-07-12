using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Timers;

namespace alice
{
  class ProjectManager
  {
    static public string c_projectFolderPath;
    static public string c_projectExt = "prj";

    private const string c_settingsFilename = "settings.xml";         // readonly (since it's under P4 now)
    private const string c_localSettingsFilename = "localSettings.xml";    // after first run we use this one which we can write to

    private List< Project > m_projects = new List< Project >();
    private Dictionary< string, string > m_commonValues = new Dictionary< string, string >();
    private List< string > m_hardcodedCommonValues = new List< string >();

    private Timer m_scheduledTasksTimer = new Timer( 5000.0 );

    //-------------------------------------------------------------------------

    public ProjectManager( string projectFolderFilename )
    {
      LoadProjectFolderFromFile( projectFolderFilename );
      LoadProjects();
      LoadSettings();

      m_scheduledTasksTimer.Elapsed += OnScheduledTaskTimerElapsed;
      m_scheduledTasksTimer.Start();
    }

    //-------------------------------------------------------------------------

    ~ProjectManager()
    {

    }

    //-------------------------------------------------------------------------

    private void LoadProjectFolderFromFile( string projectFolderFilename )
    {
      // Default to app path.
      c_projectFolderPath = Program.g_path + @"\prj";

      // Try get the path from file.
      if( projectFolderFilename.Length > 0 &&
          File.Exists( projectFolderFilename ) )
      {
        XmlDocument doc = new XmlDocument();
        doc.Load( projectFolderFilename );

        XmlElement aliceElement = doc.SelectSingleNode( "//Alice" ) as XmlElement;
        if( aliceElement != null &&
            aliceElement.HasAttribute( "absProjectFolderPath" ) )
        {
          c_projectFolderPath = aliceElement.Attributes[ "absProjectFolderPath" ].Value;
        }
      }
    }

    //-------------------------------------------------------------------------

    private void LoadProjects()
    {
      // create project folder if necessary
      string fullProjectPath = c_projectFolderPath;

      if( Directory.Exists( fullProjectPath ) == false )
      {
        Directory.CreateDirectory( fullProjectPath );
      }

      // find projects
      string[] foundFilenames =
        Directory.GetFiles(
          fullProjectPath,
          "*." + c_projectExt,
          SearchOption.TopDirectoryOnly );

      foreach( string fullFilename in foundFilenames )
      {
        if( File.Exists( fullFilename ) )
        {
          string filename = Path.GetFileName( fullFilename );

          Project prj = new Project( filename );

          prj.LoadFromFile();

          m_projects.Add( prj );
        }
      }
    }

    //-------------------------------------------------------------------------

    private void LoadSettings()
    {
      LoadCommonValues();
    }

    //-------------------------------------------------------------------------

    private void LoadCommonValues()
    {
      // some hardcoded common vals
      m_hardcodedCommonValues.Add( "ALICE_APP_PATH" );

      m_commonValues.Add( "ALICE_APP_PATH", Program.g_path );
      m_commonValues.Add( "ALICE_SERVER", System.Environment.MachineName );

      // is there a local settings file?
      string fullSettingsFilename;

      if( File.Exists( Program.g_path + "\\" + c_localSettingsFilename ) )
      {
        fullSettingsFilename = Program.g_path + "\\" + c_localSettingsFilename;
      }
      else
      {
        fullSettingsFilename = Program.g_path + "\\" + c_settingsFilename;
      }

      // load from settings file
      if( File.Exists( fullSettingsFilename ) )
      {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load( fullSettingsFilename );

        XmlNodeList commonValuesNode =
          xmlDoc.GetElementsByTagName( "CommonValues" );

        if( commonValuesNode.Count > 0 )
        {
          XmlElement commonValuesElement = ( commonValuesNode[ 0 ] as XmlElement );

          XmlNodeList commonValueNodes = commonValuesElement.GetElementsByTagName( "Value" );

          foreach( XmlNode node in commonValueNodes )
          {
            XmlElement commonValueElement = ( node as XmlElement );

            if( commonValueElement.HasAttribute( "name" ) &&
                commonValueElement.HasAttribute( "value" ) )
            {
              if( m_commonValues.ContainsKey( commonValueElement.Attributes[ "name" ].Value ) == false )
              {
                m_commonValues.Add( commonValueElement.Attributes[ "name" ].Value,
                                    commonValueElement.Attributes[ "value" ].Value );
              }
            }
          }
        }
      }
    }

    //-------------------------------------------------------------------------

    public void SaveSettings()
    {
      string fullSettingsFilename = Program.g_path + "\\" + c_localSettingsFilename;

      XmlDocument xmlDoc = new XmlDocument();
      XmlElement settingsElement = xmlDoc.CreateElement( "Settings" );
      xmlDoc.AppendChild( settingsElement );

      SaveCommonValues( xmlDoc, settingsElement );

      xmlDoc.Save( fullSettingsFilename );
    }

    //-------------------------------------------------------------------------

    private void SaveCommonValues( XmlDocument xmlDoc, XmlElement rootElement )
    {
      XmlElement commonValuesElement = xmlDoc.CreateElement( "CommonValues" );
      rootElement.AppendChild( commonValuesElement );

      foreach( string key in m_commonValues.Keys )
      {
        // don't write hardcoded vals to file
        if( m_hardcodedCommonValues.Contains( key ) )
        {
          continue;
        }
          
        string value;
        if( m_commonValues.TryGetValue( key, out value ) )
        {
          XmlElement commonValueElement = xmlDoc.CreateElement( "Value" );

          XmlAttribute newAttrib = xmlDoc.CreateAttribute( "name" );
          newAttrib.Value = key;
          commonValueElement.Attributes.Append( newAttrib );

          newAttrib = xmlDoc.CreateAttribute( "value" );
          newAttrib.Value = value;
          commonValueElement.Attributes.Append( newAttrib );

          commonValuesElement.AppendChild( commonValueElement );
        }
      }
    }

    //-------------------------------------------------------------------------

    public Project CreateProject( string name )
    {
      // name already used?
      foreach( Project prj in m_projects )
      {
        if( prj.Name.Equals( name, StringComparison.OrdinalIgnoreCase ) )
        {
          throw new Exception( "Project name '" + name + "' already exists." );
        }
      }

      // TODO: Test if filename is legal.

      // create the project
      Project newProject = new Project( name + "." + c_projectExt );
      newProject.Name = name;
      newProject.WriteToFile();

      m_projects.Add( newProject );

      return newProject;
    }

    //-------------------------------------------------------------------------

    public void CopyProject( Project projectToCopy, string newProjectName )
    {
      m_projects.Add( projectToCopy.Copy( newProjectName ) );
    }

    //-------------------------------------------------------------------------

    public void DeleteProject( Project prj )
    {
      prj.DeleteFile();

      m_projects.Remove( prj );
    }

    //-------------------------------------------------------------------------

    public void DoScheduledTasksLogic()
    {
      foreach( Project prj in m_projects )
      {
        foreach( TemplateShortcutEntry entry in prj.Template.FileShortcuts )
        {
          if( entry.ScheduledRunEnabled )
          {
            // Run the task if the sceduled time has recently passed and the
            // prev run time was more than 12 hours ago.
            TimeSpan diff = entry.NextScheduledRunTime - DateTime.Now;
            
            if( diff.Seconds < 0 &&       // time is in the past
                diff.Seconds > -30 &&     // was within 30 secs
                diff.Minutes == 0 &&
                diff.Hours == 0 )
            {
              entry.NextScheduledRunTime = entry.NextScheduledRunTime.AddDays( 1.0 );
              prj.WriteToFile();

              entry.PerformAction( prj, DateTime.Now.GetHashCode() );
            }
            
            // Task is overdue by more than 1 hour? Set the next run time.
            if( diff.Days <= 0 &&
                diff.Hours <= -1 )
            {
              entry.NextScheduledRunTime = entry.NextScheduledRunTime.AddDays( 1.0 );
              prj.WriteToFile();
            }
          }
        }
      }
    }

    //-------------------------------------------------------------------------

    private void OnScheduledTaskTimerElapsed( Object source, ElapsedEventArgs e )
    {
      DoScheduledTasksLogic();
    }

    //-------------------------------------------------------------------------

    public List< Project > Projects
    {
      get
      {
        return m_projects;
      }
    }

    //-------------------------------------------------------------------------

    public List< Project > GetFilteredProjects( string filter,
                                                bool excludeArchivedProjects )
    {
      string filterLower = filter.ToLower();

      List< Project > list = new List< Project >();

      foreach( Project prj in m_projects )
      {
        string name = prj.Name.ToLower();

        if( filterLower == "" ||
            name.Contains( filterLower ) )
        {
          if( excludeArchivedProjects &&
              prj.IsArchived )
          {
            continue;
          }

          list.Add( prj );
        }
      }

      return list;
    }

    //-------------------------------------------------------------------------

    public Dictionary< string, string > CommonValues
    {
      get
      {
        return m_commonValues;
      }

      set
      {
        m_commonValues = value;

        SaveSettings();
      }
    }

    //-------------------------------------------------------------------------

    public List< string > ReadOnlyCommonValues
    {
      get
      {
        return m_hardcodedCommonValues;
      }
    }

    //-------------------------------------------------------------------------
  }
}