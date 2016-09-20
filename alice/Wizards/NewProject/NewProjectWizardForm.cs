using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Xml;
using System.IO;

namespace alice
{
  public partial class NewProjectWizardForm : Form
  {
    //-------------------------------------------------------------------------

    private string m_sourcePathAbs;
    private string m_projectsPathAbs;
    private string m_initDataPathAbs;

    private class SolutionInfo
    {
      public string m_name;
      public string m_projectPathAbs;
      public string m_managerSolutionPathAbs;
      public string m_simSolutionPathAbs;
      public string m_benchSolutionPathAbs;
      public string m_managerExecutablePathAbs;
      public string m_simExecutablePathAbs;
      public string m_managerOnlySimExecutablePathAbs;
      public string m_benchExecutablePathAbs;
    };

    Dictionary< string, SolutionInfo > m_solutions = new Dictionary< string, SolutionInfo >();

    private string m_masterConfigPath = "";

    private Project m_newProject;
    private MasterConfig m_masterConfig;

    //-------------------------------------------------------------------------

    public NewProjectWizardForm()
    {
      try
      {
        InitializeComponent();
        LoadSettings();
        SetupForm();

        masterConfigLbl.Text = "";

        if( m_solutions.Count > 0 )
        {
          solutionCbo.SelectedIndex = 0;
        }

        dfltManagerBuildCbo.SelectedItem = "Release";
        dfltSimBuildCbo.SelectedItem = "FastDebug";
        dfltBenchBuildCbo.SelectedItem = "FastDebug";

        modelFolderLbl.Text = "";
        worldFolderLbl.Text = "";
        specLbl.Text = "";
        worldSpecLbl.Text = "";
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void LoadSettings()
    {
      try
      {
        m_solutions.Clear();

        //-- Source path exists?
        m_sourcePathAbs = Environment.GetEnvironmentVariable( "TTDEVPATH" );

        if( m_sourcePathAbs == null )
        {
          m_sourcePathAbs = @"c:\dev\main\source\";
        }
        else
        {
          if( m_sourcePathAbs[ m_sourcePathAbs.Length - 1 ] != '\\' )
          {
            m_sourcePathAbs += '\\';
          }

          m_sourcePathAbs += "source\\";
        }

        sourceFolderTxt.Text = m_sourcePathAbs;

        if( Directory.Exists( m_sourcePathAbs ) == false )
        {
          MessageBox.Show( "Failed to find 'source' path '" + m_sourcePathAbs + "'.",
                           "Error",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Error );
          okBtn.Enabled = false;
          sourceFolderTxt.Focus();
          return;
        }

        okBtn.Enabled = true;

        //-- Load xml.
        XmlDocument doc = new XmlDocument();
        doc.Load( Program.g_path + "\\res\\NewProjectWizard.xml" );

        //-- Paths.
        XmlElement pathsElement = doc.SelectSingleNode( "NewProjectWizard/Paths" ) as XmlElement;

        m_projectsPathAbs = m_sourcePathAbs + pathsElement.Attributes[ "projectsRel" ].Value;
        m_initDataPathAbs = m_sourcePathAbs + pathsElement.Attributes[ "initDataRepositoryRel" ].Value;

        //-- Read the solutions collection.
        XmlElement solutionCollection = doc.SelectSingleNode( "NewProjectWizard/SolutionCollection" ) as XmlElement;

        if( solutionCollection != null )
        {
          XmlNodeList solutionElements = solutionCollection.SelectNodes( "Solution" );

          foreach( XmlElement e in solutionElements )
          {
            SolutionInfo info = new SolutionInfo();
            info.m_name = e.Attributes[ "name" ].Value;
            info.m_projectPathAbs = m_projectsPathAbs + e.Attributes[ "projectPathRel" ].Value;
            info.m_managerSolutionPathAbs = m_projectsPathAbs + e.Attributes[ "managerSolutionPathRel" ].Value;
            info.m_simSolutionPathAbs = m_projectsPathAbs + e.Attributes[ "simSolutionPathRel" ].Value;
            info.m_benchSolutionPathAbs = m_sourcePathAbs + e.Attributes[ "benchSolutionPathRel" ].Value;

            FileInfo fileInfo = new FileInfo( info.m_managerSolutionPathAbs );
            info.m_managerExecutablePathAbs = fileInfo.DirectoryName + "\\PRJ_BUILD_PROFILE_MANAGER\\" + e.Attributes[ "managerFilename" ].Value;

            fileInfo = new FileInfo( info.m_simSolutionPathAbs );
            info.m_simExecutablePathAbs = fileInfo.DirectoryName + "\\PRJ_BUILD_PROFILE_SIM\\" + e.Attributes[ "simFilename" ].Value;

            fileInfo = new FileInfo( info.m_managerSolutionPathAbs );
            info.m_managerOnlySimExecutablePathAbs = fileInfo.DirectoryName + "\\PRJ_BUILD_PROFILE_MANAGER\\" + e.Attributes[ "simFilename" ].Value;

            fileInfo = new FileInfo( info.m_benchSolutionPathAbs );
            info.m_benchExecutablePathAbs = fileInfo.DirectoryName + "\\PRJ_BUILD_PROFILE_BENCH\\" + e.Attributes[ "benchFilename" ].Value;

            // Add to collection.
            m_solutions.Add( info.m_name, info );
          }
        }
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void SetupForm()
    {
      //-- Populate solutions dropdown list.
      foreach( SolutionInfo info in m_solutions.Values )
      {
        solutionCbo.Items.Add( info.m_name );
      }
    }

    //-------------------------------------------------------------------------

    private void okBtn_Click( object sender, EventArgs e )
    {
      try
      {
        if( m_masterConfigPath == "" )
        {
          ErrorMsg( "Select a MasterConfig first." );
          return;
        }

        if( projectNameTxt.Text == "" )
        {
          ErrorMsg( "Enter a 'Project name' first." );
          return;
        }

        bool result = CreateProject( solutionCbo.Text, projectNameTxt.Text );

        if( result )
        {
          DialogResult = DialogResult.OK;
          Hide();
        }
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void ErrorMsg( string message )
    {
      MessageBox.Show( message,
                       "Error",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Error );
    }

    //-------------------------------------------------------------------------

    private void masterConfigBrowse_Click( object sender, EventArgs e )
    {
      try
      {
        //-- If a solution was selected, get the info for it.
        SolutionInfo info = null;

        if( solutionCbo.SelectedIndex != -1 )
        {
          info = m_solutions[ solutionCbo.SelectedItem as string ];
        }

        //-- Show the file selection dialog.
        OpenFileDialog dlg = new OpenFileDialog();
        dlg.CheckFileExists = true;
        dlg.Multiselect = false;
        dlg.InitialDirectory = ( info != null ? info.m_projectPathAbs : m_projectsPathAbs );
        dlg.Filter = "MasterConfig | *.masterconfig";
        dlg.ShowDialog();

        if( dlg.FileName != "" )
        {
          m_masterConfigPath = dlg.FileName;

          if( m_masterConfigPath.Length >= 40 )
          {
            masterConfigLbl.Text = "..." + m_masterConfigPath.Substring( m_masterConfigPath.Length - 40, 40 );
          }

          FileInfo fileInfo = new FileInfo( m_masterConfigPath );
          projectNameTxt.Text = fileInfo.Directory.Name;

          //-- Update using the masterconfig.
          m_masterConfig = new MasterConfig( m_masterConfigPath );

          projectNameTxt.Text = m_masterConfig.VehicleName;
          modelFolderLbl.Text = m_masterConfig.ModelPath;
          worldFolderLbl.Text = m_masterConfig.WorldPath;
        }
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private bool CreateProject( string solution, string projectName )
    {
      m_newProject = null;

      try
      {
        //-- Get solution info.
        SolutionInfo info = m_solutions[ solution ];

        //-- Create a Project.
        try
        {
          m_newProject = Program.g_projectManager.CreateProject( projectName );
        }
        catch( Exception ex )
        {
          ErrorMsg( ex.Message );
          return false;
        }

        string simExecPath =
          uiManagerOnly.Checked ? info.m_managerOnlySimExecutablePathAbs : info.m_simExecutablePathAbs;

        // Profiles
        TemplateCommonValueCollectionEntry newProfile = new TemplateCommonValueCollectionEntry();
        newProfile.Description = "Default";
        newProfile.Values.Add( "PRJ_BUILD_PROFILE_MANAGER", dfltManagerBuildCbo.SelectedItem as string );
        newProfile.Values.Add( "PRJ_BUILD_PROFILE_SIM", dfltSimBuildCbo.SelectedItem as string );
        newProfile.Values.Add( "PRJ_BUILD_PROFILE_BENCH", dfltBenchBuildCbo.SelectedItem as string );
        newProfile.Values.Add( "PRJ_BUILD_PLATFORM_MANAGER", dfltManagerPlatformCbo.Text );
        newProfile.Values.Add( "PRJ_BUILD_PLATFORM_SIM", dfltSimPlatformCbo.Text );
        newProfile.Values.Add( "PRJ_BUILD_PLATFORM_BENCH", dfltBenchPlatformCbo.Text );
        m_newProject.Template.AddEntry( newProfile );

        newProfile = new TemplateCommonValueCollectionEntry();
        newProfile.Description = "Debug";
        newProfile.Values.Add( "PRJ_BUILD_PROFILE_MANAGER", "Debug" );
        newProfile.Values.Add( "PRJ_BUILD_PROFILE_SIM", "Debug" );
        newProfile.Values.Add( "PRJ_BUILD_PROFILE_BENCH", "Debug" );
        newProfile.Values.Add( "PRJ_BUILD_PLATFORM_MANAGER", dfltManagerPlatformCbo.Text );
        newProfile.Values.Add( "PRJ_BUILD_PLATFORM_SIM", dfltSimPlatformCbo.Text );
        newProfile.Values.Add( "PRJ_BUILD_PLATFORM_BENCH", dfltBenchPlatformCbo.Text );
        m_newProject.Template.AddEntry( newProfile );

        newProfile = new TemplateCommonValueCollectionEntry();
        newProfile.Description = "FastDebug";
        newProfile.Values.Add( "PRJ_BUILD_PROFILE_MANAGER", "FastDebug" );
        newProfile.Values.Add( "PRJ_BUILD_PROFILE_SIM", "FastDebug" );
        newProfile.Values.Add( "PRJ_BUILD_PROFILE_BENCH", "FastDebug" );
        newProfile.Values.Add( "PRJ_BUILD_PLATFORM_MANAGER", dfltManagerPlatformCbo.Text );
        newProfile.Values.Add( "PRJ_BUILD_PLATFORM_SIM", dfltSimPlatformCbo.Text );
        newProfile.Values.Add( "PRJ_BUILD_PLATFORM_BENCH", dfltBenchPlatformCbo.Text );
        m_newProject.Template.AddEntry( newProfile );

        newProfile = new TemplateCommonValueCollectionEntry();
        newProfile.Description = "Release";
        newProfile.Values.Add( "PRJ_BUILD_PROFILE_MANAGER", "Release" );
        newProfile.Values.Add( "PRJ_BUILD_PROFILE_SIM", "Release" );
        newProfile.Values.Add( "PRJ_BUILD_PROFILE_BENCH", "Release" );
        newProfile.Values.Add( "PRJ_BUILD_PLATFORM_MANAGER", dfltManagerPlatformCbo.Text );
        newProfile.Values.Add( "PRJ_BUILD_PLATFORM_SIM", dfltSimPlatformCbo.Text );
        newProfile.Values.Add( "PRJ_BUILD_PLATFORM_BENCH", dfltBenchPlatformCbo.Text );
        m_newProject.Template.AddEntry( newProfile );

        // Build All shortcut
        TemplateShortcutEntry newShortcut = new TemplateShortcutEntry();
        newShortcut.Description = "Build All";
        newShortcut.Type = TemplateEntry.EntryType.Type_Shortcut;
        newShortcut.UiGroupName = "Build";
        newShortcut.ConfirmBeforeRunning = true;
        newShortcut.LinkedShortcuts.Add( "Build Manager (PRJ_BUILD_PROFILE_MANAGER)" );
        newShortcut.LinkedShortcuts.Add( "Build Sim (PRJ_BUILD_PROFILE_SIM)" );
        newShortcut.LinkedShortcuts.Add( "Build Bench (PRJ_BUILD_PROFILE_BENCH)" );

        m_newProject.Template.AddEntry( newShortcut );

        // Build Manager & Sim shortcut
        if( uiManagerOnly.Checked == false )
        {
          newShortcut = new TemplateShortcutEntry();
          newShortcut.Description = "Build Manager & Sim";
          newShortcut.Type = TemplateEntry.EntryType.Type_Shortcut;
          newShortcut.UiGroupName = "Build";
          newShortcut.LinkedShortcuts.Add( "Build Manager (PRJ_BUILD_PROFILE_MANAGER)" );
          newShortcut.LinkedShortcuts.Add( "Build Sim (PRJ_BUILD_PROFILE_SIM)" );

          m_newProject.Template.AddEntry( newShortcut );
        }

        // Build Manager shortcut
        newShortcut = new TemplateShortcutEntry();
        newShortcut.Description = "Build Manager (PRJ_BUILD_PROFILE_MANAGER)";
        newShortcut.Type = TemplateEntry.EntryType.Type_Shortcut;
        newShortcut.Filename = "ALICE_APP_PATH\\res\\compile.bat";
        newShortcut.UiGroupName = "Build";

        newShortcut.EnvironmentVars.Add( "COMPILE_DESCRIPTION", info.m_name + ": Manager Solution" );
        newShortcut.EnvironmentVarStates.Add( "COMPILE_DESCRIPTION", true );
        newShortcut.EnvironmentVars.Add( "COMPILE_PROFILE", "PRJ_BUILD_PROFILE_MANAGER" );
        newShortcut.EnvironmentVarStates.Add( "COMPILE_PROFILE", true );
        newShortcut.EnvironmentVars.Add( "COMPILE_PLATFORM", "PRJ_BUILD_PLATFORM_MANAGER" );
        newShortcut.EnvironmentVarStates.Add( "COMPILE_PLATFORM", true );
        newShortcut.EnvironmentVars.Add( "VS_VC_BIN_PATH", "ALICE_VS_VC_BIN" );
        newShortcut.EnvironmentVarStates.Add( "VS_VC_BIN_PATH", true );
        newShortcut.EnvironmentVars.Add( "COMPILER_FULL_FILENAME", "ALICE_VS_DEVENV" );
        newShortcut.EnvironmentVarStates.Add( "COMPILER_FULL_FILENAME", true );
        newShortcut.EnvironmentVars.Add( "COMPILE_SOLUTION", info.m_managerSolutionPathAbs );
        newShortcut.EnvironmentVarStates.Add( "COMPILE_SOLUTION", true );
        newShortcut.EnvironmentVars.Add( "COMPILE_OUTPUT", "ALICE_MANAGER_BUILD_OUTPUT" );
        newShortcut.EnvironmentVarStates.Add( "COMPILE_OUTPUT", true );

        m_newProject.Template.AddEntry( newShortcut );

        // Build Sim shortcut
        if( uiManagerOnly.Checked == false )
        {
          newShortcut = new TemplateShortcutEntry();
          newShortcut.Description = "Build Sim (PRJ_BUILD_PROFILE_SIM)";
          newShortcut.Type = TemplateEntry.EntryType.Type_Shortcut;
          newShortcut.Filename = "ALICE_APP_PATH\\res\\compile.bat";
          newShortcut.UiGroupName = "Build";

          newShortcut.EnvironmentVars.Add( "COMPILE_DESCRIPTION", info.m_name + ": Sim Solution" );
          newShortcut.EnvironmentVarStates.Add( "COMPILE_DESCRIPTION", true );
          newShortcut.EnvironmentVars.Add( "COMPILE_PROFILE", "PRJ_BUILD_PROFILE_SIM" );
          newShortcut.EnvironmentVarStates.Add( "COMPILE_PROFILE", true );
          newShortcut.EnvironmentVars.Add( "COMPILE_PLATFORM", "PRJ_BUILD_PLATFORM_SIM" );
          newShortcut.EnvironmentVarStates.Add( "COMPILE_PLATFORM", true );
          newShortcut.EnvironmentVars.Add( "VS_VC_BIN_PATH", "ALICE_VS_VC_BIN" );
          newShortcut.EnvironmentVarStates.Add( "VS_VC_BIN_PATH", true );
          newShortcut.EnvironmentVars.Add( "COMPILER_FULL_FILENAME", "ALICE_VS_DEVENV" );
          newShortcut.EnvironmentVarStates.Add( "COMPILER_FULL_FILENAME", true );
          newShortcut.EnvironmentVars.Add( "COMPILE_SOLUTION", info.m_simSolutionPathAbs );
          newShortcut.EnvironmentVarStates.Add( "COMPILE_SOLUTION", true );
          newShortcut.EnvironmentVars.Add( "COMPILE_OUTPUT", "ALICE_SIM_BUILD_OUTPUT" );
          newShortcut.EnvironmentVarStates.Add( "COMPILE_OUTPUT", true );

          m_newProject.Template.AddEntry( newShortcut );
        }

        // Build Bench shortcut
        newShortcut = new TemplateShortcutEntry();
        newShortcut.Description = "Build Bench (PRJ_BUILD_PROFILE_BENCH)";
        newShortcut.Type = TemplateEntry.EntryType.Type_Shortcut;
        newShortcut.Filename = "ALICE_APP_PATH\\res\\compile.bat";
        newShortcut.UiGroupName = "Build";

        newShortcut.EnvironmentVars.Add( "COMPILE_DESCRIPTION", info.m_name + ": Bench Solution" );
        newShortcut.EnvironmentVarStates.Add( "COMPILE_DESCRIPTION", true );
        newShortcut.EnvironmentVars.Add( "COMPILE_PROFILE", "PRJ_BUILD_PROFILE_BENCH" );
        newShortcut.EnvironmentVarStates.Add( "COMPILE_PROFILE", true );
        newShortcut.EnvironmentVars.Add( "COMPILE_PLATFORM", "PRJ_BUILD_PLATFORM_BENCH" );
        newShortcut.EnvironmentVarStates.Add( "COMPILE_PLATFORM", true );
        newShortcut.EnvironmentVars.Add( "VS_VC_BIN_PATH", "ALICE_VS_VC_BIN" );
        newShortcut.EnvironmentVarStates.Add( "VS_VC_BIN_PATH", true );
        newShortcut.EnvironmentVars.Add( "COMPILER_FULL_FILENAME", "ALICE_VS_DEVENV" );
        newShortcut.EnvironmentVarStates.Add( "COMPILER_FULL_FILENAME", true );
        newShortcut.EnvironmentVars.Add( "COMPILE_SOLUTION", info.m_benchSolutionPathAbs );
        newShortcut.EnvironmentVarStates.Add( "COMPILE_SOLUTION", true );
        newShortcut.EnvironmentVars.Add( "COMPILE_OUTPUT", "ALICE_SIM_BUILD_OUTPUT" );
        newShortcut.EnvironmentVarStates.Add( "COMPILE_OUTPUT", true );
        
        m_newProject.Template.AddEntry( newShortcut );

        // Clean Manager shortcut
        newShortcut = new TemplateShortcutEntry();
        newShortcut.Description = "Clean Manager (PRJ_BUILD_PROFILE_MANAGER)";
        newShortcut.Type = TemplateEntry.EntryType.Type_Shortcut;
        newShortcut.Filename = "ALICE_APP_PATH\\res\\clean.bat";
        newShortcut.UiGroupName = "Clean";
        newShortcut.ConfirmBeforeRunning = true;

        newShortcut.EnvironmentVars.Add( "COMPILE_DESCRIPTION", info.m_name + ": Manager Solution" );
        newShortcut.EnvironmentVarStates.Add( "COMPILE_DESCRIPTION", true );
        newShortcut.EnvironmentVars.Add( "COMPILE_PROFILE", "PRJ_BUILD_PROFILE_MANAGER" );
        newShortcut.EnvironmentVarStates.Add( "COMPILE_PROFILE", true );
        newShortcut.EnvironmentVars.Add( "VS_VC_BIN_PATH", "ALICE_VS_VC_BIN" );
        newShortcut.EnvironmentVarStates.Add( "VS_VC_BIN_PATH", true );
        newShortcut.EnvironmentVars.Add( "COMPILER_FULL_FILENAME", "ALICE_VS_DEVENV" );
        newShortcut.EnvironmentVarStates.Add( "COMPILER_FULL_FILENAME", true );
        newShortcut.EnvironmentVars.Add( "COMPILE_SOLUTION", info.m_managerSolutionPathAbs );
        newShortcut.EnvironmentVarStates.Add( "COMPILE_SOLUTION", true );
        newShortcut.EnvironmentVars.Add( "COMPILE_OUTPUT", "ALICE_MANAGER_BUILD_OUTPUT" );
        newShortcut.EnvironmentVarStates.Add( "COMPILE_OUTPUT", true );

        m_newProject.Template.AddEntry( newShortcut );

        // Clean Sim shortcut
        if( uiManagerOnly.Checked == false )
        {
          newShortcut = new TemplateShortcutEntry();
          newShortcut.Description = "Clean Sim (PRJ_BUILD_PROFILE_SIM)";
          newShortcut.Type = TemplateEntry.EntryType.Type_Shortcut;
          newShortcut.Filename = "ALICE_APP_PATH\\res\\clean.bat";
          newShortcut.UiGroupName = "Clean";
          newShortcut.ConfirmBeforeRunning = true;

          newShortcut.EnvironmentVars.Add( "COMPILE_DESCRIPTION", info.m_name + ": Sim Solution" );
          newShortcut.EnvironmentVarStates.Add( "COMPILE_DESCRIPTION", true );
          newShortcut.EnvironmentVars.Add( "COMPILE_PROFILE", "PRJ_BUILD_PROFILE_SIM" );
          newShortcut.EnvironmentVarStates.Add( "COMPILE_PROFILE", true );
          newShortcut.EnvironmentVars.Add( "VS_VC_BIN_PATH", "ALICE_VS_VC_BIN" );
          newShortcut.EnvironmentVarStates.Add( "VS_VC_BIN_PATH", true );
          newShortcut.EnvironmentVars.Add( "COMPILER_FULL_FILENAME", "ALICE_VS_DEVENV" );
          newShortcut.EnvironmentVarStates.Add( "COMPILER_FULL_FILENAME", true );
          newShortcut.EnvironmentVars.Add( "COMPILE_SOLUTION", info.m_simSolutionPathAbs );
          newShortcut.EnvironmentVarStates.Add( "COMPILE_SOLUTION", true );
          newShortcut.EnvironmentVars.Add( "COMPILE_OUTPUT", "ALICE_SIM_BUILD_OUTPUT" );
          newShortcut.EnvironmentVarStates.Add( "COMPILE_OUTPUT", true );

          m_newProject.Template.AddEntry( newShortcut );
        }

        // Clean Bench shortcut
        newShortcut = new TemplateShortcutEntry();
        newShortcut.Description = "Clean Bench (PRJ_BUILD_PROFILE_BENCH)";
        newShortcut.Type = TemplateEntry.EntryType.Type_Shortcut;
        newShortcut.Filename = "ALICE_APP_PATH\\res\\clean.bat";
        newShortcut.UiGroupName = "Clean";
        newShortcut.ConfirmBeforeRunning = true;

        newShortcut.EnvironmentVars.Add( "COMPILE_DESCRIPTION", info.m_name + ": Bench Solution" );
        newShortcut.EnvironmentVarStates.Add( "COMPILE_DESCRIPTION", true );
        newShortcut.EnvironmentVars.Add( "COMPILE_PROFILE", "PRJ_BUILD_PROFILE_BENCH" );
        newShortcut.EnvironmentVarStates.Add( "COMPILE_PROFILE", true );
        newShortcut.EnvironmentVars.Add( "VS_VC_BIN_PATH", "ALICE_VS_VC_BIN" );
        newShortcut.EnvironmentVarStates.Add( "VS_VC_BIN_PATH", true );
        newShortcut.EnvironmentVars.Add( "COMPILER_FULL_FILENAME", "ALICE_VS_DEVENV" );
        newShortcut.EnvironmentVarStates.Add( "COMPILER_FULL_FILENAME", true );
        newShortcut.EnvironmentVars.Add( "COMPILE_SOLUTION", info.m_benchSolutionPathAbs );
        newShortcut.EnvironmentVarStates.Add( "COMPILE_SOLUTION", true );
        newShortcut.EnvironmentVars.Add( "COMPILE_OUTPUT", "ALICE_SIM_BUILD_OUTPUT" );
        newShortcut.EnvironmentVarStates.Add( "COMPILE_OUTPUT", true );
        
        m_newProject.Template.AddEntry( newShortcut );

        // Run Manager & Sim shortcut
        newShortcut = new TemplateShortcutEntry();
        newShortcut.Description = "Run Manager & Dynamics";
        newShortcut.Type = TemplateEntry.EntryType.Type_Shortcut;
        newShortcut.UiGroupName = "Run";
        newShortcut.LinkedShortcuts.Add( "Run Manager" );
        newShortcut.LinkedShortcuts.Add( "Run Dynamics" );

        m_newProject.Template.AddEntry( newShortcut );

        // Run Manager shortcut
        newShortcut = new TemplateShortcutEntry();
        newShortcut.Description = "Run Manager";
        newShortcut.Type = TemplateEntry.EntryType.Type_Shortcut;
        newShortcut.Filename = info.m_managerExecutablePathAbs;
        newShortcut.UiGroupName = "Run";

        newShortcut.Arguments.Add( "-networkname", "SimManager" );
        newShortcut.ArgumentStates.Add( "-networkname", true );
        newShortcut.Arguments.Add( "-serverport", "ALICE_SERVER_PORT" );
        newShortcut.ArgumentStates.Add( "-serverport", true );
        newShortcut.Arguments.Add( "-config", "\"" + m_masterConfigPath + "\"" );
        newShortcut.ArgumentStates.Add( "-config", true );
        newShortcut.Arguments.Add( "-devmode", "" );
        newShortcut.ArgumentStates.Add( "-devmode", true );
        newShortcut.Arguments.Add( "-nohardware", "" );
        newShortcut.ArgumentStates.Add( "-nohardware", true );
        newShortcut.Arguments.Add( "-devpath", "" );
        newShortcut.ArgumentStates.Add( "-devpath", true );

        newShortcut.EnvironmentVars.Add( "TTDEVPATH", m_sourcePathAbs + "..\\" );
        newShortcut.EnvironmentVarStates.Add( "TTDEVPATH", true );
        newShortcut.EnvironmentVars.Add( "TTLIBPATH", m_sourcePathAbs + "..\\Library\\" );
        newShortcut.EnvironmentVarStates.Add( "TTLIBPATH", true );

        m_newProject.Template.AddEntry( newShortcut );

        // Run Dynamics shortcut
        newShortcut = new TemplateShortcutEntry();
        newShortcut.Description = "Run Dynamics";
        newShortcut.Type = TemplateEntry.EntryType.Type_Shortcut;
        newShortcut.Filename = simExecPath;
        newShortcut.UiGroupName = "Run";

        newShortcut.Arguments.Add( "-networkname", "Dynamics" );
        newShortcut.ArgumentStates.Add( "-networkname", true );
        newShortcut.Arguments.Add( "-servername", "ALICE_SERVER" );
        newShortcut.ArgumentStates.Add( "-servername", true );
        newShortcut.Arguments.Add( "-serverport", "ALICE_SERVER_PORT" );
        newShortcut.ArgumentStates.Add( "-serverport", true );
        newShortcut.Arguments.Add( "-config", "\"" + m_masterConfigPath + "\"" );
        newShortcut.ArgumentStates.Add( "-config", true );
        newShortcut.Arguments.Add( "-devmode", "" );
        newShortcut.ArgumentStates.Add( "-devmode", true );
        newShortcut.Arguments.Add( "-nohardware", "" );
        newShortcut.ArgumentStates.Add( "-nohardware", true );
        newShortcut.Arguments.Add( "-devpath", "" );
        newShortcut.ArgumentStates.Add( "-devpath", true );

        newShortcut.EnvironmentVars.Add( "TTDEVPATH", m_sourcePathAbs + "..\\" );
        newShortcut.EnvironmentVarStates.Add( "TTDEVPATH", true );
        newShortcut.EnvironmentVars.Add( "TTLIBPATH", m_sourcePathAbs + "..\\Library\\" );
        newShortcut.EnvironmentVarStates.Add( "TTLIBPATH", true );

        m_newProject.Template.AddEntry( newShortcut );

        // Run Centre View shortcut
        newShortcut = new TemplateShortcutEntry();
        newShortcut.Description = "Run Centre View";
        newShortcut.Type = TemplateEntry.EntryType.Type_Shortcut;
        newShortcut.Filename = simExecPath;
        newShortcut.UiGroupName = "Run";

        newShortcut.Arguments.Add( "-networkname", "CentreView" );
        newShortcut.ArgumentStates.Add( "-networkname", true );
        newShortcut.Arguments.Add( "-servername", "ALICE_SERVER" );
        newShortcut.ArgumentStates.Add( "-servername", true );
        newShortcut.Arguments.Add( "-serverport", "ALICE_SERVER_PORT" );
        newShortcut.ArgumentStates.Add( "-serverport", true );
        newShortcut.Arguments.Add( "-config", "\"" + m_masterConfigPath + "\"" );
        newShortcut.ArgumentStates.Add( "-config", true );
        newShortcut.Arguments.Add( "-devmode", "" );
        newShortcut.ArgumentStates.Add( "-devmode", true );
        newShortcut.Arguments.Add( "-nohardware", "" );
        newShortcut.ArgumentStates.Add( "-nohardware", true );
        newShortcut.Arguments.Add( "-devpath", "" );
        newShortcut.ArgumentStates.Add( "-devpath", true );

        newShortcut.EnvironmentVars.Add( "TTDEVPATH", m_sourcePathAbs + "..\\" );
        newShortcut.EnvironmentVarStates.Add( "TTDEVPATH", true );
        newShortcut.EnvironmentVars.Add( "TTLIBPATH", m_sourcePathAbs + "..\\Library\\" );
        newShortcut.EnvironmentVarStates.Add( "TTLIBPATH", true );

        m_newProject.Template.AddEntry( newShortcut );

        // Run LeftView shortcut
        newShortcut = new TemplateShortcutEntry();
        newShortcut.Description = "Run Left View";
        newShortcut.Type = TemplateEntry.EntryType.Type_Shortcut;
        newShortcut.Filename = simExecPath;
        newShortcut.UiGroupName = "Run";

        newShortcut.Arguments.Add( "-networkname", "LeftView" );
        newShortcut.ArgumentStates.Add( "-networkname", true );
        newShortcut.Arguments.Add( "-servername", "ALICE_SERVER" );
        newShortcut.ArgumentStates.Add( "-servername", true );
        newShortcut.Arguments.Add( "-serverport", "ALICE_SERVER_PORT" );
        newShortcut.ArgumentStates.Add( "-serverport", true );
        newShortcut.Arguments.Add( "-config", "\"" + m_masterConfigPath + "\"" );
        newShortcut.ArgumentStates.Add( "-config", true );
        newShortcut.Arguments.Add( "-devmode", "" );
        newShortcut.ArgumentStates.Add( "-devmode", true );
        newShortcut.Arguments.Add( "-nohardware", "" );
        newShortcut.ArgumentStates.Add( "-nohardware", true );
        newShortcut.Arguments.Add( "-devpath", "" );
        newShortcut.ArgumentStates.Add( "-devpath", true );

        newShortcut.EnvironmentVars.Add( "TTDEVPATH", m_sourcePathAbs + "..\\" );
        newShortcut.EnvironmentVarStates.Add( "TTDEVPATH", true );
        newShortcut.EnvironmentVars.Add( "TTLIBPATH", m_sourcePathAbs + "..\\Library\\" );
        newShortcut.EnvironmentVarStates.Add( "TTLIBPATH", true );

        m_newProject.Template.AddEntry( newShortcut );

        // Run RightView shortcut
        newShortcut = new TemplateShortcutEntry();
        newShortcut.Description = "Run Right View";
        newShortcut.Type = TemplateEntry.EntryType.Type_Shortcut;
        newShortcut.Filename = simExecPath;
        newShortcut.UiGroupName = "Run";

        newShortcut.Arguments.Add( "-networkname", "RightView" );
        newShortcut.ArgumentStates.Add( "-networkname", true );
        newShortcut.Arguments.Add( "-servername", "ALICE_SERVER" );
        newShortcut.ArgumentStates.Add( "-servername", true );
        newShortcut.Arguments.Add( "-serverport", "ALICE_SERVER_PORT" );
        newShortcut.ArgumentStates.Add( "-serverport", true );
        newShortcut.Arguments.Add( "-config", "\"" + m_masterConfigPath + "\"" );
        newShortcut.ArgumentStates.Add( "-config", true );
        newShortcut.Arguments.Add( "-devmode", "" );
        newShortcut.ArgumentStates.Add( "-devmode", true );
        newShortcut.Arguments.Add( "-nohardware", "" );
        newShortcut.ArgumentStates.Add( "-nohardware", true );
        newShortcut.Arguments.Add( "-devpath", "" );
        newShortcut.ArgumentStates.Add( "-devpath", true );

        newShortcut.EnvironmentVars.Add( "TTDEVPATH", m_sourcePathAbs + "..\\" );
        newShortcut.EnvironmentVarStates.Add( "TTDEVPATH", true );
        newShortcut.EnvironmentVars.Add( "TTLIBPATH", m_sourcePathAbs + "..\\Library\\" );
        newShortcut.EnvironmentVarStates.Add( "TTLIBPATH", true );

        m_newProject.Template.AddEntry( newShortcut );

        // Run Aux shortcut
        newShortcut = new TemplateShortcutEntry();
        newShortcut.Description = "Run Aux";
        newShortcut.Type = TemplateEntry.EntryType.Type_Shortcut;
        newShortcut.Filename = simExecPath;
        newShortcut.UiGroupName = "Run";

        newShortcut.Arguments.Add( "-networkname", "Aux" );
        newShortcut.ArgumentStates.Add( "-networkname", true );
        newShortcut.Arguments.Add( "-servername", "ALICE_SERVER" );
        newShortcut.ArgumentStates.Add( "-servername", true );
        newShortcut.Arguments.Add( "-serverport", "ALICE_SERVER_PORT" );
        newShortcut.ArgumentStates.Add( "-serverport", true );
        newShortcut.Arguments.Add( "-config", "\"" + m_masterConfigPath + "\"" );
        newShortcut.ArgumentStates.Add( "-config", true );
        newShortcut.Arguments.Add( "-devmode", "" );
        newShortcut.ArgumentStates.Add( "-devmode", true );
        newShortcut.Arguments.Add( "-nohardware", "" );
        newShortcut.ArgumentStates.Add( "-nohardware", true );
        newShortcut.Arguments.Add( "-devpath", "" );
        newShortcut.ArgumentStates.Add( "-devpath", true );

        newShortcut.EnvironmentVars.Add( "TTDEVPATH", m_sourcePathAbs + "..\\" );
        newShortcut.EnvironmentVarStates.Add( "TTDEVPATH", true );
        newShortcut.EnvironmentVars.Add( "TTLIBPATH", m_sourcePathAbs + "..\\Library\\" );
        newShortcut.EnvironmentVarStates.Add( "TTLIBPATH", true );

        m_newProject.Template.AddEntry( newShortcut );

        // Run Bench shortcut
        foreach( string initDataFilename in initDataLst.Items )
        {
          newShortcut = new TemplateShortcutEntry();
          newShortcut.Description = "Run Bench (" + initDataFilename.Remove( initDataFilename.LastIndexOf( '.' ) ) + ")";
          newShortcut.Type = TemplateEntry.EntryType.Type_Shortcut;
          newShortcut.Filename = info.m_benchExecutablePathAbs;
          newShortcut.UiGroupName = "Run";

          newShortcut.Arguments.Add( "-config", "./simbenchconfig.xml" );
          newShortcut.ArgumentStates.Add( "-config", true );
          newShortcut.Arguments.Add( "-devmode", "" );
          newShortcut.ArgumentStates.Add( "-devmode", true );
          newShortcut.Arguments.Add( "-devpath", "" );
          newShortcut.ArgumentStates.Add( "-devpath", true );
          newShortcut.Arguments.Add( "-initdata", m_initDataPathAbs + initDataFilename );
          newShortcut.ArgumentStates.Add( "-initdata", true );

          newShortcut.EnvironmentVars.Add( "TTDEVPATH", m_sourcePathAbs + "..\\" );
          newShortcut.EnvironmentVarStates.Add( "TTDEVPATH", true );
          newShortcut.EnvironmentVars.Add( "TTLIBPATH", m_sourcePathAbs + "..\\Library\\" );
          newShortcut.EnvironmentVarStates.Add( "TTLIBPATH", true );

          m_newProject.Template.AddEntry( newShortcut );
        }

        // Shortcut: Manager folder
        newShortcut = new TemplateShortcutEntry();
        newShortcut.Description = "Manager folder";
        newShortcut.Type = TemplateEntry.EntryType.Type_Shortcut;
        newShortcut.UiGroupName = "Shortcuts";

        FileInfo managerSolution = new FileInfo( info.m_managerSolutionPathAbs );
        newShortcut.Filename = managerSolution.DirectoryName + "\\PRJ_BUILD_PROFILE_MANAGER";

        m_newProject.Template.AddEntry( newShortcut );

        // Shortcut: Sim folder
        if( uiManagerOnly.Checked == false )
        {
          newShortcut = new TemplateShortcutEntry();
          newShortcut.Description = "Sim folder";
          newShortcut.Type = TemplateEntry.EntryType.Type_Shortcut;
          newShortcut.UiGroupName = "Shortcuts";

          FileInfo simSolution = new FileInfo( info.m_simSolutionPathAbs );
          newShortcut.Filename = simSolution.DirectoryName + "\\PRJ_BUILD_PROFILE_SIM";

          m_newProject.Template.AddEntry( newShortcut );
        }

        // Shortcut: Bench folder
        newShortcut = new TemplateShortcutEntry();
        newShortcut.Description = "Bench folder";
        newShortcut.Type = TemplateEntry.EntryType.Type_Shortcut;
        newShortcut.UiGroupName = "Shortcuts";

        FileInfo benchSolution = new FileInfo( info.m_benchSolutionPathAbs );
        newShortcut.Filename = benchSolution.DirectoryName + "\\PRJ_BUILD_PROFILE_BENCH";

        m_newProject.Template.AddEntry( newShortcut );

        // Shortcut: Project folder
        FileInfo masterConfigFileInfo = new FileInfo( m_masterConfigPath );

        newShortcut = new TemplateShortcutEntry();
        newShortcut.Description = "Project folder";
        newShortcut.Type = TemplateEntry.EntryType.Type_Shortcut;
        newShortcut.UiGroupName = "Shortcuts";
        newShortcut.Filename = masterConfigFileInfo.DirectoryName;

        m_newProject.Template.AddEntry( newShortcut );

        // Shortcut: Model folder
        if( modelFolderLbl.Text != "" )
        {
          newShortcut = new TemplateShortcutEntry();
          newShortcut.Description = "Model folder";
          newShortcut.Type = TemplateEntry.EntryType.Type_Shortcut;
          newShortcut.UiGroupName = "Shortcuts";
          newShortcut.Filename = modelFolderLbl.Text;

          m_newProject.Template.AddEntry( newShortcut );
        }

        // Shortcut: World folder
        if( worldFolderLbl.Text != "" )
        {
          newShortcut = new TemplateShortcutEntry();
          newShortcut.Description = "World folder";
          newShortcut.Type = TemplateEntry.EntryType.Type_Shortcut;
          newShortcut.UiGroupName = "Shortcuts";
          newShortcut.Filename = worldFolderLbl.Text;

          m_newProject.Template.AddEntry( newShortcut );
        }

        // Shortcut: Spec
        if( specLbl.Text != "" )
        {
          newShortcut = new TemplateShortcutEntry();
          newShortcut.Description = "Spec";
          newShortcut.Type = TemplateEntry.EntryType.Type_Shortcut;
          newShortcut.UiGroupName = "Shortcuts";
          newShortcut.Filename = specLbl.Text;

          m_newProject.Template.AddEntry( newShortcut );
        }

        // Shortcut: World Spec
        if( worldSpecLbl.Text != "" )
        {
          newShortcut = new TemplateShortcutEntry();
          newShortcut.Description = "World Spec";
          newShortcut.Type = TemplateEntry.EntryType.Type_Shortcut;
          newShortcut.UiGroupName = "Shortcuts";
          newShortcut.Filename = worldSpecLbl.Text;

          m_newProject.Template.AddEntry( newShortcut );
        }

        //-- Save.
        m_newProject.WriteToFile();
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );

        if( m_newProject != null )
        {
          Program.g_projectManager.DeleteProject( m_newProject );
        }

        return false;
      }

      return true;
    }

    //-------------------------------------------------------------------------

    public Project NewProject
    {
      get
      {
        return m_newProject;
      }
    }

    //-------------------------------------------------------------------------

    private void initDataAdd_Click( object sender, EventArgs e )
    {
      try
      {
        //-- Show the file selection dialog.
        OpenFileDialog dlg = new OpenFileDialog();
        dlg.CheckFileExists = true;
        dlg.Multiselect = true;
        dlg.InitialDirectory = m_initDataPathAbs;
        dlg.Filter = "InitData | *.initdata";
        dlg.ShowDialog();

        if( dlg.FileNames.Length > 0 )
        {
          foreach( string filename in dlg.FileNames )
          {
            FileInfo info = new FileInfo( filename );

            initDataLst.Items.Add( info.Name );
          }
        }
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void initDataRemove_Click( object sender, EventArgs e )
    {
      if( initDataLst.SelectedItem != null )
      {
        initDataLst.Items.Remove( initDataLst.SelectedItem );
      }
    }

    //-------------------------------------------------------------------------

    private void modelFolderBrowseBtn_Click( object sender, EventArgs e )
    {
      try
      {
        string selectedPath = MasterConfig.LibraryPath + "Objects\\";

        if( modelFolderLbl.Text != "" )
        {
          selectedPath = modelFolderLbl.Text;
        }

        //-- Show the file selection dialog.
        FolderBrowserDialog dlg = new FolderBrowserDialog();
        dlg.ShowNewFolderButton = false;
        dlg.Description = "Select the vehicle model folder";
        dlg.SelectedPath = selectedPath;
        dlg.RootFolder = Environment.SpecialFolder.MyComputer;

        if( dlg.ShowDialog( this ) == DialogResult.OK )
        {
          modelFolderLbl.Text = dlg.SelectedPath;
        }
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void worldFolderBrowse_Click( object sender, EventArgs e )
    {
      try
      {
        string selectedPath = MasterConfig.LibraryPath + "Terrain\\";

        if( worldFolderLbl.Text != "" )
        {
          selectedPath = worldFolderLbl.Text;
        }

        //-- Show the file selection dialog.
        FolderBrowserDialog dlg = new FolderBrowserDialog();
        dlg.ShowNewFolderButton = false;
        dlg.Description = "Select the world folder";
        dlg.SelectedPath = selectedPath;
        dlg.RootFolder = Environment.SpecialFolder.MyComputer;

        if( dlg.ShowDialog( this ) == DialogResult.OK )
        {
          worldFolderLbl.Text = dlg.SelectedPath;
        }
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void specBrowse_Click( object sender, EventArgs e )
    {
      try
      {
        string selectedPath = Program.g_driveLetter + ":\\dev\\main\\docs\\Specifications\\";

        if( specLbl.Text != "" )
        {
          selectedPath = specLbl.Text;
        }

        //-- Show the file selection dialog.
        OpenFileDialog dlg = new OpenFileDialog();
        dlg.CheckFileExists = true;
        dlg.Multiselect = false;
        dlg.InitialDirectory = selectedPath;
        dlg.Filter = "Word document | *.doc; *.docx";
        dlg.ShowDialog();

        if( dlg.FileName != "" )
        {
          specLbl.Text = dlg.FileName;
        }
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void sourceFolderTxt_Validating( object sender, System.ComponentModel.CancelEventArgs e )
    {
      if( sourceFolderTxt.Text.Length > 0 )
      {
        if( sourceFolderTxt.Text[ sourceFolderTxt.Text.Length - 1 ] != '\\' )
        {
          sourceFolderTxt.Text += '\\';
        }
      }
      else
      {
        sourceFolderTxt.Text = Program.g_driveLetter + ":\\dev\\main\\source\\";
      }

      LoadSettings();
    }

    //-------------------------------------------------------------------------

    private void worldSpecBrowse_Click( object sender, EventArgs e )
    {
      try
      {
        string selectedPath = Program.g_driveLetter + @":\dev\main\docs\Specifications\World Specs";

        if( worldSpecLbl.Text != "" )
        {
          selectedPath = worldSpecLbl.Text;
        }

        //-- Show the file selection dialog.
        OpenFileDialog dlg = new OpenFileDialog();
        dlg.CheckFileExists = true;
        dlg.Multiselect = false;
        dlg.InitialDirectory = selectedPath;
        dlg.Filter = "Word document | *.doc; *.docx";
        dlg.ShowDialog();

        if( dlg.FileName != "" )
        {
          worldSpecLbl.Text = dlg.FileName;
        }
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void uiManagerOnly_CheckedChanged( object sender, EventArgs e )
    {
      dfltManagerBuildCbo.Text = uiManagerOnly.Checked ? "FastDebug" : "Release";
    }

    //-------------------------------------------------------------------------
  }
}
