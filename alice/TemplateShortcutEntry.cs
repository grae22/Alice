using System;
using System.Xml;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace alice
{
  public class TemplateShortcutEntry : TemplateEntry
  {
    public const string c_typeName = "shortcut";

    private string m_filename = "";
    private Dictionary< string, string > m_args = new Dictionary< string, string >();
    private Dictionary< string, bool > m_argStates = new Dictionary< string, bool >();
    private Dictionary< string, string > m_environmentVars = new Dictionary< string, string >();
    private Dictionary< string, bool > m_environmentVarStates = new Dictionary< string, bool >();
    private Project m_project;
    private string m_uiGroupName = "";
    private List< string > m_linkedShortcutDescriptions = new List< string >();
    private bool m_confirmBeforeRunning = false;
    private bool m_scheduledRunEnabled = false;
    private DateTime m_nextScheduledRunTime = new DateTime();

    //-------------------------------------------------------------------------

    public TemplateShortcutEntry()
    {
      m_type = EntryType.Type_Shortcut;
    }

    //-------------------------------------------------------------------------

    public TemplateShortcutEntry( XmlElement element )
    :
    base( element )
    {
      m_type = EntryType.Type_Shortcut;
      m_filename = element.Attributes[ "filename" ].Value;

      // ui group
      if( element.HasAttribute( "uiGroup" ) )
      {
        m_uiGroupName = element.Attributes[ "uiGroup" ].Value;
      }

      // confirm before running
      if( element.HasAttribute( "confirmBeforeRunning" ) )
      {
        m_confirmBeforeRunning = Boolean.Parse( element.Attributes[ "confirmBeforeRunning" ].Value );
      }

      // scheduled run
      if( element.HasAttribute( "scheduledRunEnabled" ) )
      {
        m_scheduledRunEnabled = Boolean.Parse( element.Attributes[ "scheduledRunEnabled" ].Value );
      }

      if( element.HasAttribute( "nextScheduledRunTime" ) )
      {
        try
        {
          m_nextScheduledRunTime = DateTime.Parse( element.Attributes[ "nextScheduledRunTime" ].Value );
        }
        catch
        {
          m_scheduledRunEnabled = false;
        }
      }

      // args
      XmlNodeList argCollection = element.GetElementsByTagName( "ArgumentCollection" );

      if( argCollection.Count > 0 )
      {
        XmlElement argCollectionElement = ( argCollection[ 0 ] as XmlElement );

        XmlNodeList argElements = argCollectionElement.GetElementsByTagName( "Arg" );

        foreach( XmlNode n in argElements )
        {
          XmlElement arg = ( n as XmlElement );

          if( arg.HasAttribute( "name" ) &&
              arg.HasAttribute( "value" ) &&
              arg.HasAttribute( "enabled" ) )
          {
            m_args.Add( arg.Attributes[ "name" ].Value, arg.Attributes[ "value" ].Value );
            m_argStates.Add( arg.Attributes[ "name" ].Value, Boolean.Parse( arg.Attributes[ "enabled" ].Value ) );
          }
        }
      }

      // env vars
      XmlNodeList varCollection = element.GetElementsByTagName( "EnvironmentVarCollection" );

      if( varCollection.Count > 0 )
      {
        XmlElement varCollectionElement = ( varCollection[ 0 ] as XmlElement );

        XmlNodeList varElements = varCollectionElement.GetElementsByTagName( "Var" );

        foreach( XmlNode n in varElements )
        {
          XmlElement var = ( n as XmlElement );

          if( var.HasAttribute( "name" ) &&
              var.HasAttribute( "value" ) &&
              var.HasAttribute( "enabled" ) )
          {
            m_environmentVars.Add( var.Attributes[ "name" ].Value, var.Attributes[ "value" ].Value );
            m_environmentVarStates.Add( var.Attributes[ "name" ].Value, Boolean.Parse( var.Attributes[ "enabled" ].Value ) );
          }
        }
      }

      // linked shortcuts
      XmlElement linkedCollectionElement = element.SelectSingleNode( "LinkedShortcuts" ) as XmlElement;

      if( linkedCollectionElement != null )
      {
        XmlNodeList shortcutNodes = linkedCollectionElement.SelectNodes( "Shortcut" );

        foreach( XmlElement shortcutElement in shortcutNodes )
        {
          if( shortcutElement.HasAttribute( "description" ) )
          {
            m_linkedShortcutDescriptions.Add( shortcutElement.Attributes[ "description" ].Value );
          }
        }
      }
    }

    //-------------------------------------------------------------------------

    public override bool PerformAction( Project project, int actionHashCode )
    {
      //-- Base.
      if( base.PerformAction( project, actionHashCode ) == false )
      {
        return true;
      }

      //-- Confirm?
      if( m_confirmBeforeRunning )
      {
        if( MessageBox.Show( m_project.GetCommonValue( "Run shortcut '" + Description + "'?", false ),
                             "Are you sure?",
                             MessageBoxButtons.OKCancel,
                             MessageBoxIcon.Question,
                             MessageBoxDefaultButton.Button2 ) == DialogResult.Cancel )
        {
          return false;
        }
      }

      //-- Run the process.
      if( m_filename != "" )
      {
        bool isExecutable = ( m_filename.Contains( ".exe" ) ||
                              m_filename.Contains( ".bat" ) );

        ProcessStartInfo info = new ProcessStartInfo();
        info.FileName = project.GetCommonValue( m_filename, false );
        info.Arguments = GetArgumentString( m_args, m_argStates, project );
        info.UseShellExecute = !isExecutable;
        info.WorkingDirectory = Path.GetDirectoryName( info.FileName );

        if( isExecutable )
        {
          info.EnvironmentVariables.Add( "ALICE_RESOURCE_PATH", Program.g_path + "\\res\\" );
        }

        foreach( string key in m_environmentVars.Keys )
        {
          if( EnvironmentVarStates[ key ] == false )
          {
            continue;
          }

          string value;
          if( m_environmentVars.TryGetValue( key, out value ) )
          {
            value = project.GetCommonValue( value, true );

            if( info.EnvironmentVariables.ContainsKey( key ) )
            {
              info.EnvironmentVariables[ key ] = value;
            }
            else
            {
              info.EnvironmentVariables.Add( key, value );
            }
          }
        }

        Process.Start( info );
      }

      //-- Run any linked shortcuts.
      foreach( string description in m_linkedShortcutDescriptions )
      {
        TemplateEntry entry = project.Template.GetEntryWithDescription( description );

        if( entry != null )
        {
          entry.PerformAction( project, actionHashCode );
        }
      }

      return true;
    }

    //-----------------------------------------------------------------------

    public override void AddXmlAttributes( XmlDocument xmlDoc, XmlElement element )
    {
      base.AddXmlAttributes( xmlDoc, element );

      // filename
      XmlAttribute newAttrib;
      newAttrib = xmlDoc.CreateAttribute( "filename" );
      newAttrib.Value = m_filename;
      element.Attributes.Append( newAttrib );

      // ui group
      newAttrib = xmlDoc.CreateAttribute( "uiGroup" );
      newAttrib.Value = m_uiGroupName;
      element.Attributes.Append( newAttrib );

      // confirm before running
      newAttrib = xmlDoc.CreateAttribute( "confirmBeforeRunning" );
      newAttrib.Value = m_confirmBeforeRunning.ToString();
      element.Attributes.Append( newAttrib );

      // scheduled run
      newAttrib = xmlDoc.CreateAttribute( "scheduledRunEnabled" );
      newAttrib.Value = m_scheduledRunEnabled.ToString();
      element.Attributes.Append( newAttrib );

      newAttrib = xmlDoc.CreateAttribute( "nextScheduledRunTime" );
      newAttrib.Value = m_nextScheduledRunTime.ToString( "yyyy-MM-ddTHH:mm" );
      element.Attributes.Append( newAttrib );

      // args
      XmlElement argCollectionElement = xmlDoc.CreateElement( "ArgumentCollection" );
      element.AppendChild( argCollectionElement );

      foreach( string key in m_args.Keys )
      {
        XmlElement argElement = xmlDoc.CreateElement( "Arg" );
        argCollectionElement.AppendChild( argElement );

        newAttrib = xmlDoc.CreateAttribute( "name" );
        newAttrib.Value = key;
        argElement.Attributes.Append( newAttrib );

        string value;
        bool enabled;
        if( m_args.TryGetValue( key, out value ) &&
            m_argStates.TryGetValue( key, out enabled ) )
        {
          newAttrib = xmlDoc.CreateAttribute( "value" );
          newAttrib.Value = value;
          argElement.Attributes.Append( newAttrib );

          newAttrib = xmlDoc.CreateAttribute( "enabled" );
          newAttrib.Value = enabled.ToString();
          argElement.Attributes.Append( newAttrib );
        }
        else
        {
          argCollectionElement.RemoveChild( argElement );
        }
      }

      // environment vars
      XmlElement varCollectionElement = xmlDoc.CreateElement( "EnvironmentVarCollection" );
      element.AppendChild( varCollectionElement );

      foreach( string key in m_environmentVars.Keys )
      {
        XmlElement varElement = xmlDoc.CreateElement( "Var" );
        varCollectionElement.AppendChild( varElement );

        newAttrib = xmlDoc.CreateAttribute( "name" );
        newAttrib.Value = key;
        varElement.Attributes.Append( newAttrib );

        string value;
        bool enabled;
        if( m_environmentVars.TryGetValue( key, out value ) &&
            m_environmentVarStates.TryGetValue( key, out enabled ) )
        {
          newAttrib = xmlDoc.CreateAttribute( "value" );
          newAttrib.Value = value;
          varElement.Attributes.Append( newAttrib );

          newAttrib = xmlDoc.CreateAttribute( "enabled" );
          newAttrib.Value = enabled.ToString();
          varElement.Attributes.Append( newAttrib );
        }
        else
        {
          varCollectionElement.RemoveChild( varElement );
        }
      }

      // linked shortcuts
      XmlElement linkedCollectionElement = xmlDoc.CreateElement( "LinkedShortcuts" );
      element.AppendChild( linkedCollectionElement );

      foreach( string shortcut in m_linkedShortcutDescriptions )
      {
        XmlElement shortcutElement = xmlDoc.CreateElement( "Shortcut" );
        linkedCollectionElement.AppendChild( shortcutElement );

        newAttrib = xmlDoc.CreateAttribute( "description" );
        newAttrib.Value = shortcut;
        shortcutElement.Attributes.Append( newAttrib );
      }
    }

    //-------------------------------------------------------------------------

    public static string GetArgumentString( Dictionary< string, string > args,
                                            Dictionary< string, bool > argStates,
                                            Project project )
    {
      string s = "";

      foreach( string key in args.Keys )
      {
        // is arg enabled?
        bool enabled;
        if( argStates.TryGetValue( key, out enabled ) == false )
        {
          continue;
        }

        // append arg to string if it's enabled
        if( enabled )
        {
          s += key;

          string value;

          if( args.TryGetValue( key, out value ) &&
              value != "" )
          {
            value = project.GetCommonValue( value, true );

            s += " " + value;
          }

          s += " ";
        }
      }

      return s;
    }

    //-------------------------------------------------------------------------

    public TemplateShortcutEntry CreateCopy()
    {
      TemplateShortcutEntry copy = new TemplateShortcutEntry();
      copy.Description = "Copy of " + Description;
      copy.Filename = m_filename;
      copy.m_uiGroupName = m_uiGroupName;
      copy.m_confirmBeforeRunning = m_confirmBeforeRunning;
      copy.m_scheduledRunEnabled = m_scheduledRunEnabled;
      copy.m_nextScheduledRunTime = m_nextScheduledRunTime;

      foreach( string key in m_args.Keys )
      {
        string value = "";
        if( m_args.TryGetValue( key, out value ) )
        {
          copy.Arguments.Add( key, value );
        }
        else
        {
          // TODO
        }
      }

      foreach( string key in m_environmentVars.Keys )
      {
        string value = "";
        if( m_environmentVars.TryGetValue( key, out value ) )
        {
          copy.EnvironmentVars.Add( key, value );
        }
        else
        {
          // TODO
        }
      }

      foreach( string key in m_argStates.Keys )
      {
        bool enabled;
        if( m_argStates.TryGetValue( key, out enabled ) )
        {
          copy.ArgumentStates.Add( key, enabled );
        }
        else
        {
          // TODO
        }
      }

      foreach( string key in m_environmentVarStates.Keys )
      {
        bool enabled;
        if( m_environmentVarStates.TryGetValue( key, out enabled ) )
        {
          copy.EnvironmentVarStates.Add( key, enabled );
        }
        else
        {
          // TODO
        }
      }

      foreach( string entry in m_linkedShortcutDescriptions )
      {
        copy.LinkedShortcuts.Add( entry );
      }

      return copy;
    }

    //-------------------------------------------------------------------------

    public string Filename
    {
      get
      {
        return m_filename;
      }

      set
      {
        m_filename = value;
      }
    }

    //-------------------------------------------------------------------------

    public Dictionary< string, string > Arguments
    {
      get
      {
        return m_args;
      }

      set
      {
        m_args = value;
      }
    }

    //-------------------------------------------------------------------------

    public Dictionary< string, string > EnvironmentVars
    {
      get
      {
        return m_environmentVars;
      }

      set
      {
        m_environmentVars = value;
      }
    }

    //-------------------------------------------------------------------------

    public Dictionary< string, bool > ArgumentStates
    {
      get
      {
        return m_argStates;
      }

      set
      {
        m_argStates = value;
      }
    }

    //-------------------------------------------------------------------------

    public Dictionary< string, bool > EnvironmentVarStates
    {
      get
      {
        return m_environmentVarStates;
      }

      set
      {
        m_environmentVarStates = value;
      }
    }
    
    //-------------------------------------------------------------------------

    public Project Project
    {
      get
      {
        return m_project;
      }

      set
      {
        m_project = value;
      }
    }

    //-------------------------------------------------------------------------

    public string UiGroupName
    {
      get
      {
        return m_uiGroupName;
      }

      set
      {
        m_uiGroupName = value;
      }
    }

    //-------------------------------------------------------------------------

    public List< string > LinkedShortcuts
    {
      get
      {
        return m_linkedShortcutDescriptions;
      }

      set
      {
        m_linkedShortcutDescriptions = value;
      }
    }

    //-------------------------------------------------------------------------

    public bool ConfirmBeforeRunning
    {
      get
      {
        return m_confirmBeforeRunning;
      }

      set
      {
        m_confirmBeforeRunning = value;
      }
    }

    //-------------------------------------------------------------------------

    public bool ScheduledRunEnabled
    {
      get
      {
        return m_scheduledRunEnabled;
      }

      set
      {
        m_scheduledRunEnabled = value;
      }
    }

    //-------------------------------------------------------------------------

    public DateTime NextScheduledRunTime
    {
      get
      {
        return m_nextScheduledRunTime;
      }

      set
      {
        m_nextScheduledRunTime = value;
      }
    }

    //-------------------------------------------------------------------------

    public override string ToString()
    {
      if( m_project == null )
      {
        return Description;
      }
      else
      {
        return m_project.GetCommonValue( Description, false );
      }
    }

    //-------------------------------------------------------------------------

    public override void PostLoadEvent( Project project )
    {

    }

    //-------------------------------------------------------------------------
  }
}
