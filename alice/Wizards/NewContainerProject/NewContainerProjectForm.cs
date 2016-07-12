using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace alice
{
  public partial class NewContainerProjectForm : Form
  {
    //-------------------------------------------------------------------------

    const string c_exampleSourceFolder = @"Example: C:\dev\main\source\sim400\MineVehicles\GenADT\ADTs\SandvikEJC417";

    private string m_masterconfigPath;
    private Project m_project = null;
    private string m_devDeployFolder;

    private static string m_binderArtPath = Program.g_driveLetter + @":\dev\main\library\PROJECTS\Cybermine\";

    private static string m_dfltBinderArtCentreScreenFilename = "AboveGroundGeneric_CentreScreen_Haultruck_Komatsu960E.bmp";
    private static string m_dfltBinderArtLeftScreenFilename = "BelowGroundGeneric_LeftScreen.bmp";
    private static string m_dfltBinderArtRightScreenFilename = "BelowGroundGeneric_RightScreen.bmp";
    private static string m_dfltBinderArtAuxScreenFilename = "AboveGroundGeneric_AuxScreen.bmp";
    private static string m_dfltBinderArtLogonScreenFilename = "MainMenu_CM_Login_1920x1080.bmp";

    private string m_binderArtCentreScreenFilename = m_dfltBinderArtCentreScreenFilename;
    private string m_binderArtLeftScreenFilename = m_dfltBinderArtLeftScreenFilename;
    private string m_binderArtRightScreenFilename = m_dfltBinderArtRightScreenFilename;
    private string m_binderArtAuxScreenFilename = m_dfltBinderArtAuxScreenFilename;
    private string m_binderArtLogonScreenFilename = m_dfltBinderArtLogonScreenFilename;

    //-------------------------------------------------------------------------

    public NewContainerProjectForm()
    {
      InitializeComponent();
      PopulateProjectTypeComboBox();
      profilesCbo.Text = profilesCbo.Items[ 0 ] as string;
      sourceFolderTxt.Text = c_exampleSourceFolder;
      binderArtLst.SelectedIndex = 0;
    }

    //-------------------------------------------------------------------------

    void PopulateProjectTypeComboBox()
    {
      // The folder names give us our type names.
      string[] typeFolders =
        Directory.GetDirectories( Program.g_path + @"\res\GenerateContainerProject\" );

      foreach( string path in typeFolders )
      {
        string type = path.Substring( path.LastIndexOf( Path.DirectorySeparatorChar ) + 1 );
        typeCbo.Items.Add( type );
      }
    }

    //-------------------------------------------------------------------------

    private void masterconfigBrowse_Click( object sender, EventArgs e )
    {
      try
      {
        //-- Show the file selection dialog.
        OpenFileDialog dlg = new OpenFileDialog();
        dlg.CheckFileExists = true;
        dlg.Multiselect = false;
        dlg.InitialDirectory = @"C:\dev\main\source\SimProjects\CyberMine";
        dlg.Filter = "MasterConfig | *.masterconfig";
        dlg.ShowDialog();

        if( dlg.FileName != "" )
        {
          m_masterconfigPath = dlg.FileName;

          MasterConfig masterConfig = new MasterConfig( m_masterconfigPath );

          masterconfigTxt.Text = Path.GetFileNameWithoutExtension( m_masterconfigPath );
          projectFolderTxt.Text = Path.GetDirectoryName( m_masterconfigPath ) + @"\";

          nameTxt.Text = masterConfig.RootFolder;
          if( nameTxt.Text.EndsWith( @"\" ) )
          {
            nameTxt.Text = nameTxt.Text.Substring( 0, nameTxt.Text.Length - 1 );
          }
          nameTxt.Text = nameTxt.Text.Substring( nameTxt.Text.LastIndexOf( @"\" ) + 1 );

          deployedFolderTxt.Text = masterConfig.RootFolder;
          m_devDeployFolder = Program.g_driveLetter + @":\program files\thoroughtec\DEV_" + nameTxt.Text + @"\";
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

    private void createBtn_Click( object sender, EventArgs e )
    {
      //-- Validate input.
      if( typeCbo.Text == "" ||
          masterconfigTxt.Text == "" ||
          nameTxt.Text == "" ||
          sourceFolderTxt.Text == "" ||
          sourceFolderTxt.Text == c_exampleSourceFolder )
      {
        ErrorMsg( "Incomplete information!" );
        return;
      }

      //-- Create a Project.
      try
      {
        m_project = Program.g_projectManager.CreateProject( nameTxt.Text );
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
        return;
      }

      //-- Profiles?
      bool useTestProfileOnly = ( profilesCbo.Text == "TEST ONLY" );

      //-- Create profiles.
      TemplateCommonValueCollectionEntry newProfile = new TemplateCommonValueCollectionEntry();
      newProfile.Description = "TEST";
      newProfile.Values.Add( "PRJ_PROFILE", "TEST" );
      newProfile.Values.Add( "PRJ_DEVELOPER_MODE", "0" );
      newProfile.Values.Add( "PRJ_FOLDER_DEPOLOYED_PROJECT", deployedFolderTxt.Text );
      newProfile.Values.Add( "PRJ_MASTERCONFIG", deployedFolderTxt.Text + masterconfigTxt.Text + ".masterconfig" );
      newProfile.Values.Add( "PRJ_LAUNCH_BAT", projectFolderTxt.Text + "Launch.bat" );
      m_project.Template.AddEntry( newProfile );

      if( useTestProfileOnly == false )
      {
        newProfile = new TemplateCommonValueCollectionEntry();
        newProfile.Description = "DEV";
        newProfile.Values.Add( "PRJ_PROFILE", "DEV" );
        newProfile.Values.Add( "PRJ_DEVELOPER_MODE", "1" );
        newProfile.Values.Add( "PRJ_FOLDER_DEPOLOYED_PROJECT", m_devDeployFolder );
        newProfile.Values.Add( "PRJ_MASTERCONFIG", m_devDeployFolder + masterconfigTxt.Text + ".masterconfig" );
        newProfile.Values.Add( "PRJ_LAUNCH_BAT", projectFolderTxt.Text + "Launch.bat" );
        m_project.Template.AddEntry( newProfile );
      }

      //-- Create shortcuts.
      TemplateShortcutEntry newShortcut = new TemplateShortcutEntry();
      newShortcut.Description = "Build (PRJ_PROFILE)";
      newShortcut.Type = TemplateEntry.EntryType.Type_Shortcut;
      newShortcut.Filename = projectFolderTxt.Text + "Build.bat";
      newShortcut.UiGroupName = "Shortcuts";
      newShortcut.ConfirmBeforeRunning = true;
      newShortcut.EnvironmentVars.Add( "DEVELOPERMODE", "PRJ_DEVELOPER_MODE" );
      newShortcut.EnvironmentVarStates.Add( "DEVELOPERMODE", true );
      m_project.Template.AddEntry( newShortcut );

      newShortcut = new TemplateShortcutEntry();
      newShortcut.Description = "Gather (PRJ_PROFILE)";
      newShortcut.Type = TemplateEntry.EntryType.Type_Shortcut;
      newShortcut.Filename = projectFolderTxt.Text + "Gather.bat";
      newShortcut.UiGroupName = "Shortcuts";
      newShortcut.ConfirmBeforeRunning = true;
      newShortcut.EnvironmentVars.Add( "DEVELOPERMODE", "PRJ_DEVELOPER_MODE" );
      newShortcut.EnvironmentVarStates.Add( "DEVELOPERMODE", true );
      m_project.Template.AddEntry( newShortcut );

      newShortcut = new TemplateShortcutEntry();
      newShortcut.Description = "Deploy (PRJ_PROFILE)";
      newShortcut.Type = TemplateEntry.EntryType.Type_Shortcut;
      newShortcut.Filename = projectFolderTxt.Text + "Deploy.bat";
      newShortcut.UiGroupName = "Shortcuts";
      newShortcut.ConfirmBeforeRunning = false;
      newShortcut.EnvironmentVars.Add( "DEVELOPERMODE", "PRJ_DEVELOPER_MODE" );
      newShortcut.EnvironmentVarStates.Add( "DEVELOPERMODE", true );
      m_project.Template.AddEntry( newShortcut );

      newShortcut = new TemplateShortcutEntry();
      newShortcut.Description = "Deploy Binder Art";
      newShortcut.Type = TemplateEntry.EntryType.Type_Shortcut;
      newShortcut.Filename = Program.g_driveLetter + @":\dev\main\Source\Sim400\Bin\SimDeploy_BinderArt.bat";
      newShortcut.UiGroupName = "Shortcuts";
      newShortcut.ConfirmBeforeRunning = false;
      m_project.Template.AddEntry( newShortcut );

      newShortcut = new TemplateShortcutEntry();
      newShortcut.Description = "Launch (PRJ_PROFILE)";
      newShortcut.Type = TemplateEntry.EntryType.Type_Shortcut;
      newShortcut.Filename = @"ALICE_APP_PATH\res\DevMasterConfigLaunch.bat";
      newShortcut.UiGroupName = "Shortcuts";
      newShortcut.ConfirmBeforeRunning = false;
      newShortcut.EnvironmentVars.Add( "DEVELOPERMODE", "PRJ_DEVELOPER_MODE" );
      newShortcut.EnvironmentVarStates.Add( "DEVELOPERMODE", true );
      newShortcut.EnvironmentVars.Add( "MASTERCONFIGPATH", "PRJ_MASTERCONFIG" );
      newShortcut.EnvironmentVarStates.Add( "MASTERCONFIGPATH", true );
      newShortcut.EnvironmentVars.Add( "LAUNCHPATH", "PRJ_LAUNCH_BAT" );
      newShortcut.EnvironmentVarStates.Add( "LAUNCHPATH", true );
      m_project.Template.AddEntry( newShortcut );

      //-- Create folder shortcuts.
      newShortcut = new TemplateShortcutEntry();
      newShortcut.Description = "Project";
      newShortcut.Type = TemplateEntry.EntryType.Type_Shortcut;
      newShortcut.Filename = projectFolderTxt.Text;
      newShortcut.UiGroupName = "Folders";
      m_project.Template.AddEntry( newShortcut );

      newShortcut = new TemplateShortcutEntry();
      newShortcut.Description = "Deployed Project";
      newShortcut.Type = TemplateEntry.EntryType.Type_Shortcut;
      newShortcut.Filename = "PRJ_FOLDER_DEPOLOYED_PROJECT";
      newShortcut.UiGroupName = "Folders";
      m_project.Template.AddEntry( newShortcut );

      //-- Save.
      m_project.WriteToFile();

      //-- Copy the files to the project folder.
      try
      {
        // Build .bat file.
        string batFilenameSrc = Program.g_path + @"\res\GenerateContainerProject\" + typeCbo.Text + @"\Build.bat";
        string batFilenameDest = projectFolderTxt.Text + "Build.bat";

          // File already exists?
          if( File.Exists( batFilenameDest ) )
          {
            if( MessageBox.Show( "'" + batFilenameDest + "' already exists, replace?",
                                 "Replace existing file?",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question ) == DialogResult.Yes )
            {
              File.SetAttributes( batFilenameDest, File.GetAttributes( batFilenameDest ) & ~FileAttributes.ReadOnly );
              File.Delete( batFilenameDest );
            }
          }

          // Copy it.
          File.Copy( batFilenameSrc,
                     batFilenameDest,
                     true );

        // Gather .bat file.
        CreateGatherBatchFileFromTemplate( Program.g_path + @"\res\GenerateContainerProject\" + typeCbo.Text + @"\Gather.bat",
                                           projectFolderTxt.Text + "Gather.bat" );

        // Gather .deploy files.
        CreateGatherDeployFileFromTemplate( Program.g_path + @"\res\GenerateContainerProject\" + typeCbo.Text + @"\Gather.deploy",
                                            projectFolderTxt.Text + "Gather.deploy",
                                            false );

        if( useTestProfileOnly == false )
        {
          CreateGatherDeployFileFromTemplate( Program.g_path + @"\res\GenerateContainerProject\" + typeCbo.Text + @"\Gather.deploy",
                                              projectFolderTxt.Text + "DEV_Gather.deploy",
                                              true );
        }

        // Deploy .bat file.
        string newDeployFilename = projectFolderTxt.Text + "Deploy.bat";
        bool copyDeployFile = true;

          // File already exists?
          if( File.Exists( newDeployFilename ) )
          {
            if( MessageBox.Show( "'" + newDeployFilename + "' already exists, replace?",
                                 "Replace existing file?",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question ) == DialogResult.Yes )
            {
              File.SetAttributes( newDeployFilename, File.GetAttributes( newDeployFilename ) & ~FileAttributes.ReadOnly );
              File.Delete( newDeployFilename );
            }
            else
            {
              copyDeployFile = false;
            }
          }

        if( copyDeployFile )
        {
          File.Copy( Program.g_path + @"\res\GenerateContainerProject\" + typeCbo.Text + @"\Deploy.bat", newDeployFilename );
        }

        // SimDeploy.xml files.
        CreateSimDeployFileFromTemplate( Program.g_path + @"\res\GenerateContainerProject\" + typeCbo.Text + @"\SimDeploy.xml",
                                         projectFolderTxt.Text + "SimDeploy.xml",
                                         false );

        if( useTestProfileOnly == false )
        {
          CreateSimDeployFileFromTemplate( Program.g_path + @"\res\GenerateContainerProject\" + typeCbo.Text + @"\SimDeploy.xml",
                                           projectFolderTxt.Text + "DEV_SimDeploy.xml",
                                           true );
        }

        // Launch .bat file.
        CreateLaunchBatchFileFromTemplate( Program.g_path + @"\res\GenerateContainerProject\" + typeCbo.Text + @"\Launch.bat",
                                           projectFolderTxt.Text + "Launch.bat" );
      }
      catch( Exception ex )
      {
        ErrorMsg( "Error while copying batch files to the project folder:\n\n" + ex.Message );
      }

      //-- Done.
      Hide();
    }

    //-------------------------------------------------------------------------

    public Project NewProject
    {
      get
      {
        return m_project;
      }
    }

    //-------------------------------------------------------------------------

    private void cancelBtn_Click( object sender, EventArgs e )
    {
      m_project = null;
    }

    //-------------------------------------------------------------------------

    private void CreateGatherBatchFileFromTemplate( string templateFilename,
                                                    string filename )
    {
      // Read template.
      StreamReader stream = File.OpenText( templateFilename );
      string buf = stream.ReadToEnd();
      stream.Close();
      stream = null;

      // Replace specific text.
      string srcFolderName = sourceFolderTxt.Text;
      srcFolderName = srcFolderName.Substring( srcFolderName.LastIndexOf( Path.DirectorySeparatorChar ) + 1 );

      buf = buf.Replace( ">>>PROJECT_NAME<<<", nameTxt.Text );
      buf = buf.Replace( ">>>SOURCE_FOLDER_NAME<<<", srcFolderName );
      buf = buf.Replace( ">>>ABS_PROJECT_FOLDER<<<", projectFolderTxt.Text );
      buf = buf.Replace( ">>>ABS_DEPLOY_FOLDER<<<", deployedFolderTxt.Text );
      buf = buf.Replace( ">>>MASTER_CONFIG<<<", masterconfigTxt.Text + ".masterconfig" );
      buf = buf.Replace( ">>>BINDER_ART_CENTRE<<<", m_binderArtCentreScreenFilename );
      buf = buf.Replace( ">>>BINDER_ART_LEFT<<<", m_binderArtLeftScreenFilename );
      buf = buf.Replace( ">>>BINDER_ART_RIGHT<<<", m_binderArtRightScreenFilename );
      buf = buf.Replace( ">>>BINDER_ART_AUX<<<", m_binderArtAuxScreenFilename );
      buf = buf.Replace( ">>>BINDER_ART_LOGON<<<", m_binderArtLogonScreenFilename );

      // File already exists?
      if( File.Exists( filename ) )
      {
        if( MessageBox.Show( "'" + filename + "' already exists, replace?",
                             "Replace existing file?",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question ) == DialogResult.No )
        {
          return;
        }

        File.SetAttributes( filename, File.GetAttributes( filename ) & ~FileAttributes.ReadOnly );
        File.Delete( filename );
      }

      // Write file.
      File.WriteAllText( filename, buf );
    }

    //-------------------------------------------------------------------------

    private void CreateGatherDeployFileFromTemplate( string templateFilename,
                                                     string filename,
                                                     bool isDeveloperMode )
    {
      // Deployed folder.
      string deployFolder = ( isDeveloperMode ? m_devDeployFolder : deployedFolderTxt.Text );

      // Read template.
      StreamReader stream = File.OpenText( templateFilename );
      string buf = stream.ReadToEnd();
      stream.Close();
      stream = null;

      // Replace specific text.
      buf = buf.Replace( "!!!ABS_DEPLOY_FOLDER!!!", deployFolder );
      buf = buf.Replace( "!!!MASTERCONFIG!!!", masterconfigTxt.Text + ".masterconfig" );

      // File already exists?
      if( File.Exists( filename ) )
      {
        if( MessageBox.Show( "'" + filename + "' already exists, replace?",
                             "Replace existing file?",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question ) == DialogResult.No )
        {
          return;
        }

        File.SetAttributes( filename, File.GetAttributes( filename ) & ~FileAttributes.ReadOnly );
        File.Delete( filename );
      }

      // Write file.
      File.WriteAllText( filename, buf );
    }

    //-------------------------------------------------------------------------

    private void CreateSimDeployFileFromTemplate( string templateFilename,
                                                  string filename,
                                                  bool isDeveloperMode )
    {
      // Developer mode prefix.
      string prefix = ( isDeveloperMode ? "DEV_" : "" );

      // Read template.
      StreamReader stream = File.OpenText( templateFilename );
      string buf = stream.ReadToEnd();
      stream.Close();
      stream = null;

      // Replace specific text.
      buf = buf.Replace( "!!!PROJECT_NAME!!!", prefix + nameTxt.Text );

      // File already exists?
      if( File.Exists( filename ) )
      {
        if( MessageBox.Show( "'" + filename + "' already exists, replace?",
                             "Replace existing file?",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question ) == DialogResult.No )
        {
          return;
        }

        File.SetAttributes( filename, File.GetAttributes( filename ) & ~FileAttributes.ReadOnly );
        File.Delete( filename );
      }

      // Write file.
      File.WriteAllText( filename, buf );
    }

    //-------------------------------------------------------------------------

    private void CreateLaunchBatchFileFromTemplate( string templateFilename,
                                                    string filename )
    {
      // Read template.
      StreamReader stream = File.OpenText( templateFilename );
      string buf = stream.ReadToEnd();
      stream.Close();
      stream = null;

      // Replace specific text.
      buf = buf.Replace( ">>>PROJECT_NAME<<<", nameTxt.Text );
      buf = buf.Replace( ">>>MASTERCONFIG<<<", masterconfigTxt.Text + ".masterconfig" );

      // File already exists?
      if( File.Exists( filename ) )
      {
        if( MessageBox.Show( "'" + filename + "' already exists, replace?",
                             "Replace existing file?",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question ) == DialogResult.No )
        {
          return;
        }

        File.SetAttributes( filename, File.GetAttributes( filename ) & ~FileAttributes.ReadOnly );
        File.Delete( filename );
      }

      // Write file.
      File.WriteAllText( filename, buf );
    }

    //-------------------------------------------------------------------------

    private void browseSourceFolderBtn_Click( object sender, EventArgs e )
    {
      try
      {
        //-- Show the file selection dialog.
        FolderBrowserDialog dlg = new FolderBrowserDialog();
        dlg.SelectedPath = Program.g_driveLetter + @":\dev\main\source\sim400\MineVehicles\";
        dlg.Description = "Select the Vehicle's source path:";
        dlg.ShowNewFolderButton = false;
        dlg.ShowDialog( this );

        if( dlg.SelectedPath != "" )
        {
          sourceFolderTxt.Text = dlg.SelectedPath;
        }
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void browseBinderArt_Click( object sender, EventArgs e )
    {
      if( binderArtLst.SelectedIndex < 0 )
      {
        return;
      }

      try
      {
        //-- Show the file selection dialog.
        OpenFileDialog dlg = new OpenFileDialog();
        dlg.InitialDirectory = m_binderArtPath;
        dlg.Title = "Select the '" + binderArtLst.Text + "' image:";
        dlg.CheckFileExists = true;
        dlg.Multiselect = false;
        dlg.ShowDialog( this );

        if( dlg.SafeFileName != "" )
        {
          switch( binderArtLst.Text )
          {
            case "Centre":
              m_binderArtCentreScreenFilename = dlg.SafeFileName;
              break;

            case "Left":
              m_binderArtLeftScreenFilename = dlg.SafeFileName;
              break;

            case "Right":
              m_binderArtRightScreenFilename = dlg.SafeFileName;
              break;

            case "Aux":
              m_binderArtAuxScreenFilename = dlg.SafeFileName;
              break;

            case "Logon":
              m_binderArtLogonScreenFilename = dlg.SafeFileName;
              break;
          }

          binderArtFilenameTxt.Text = dlg.SafeFileName;

          binderArtLst_SelectedIndexChanged( null, null );
        }
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void binderArtLst_SelectedIndexChanged( object sender, EventArgs e )
    {
      switch( binderArtLst.Text )
      {
        case "Centre":
          binderArtFilenameTxt.Text = m_binderArtCentreScreenFilename;
          break;

        case "Left":
          binderArtFilenameTxt.Text = m_binderArtLeftScreenFilename;
          break;

        case "Right":
          binderArtFilenameTxt.Text = m_binderArtRightScreenFilename;
          break;

        case "Aux":
          binderArtFilenameTxt.Text = m_binderArtAuxScreenFilename;
          break;

        case "Logon":
          binderArtFilenameTxt.Text = m_binderArtLogonScreenFilename;
          break;
      }

      binderArtPic.ImageLocation = m_binderArtPath + binderArtFilenameTxt.Text;
    }

    //-------------------------------------------------------------------------
  }
}
