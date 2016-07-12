using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.Win32;

namespace alice
{
  public partial class mainForm : Form
  {
    //-------------------------------------------------------------------------

    private bool m_showArchivedProjects = false;

    //-------------------------------------------------------------------------

    public mainForm()
    {
      try
      {
        InitializeComponent();
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void mainForm_Load( object sender, EventArgs e )
    {
      try
      {
        RefreshProjectList();
        LoadSettingsFromRegistry();
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void mainForm_FormClosed( object sender, FormClosedEventArgs e )
    {
      try
      {
        SaveSettingsToRegistry();
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }
   
    //-------------------------------------------------------------------------

    private void LoadSettingsFromRegistry()
    {
      try
      {
        RegistryKey key = Registry.CurrentUser.OpenSubKey( "Software\\Alice" );

        if( key == null )
        {
          key = Registry.CurrentUser.CreateSubKey( "Software\\Alice" );
        }

        // window location
        Point point = new Point( (int)key.GetValue( "mainFormX", 10 ), (int)key.GetValue( "mainFormY", 10 ) );

        Rectangle virtualScreen = System.Windows.Forms.SystemInformation.VirtualScreen;

        if( point.X < virtualScreen.X ||
            point.X > virtualScreen.X + virtualScreen.Width ||
            point.Y < virtualScreen.Y ||
            point.Y > virtualScreen.Y + virtualScreen.Height - 100 )
        {
          point.X = 10;
          point.Y = 10;
        }

        Location = point;

        // active project
        string projectName = (string)key.GetValue( "activeProject", "" );

        if( projectName != "" )
        {
          foreach( Project prj in projectList.Items )
          {
            if( prj.Name == projectName )
            {
              projectList.SelectedItem = prj;
              break;
            }
          }
        }
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void SaveSettingsToRegistry()
    {
      try
      {
        RegistryKey key = Registry.CurrentUser.OpenSubKey( "Software\\Alice", true );

        if( key == null )
        {
          key = Registry.CurrentUser.CreateSubKey( "Software\\Alice" );
        }

        // window location
        key.SetValue( "mainFormX", Location.X, RegistryValueKind.DWord );
        key.SetValue( "mainFormY", Location.Y, RegistryValueKind.DWord );

        // active project
        Project prj = ( projectList.SelectedItem as Project );

        if( prj != null )
        {
          key.SetValue( "activeProject", prj.Name, RegistryValueKind.String );
        }
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void projectList_SelectedIndexChanged( object sender, EventArgs e )
    {
      try
      {
        RefreshProfileCbo();

        if( projectList.SelectedItem != null )
        {
          archivedProjectChkBox.CheckedChanged -= archivedProjectChkBox_CheckedChanged;
          archivedProjectChkBox.Checked = (projectList.SelectedItem as Project).IsArchived;
          archivedProjectChkBox.CheckedChanged += archivedProjectChkBox_CheckedChanged;
        }
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void newProjectBtn_Click( object sender, EventArgs e )
    {
      try
      {
        ProjectSetupForm form = new ProjectSetupForm( null );

        form.ShowDialog();

        if( form.DialogResult == DialogResult.OK )
        {
          try
          {
            Program.g_projectManager.CreateProject( form.ProjectName );

            RefreshProjectList();
          }
          catch( Exception ex )
          {
            ErrorMsg( ex.Message );
          }
        }

        form.Close();
        form.Dispose();
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

    private void copyProjectBtn_Click( object sender, EventArgs e )
    {
      try
      {
        ProjectSetupForm form = new ProjectSetupForm( null );

        form.ShowDialog();

        if( form.DialogResult == DialogResult.OK )
        {
          try
          {
            Program.g_projectManager.CopyProject(
              projectList.SelectedItem as Project,
              form.ProjectName );

            RefreshProjectList();
          }
          catch( Exception ex )
          {
            ErrorMsg( ex.Message );
          }
        }

        form.Close();
        form.Dispose();
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void deleteProjectBtn_Click( object sender, EventArgs e )
    {
      try
      {
        Project prj = ( projectList.SelectedItem as Project );

        DialogResult result =
          MessageBox.Show( "Delete project '" + prj.Name + "'?",
                           "Delete Project?",
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Warning,
                           MessageBoxDefaultButton.Button2 );

        if( result == DialogResult.Yes )
        {
          try
          {
            Program.g_projectManager.DeleteProject( prj );

            shortcutTree.Nodes.Clear();

            RefreshProjectList();
          }
          catch( Exception ex )
          {
            ErrorMsg( ex.Message );
          }
        }
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void RefreshProjectList()
    {
      try
      {
        projectList.DataSource = null;
        projectList.DataSource =
          Program.g_projectManager.GetFilteredProjects( filterTxtBox.Text,
                                                        !m_showArchivedProjects );

        RefreshShortcutTree();
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void RefreshShortcutTree()
    {
      try
      {
        if( projectList.SelectedItem != null )
        {
          Dictionary< string, TreeNode > groupNodes = new Dictionary< string, TreeNode >();

          // disable while updating to prevent flicker
          shortcutTree.Enabled = false;

          // remember what was selected
          string prevSelected = "";
          if( shortcutTree.SelectedNode != null )
          {
            prevSelected = shortcutTree.SelectedNode.Text;
          }

          // clear the tree
          shortcutTree.Nodes.Clear();

          // go through all shortcuts in this proj
          foreach( TemplateShortcutEntry entry in ( projectList.SelectedItem as Project ).Template.FileShortcuts )
          {
            // entry belongs to a group?
            if( entry.UiGroupName != "" )
            {
              // try find the group (it won't exist if this is the first member)
              TreeNode groupNode;

              if( groupNodes.TryGetValue( entry.UiGroupName, out groupNode ) == false )
              {
                // group wasn't found so create it and add to the treeview
                groupNode = new TreeNode( entry.UiGroupName );
                groupNodes.Add( entry.UiGroupName, groupNode );

                shortcutTree.Nodes.Add( groupNode );
              }

              // create the node for this shortcut and add to the group
              TreeNode newNode = new TreeNode( entry.ToString() );
              newNode.Tag = entry;

              groupNode.Nodes.Add( newNode );
            }
            else  // entry does not belong to a group, just add to the root
            {
              TreeNode newNode = new TreeNode( entry.ToString() );
              newNode.Tag = entry;

              shortcutTree.Nodes.Add( newNode );
            }
          }

          // show all nodes by default
          shortcutTree.ExpandAll();

          // try restore the prev selected shortcut
          if( prevSelected != "" )
          {
            foreach( TreeNode node in shortcutTree.Nodes )
            {
              shortcutTree.SelectedNode = FindShortcutTreeNode( node, prevSelected );

              if( shortcutTree.SelectedNode != null )
              {
                shortcutTree.Focus();
                break;
              }
            }
          }

          // enable after updating (to prevent flicker)
          shortcutTree.Enabled = true;
        }
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );

        shortcutTree.Enabled = true;  // is disabled during update
      }
    }

    //-------------------------------------------------------------------------

    private TreeNode FindShortcutTreeNode( TreeNode node, string text )
    {
      // is current node the one we're after?
      if( node.Text == text )
      {
        return node;
      }

      // test all child nodes recursively
      foreach( TreeNode childNode in node.Nodes )
      {
        if( childNode.Text == text )
        {
          return childNode;
        }

        TreeNode foundNode = FindShortcutTreeNode( childNode, text );

        if( foundNode != null )
        {
          return foundNode;
        }
      }

      // node not found in this node or its children
      return null;
    }

    //-------------------------------------------------------------------------

    private void RefreshProfileCbo()
    {
      try
      {
        if( projectList.SelectedItem != null )
        {
          Project prj = ( projectList.SelectedItem as Project );

          RefreshShortcutTree();

          profileCbo.DataSource = prj.Template.CommonValueCollections;
        }
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void newShortcutBtn_Click( object sender, EventArgs e )
    {
      try
      {
        Project prj = ( projectList.SelectedItem as Project );

        // do the new template shortcut dialog
        TemplateShortcutEntry newEntry = new TemplateShortcutEntry();

        TemplateShortcutSetupForm form =
          new TemplateShortcutSetupForm( prj, false, newEntry );

        bool showDlg = true;

        while( showDlg )    // keep showing the dialog until user input is ok
        {
          form.ShowDialog();

          if( form.DialogResult == DialogResult.OK )
          {
            // check name doesn't already exist
            showDlg = false;

            foreach( TemplateEntry entry in prj.Template.FileShortcuts )
            {
              if( entry.Description.ToLower() == form.Description.ToLower() )
              {
                showDlg = true;

                ErrorMsg( "Name '" + entry.Description + "' already exists." );
              }
            }
          }
          else  // user cancelled the dlg
          {
            return;
          }
        }

        // update the project
        newEntry.Project = form.Project;
        newEntry.Description = form.Description;
        newEntry.Filename = form.Filename;
        newEntry.UiGroupName = form.Group;
        newEntry.Arguments = new Dictionary< string, string >( form.Arguments );
        newEntry.ArgumentStates = new Dictionary< string,bool >( form.ArgumentStates );
        newEntry.EnvironmentVars = new Dictionary< string, string >( form.EnvironmentVars );
        newEntry.EnvironmentVarStates = new Dictionary< string, bool >( form.EnvironmentVarStates );
        newEntry.LinkedShortcuts = new List< string >( form.LinkedShortcuts );
        newEntry.ConfirmBeforeRunning = form.ConfirmBeforeRunning;
        newEntry.ScheduledRunEnabled = form.ScheduledRunEnabled;
        newEntry.NextScheduledRunTime = form.ScheduledRunTime;

        prj.Template.AddEntry( newEntry );
        prj.WriteToFile();

        form.Close();
        form.Dispose();

        RefreshShortcutTree();
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void editShortcutBtn_Click( object sender, EventArgs e )
    {
      try
      {
        if( shortcutTree.SelectedNode == null )
        {
          return;
        }

        // get the project
        Project prj = ( projectList.SelectedItem as Project );

        // do the new template shortcut dialog
        TemplateShortcutEntry editEntry = ( shortcutTree.SelectedNode.Tag as TemplateShortcutEntry );

        if( editEntry == null )
        {
          return;
        }

        TemplateShortcutSetupForm form = new TemplateShortcutSetupForm( prj, false, editEntry );

        bool showDlg = true;

        while( showDlg )    // keep showing the dialog until user input is ok
        {
          form.ShowDialog();

          if( form.DialogResult == DialogResult.OK )
          {
            // check name doesn't already exist
            showDlg = false;

            foreach( TemplateEntry entry in prj.Template.FileShortcuts )
            {
              if( entry == editEntry )    // don't test entry we're editing
              {
                continue;
              }

              if( entry.Description.ToLower() == form.Description.ToLower() )
              {
                showDlg = true;

                ErrorMsg( "Name '" + entry.Description + "' already exists." );
              }
            }
          }
          else  // user cancelled the dlg
          {
            return;
          }
        }

        // update the project
        editEntry.Project = form.Project;
        editEntry.Description = form.Description;
        editEntry.Filename = form.Filename;
        editEntry.UiGroupName = form.Group;
        editEntry.Arguments = new Dictionary< string, string >( form.Arguments );
        editEntry.ArgumentStates = new Dictionary< string,bool >( form.ArgumentStates );
        editEntry.EnvironmentVars = new Dictionary< string, string >( form.EnvironmentVars );
        editEntry.EnvironmentVarStates = new Dictionary< string, bool >( form.EnvironmentVarStates );
        editEntry.LinkedShortcuts = new List< string >( form.LinkedShortcuts );
        editEntry.ConfirmBeforeRunning = form.ConfirmBeforeRunning;
        editEntry.ScheduledRunEnabled = form.ScheduledRunEnabled;
        editEntry.NextScheduledRunTime = form.ScheduledRunTime;

        prj.WriteToFile();

        form.Close();
        form.Dispose();

        RefreshShortcutTree();
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void copyShortcutBtn_Click( object sender, EventArgs e )
    {
      try
      {
        if( shortcutTree.SelectedNode == null )
        {
          return;
        }

        // get the project
        Project prj = ( projectList.SelectedItem as Project );

        // do the new template shortcut dialog
        TemplateShortcutEntry copy = ( shortcutTree.SelectedNode.Tag as TemplateShortcutEntry ).CreateCopy();

        TemplateShortcutSetupForm form = new TemplateShortcutSetupForm( prj, true, copy );

        bool showDlg = true;

        while( showDlg )    // keep showing the dialog until user input is ok
        {
          form.ShowDialog();

          if( form.DialogResult == DialogResult.OK )
          {
            // may have changed if user is copying to a different project
            prj = form.Project;

            // check name doesn't already exist
            showDlg = false;

            foreach( TemplateEntry entry in prj.Template.FileShortcuts )
            {
              if( entry.Description.ToLower() == form.Description.ToLower() )
              {
                showDlg = true;

                ErrorMsg( "Name '" + entry.Description + "' already exists." );
              }
            }
          }
          else  // user cancelled the dlg
          {
            return;
          }
        }

        // update the project
        copy.Project = form.Project;
        copy.Description = form.Description;
        copy.Filename = form.Filename;
        copy.UiGroupName = form.Group;
        copy.Arguments = new Dictionary< string, string >( form.Arguments );
        copy.ArgumentStates = new Dictionary< string,bool >( form.ArgumentStates );
        copy.EnvironmentVars = new Dictionary< string, string >( form.EnvironmentVars );
        copy.EnvironmentVarStates = new Dictionary< string, bool >( form.EnvironmentVarStates );
        copy.LinkedShortcuts = new List< string >( form.LinkedShortcuts );
        copy.ConfirmBeforeRunning = form.ConfirmBeforeRunning;
        copy.ScheduledRunEnabled = form.ScheduledRunEnabled;
        copy.NextScheduledRunTime = form.ScheduledRunTime;

        prj = form.Project;

        prj.Template.AddEntry( copy );
        prj.WriteToFile();

        form.Close();
        form.Dispose();

        RefreshShortcutTree();
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void deleteShortcutBtn_Click( object sender, EventArgs e )
    {
      try
      {
        if( shortcutTree.SelectedNode == null )
        {
          return;
        }

        if( projectList.SelectedItem != null )
        {
          TemplateShortcutEntry entry = ( shortcutTree.SelectedNode.Tag as TemplateShortcutEntry );

          if( entry == null )
          {
            return;
          }

          if( MessageBox.Show( "Delete shortcut '" + entry.Description + "'?",
                               "Delete Shortcut?",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Exclamation,
                               MessageBoxDefaultButton.Button2 ) == DialogResult.Yes )
          {
            Project prj = ( projectList.SelectedItem as Project );

            prj.Template.DeleteEntry( entry );
            prj.WriteToFile();

            RefreshShortcutTree();
          }
        }
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void globalCommonValueMenuItem_Click( object sender, EventArgs e )
    {
      try
      {
        PairedValueSetupForm form =
          new PairedValueSetupForm( Program.g_projectManager.CommonValues,
                                    Program.g_projectManager.ReadOnlyCommonValues,
                                    "ALICE_" );

        form.Text = "Global Common Values Setup";
        form.ShowDialog();

        if( form.DialogResult == DialogResult.OK )
        {
          Program.g_projectManager.CommonValues = form.PairedValues;
        }

        form.Close();
        form.Dispose();
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void projectCommonValueMenuItem_Click( object sender, EventArgs e )
    {
      try
      {
        Project prj = ( projectList.SelectedItem as Project );

        if( prj != null )
        {
          PairedValueSetupForm form =
            new PairedValueSetupForm( prj.CommonValues,
                                      new List< string >(),
                                      "PRJ_" );

          form.Text = "Project '" + prj.Name + "' Common Values Setup";
          form.ShowDialog();

          if( form.DialogResult == DialogResult.OK )
          {
            prj.CommonValues = form.PairedValues;
            prj.WriteToFile();
          }

          form.Close();
          form.Dispose();
        }
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void moveShortcutUpBtn_Click( object sender, EventArgs e )
    {
      try
      {
        if( shortcutTree.SelectedNode == null )
        {
          return;
        }

        Project prj = ( projectList.SelectedItem as Project );
        TemplateEntry entry1 = ( shortcutTree.SelectedNode.Tag as TemplateEntry );

        if( prj == null || entry1 == null )
        {
          return;
        }

        if( shortcutTree.SelectedNode.PrevNode == null )
        {
          return;
        }

        TemplateEntry entry2 = ( shortcutTree.SelectedNode.PrevNode.Tag as TemplateEntry );

        if( entry2 == null )
        {
          return;
        }

        prj.Template.SwapEntries( entry1, entry2 );

        RefreshShortcutTree();

        prj.WriteToFile();
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void moveShortcutDownBtn_Click( object sender, EventArgs e )
    {
      try
      {
        if( shortcutTree.SelectedNode == null )
        {
          return;
        }

        Project prj = ( projectList.SelectedItem as Project );
        TemplateEntry entry1 = ( shortcutTree.SelectedNode.Tag as TemplateEntry );

        if( prj == null || entry1 == null )
        {
          return;
        }

        if( shortcutTree.SelectedNode.NextNode == null )
        {
          return;
        }

        TemplateEntry entry2 = ( shortcutTree.SelectedNode.NextNode.Tag as TemplateEntry );

        if( entry2 == null )
        {
          return;
        }

        prj.Template.SwapEntries( entry1, entry2 );

        RefreshShortcutTree();

        prj.WriteToFile();
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void profileCbo_SelectedIndexChanged( object sender, EventArgs e )
    {
      try
      {
        Project prj = ( projectList.SelectedItem as Project );

        prj.ActiveCommonValueCollection =
          ( profileCbo.SelectedItem as TemplateCommonValueCollectionEntry );

        foreach( TemplateShortcutEntry entry in prj.Template.FileShortcuts )
        {
          entry.Project = prj;
        }

        RefreshShortcutTree();
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void manageProfilesBtn_Click( object sender, EventArgs e )
    {
      try
      {
        TemplateCommonValueCollectionEntry selectedProfile =
          ( profileCbo.SelectedItem as TemplateCommonValueCollectionEntry );

        ProjectProfileManagementForm form =
          new ProjectProfileManagementForm( projectList.SelectedItem as Project );

        form.ShowDialog();

        RefreshProfileCbo();

        // re-select the profile that was selected
        if( profileCbo.Items.Contains( selectedProfile ) )
        {
          profileCbo.SelectedItem = selectedProfile;
        }
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void shortcutTree_NodeMouseClick( object sender, TreeNodeMouseClickEventArgs e )
    {
      // right-click: edit shortcut
      if( e.Button == MouseButtons.Right )
      {
        shortcutTree.SelectedNode = e.Node;
        editShortcutBtn_Click( null, null );
      }
    }

    //-------------------------------------------------------------------------

    private void shortcutTree_NodeMouseDoubleClick( object sender, TreeNodeMouseClickEventArgs e )
    {
      try
      {
        Project prj = ( projectList.SelectedItem as Project );

        if( prj != null )
        {
          TemplateEntry entry = ( shortcutTree.SelectedNode.Tag as TemplateEntry );

          if( entry != null )
          {
            entry.PerformAction( prj, DateTime.Now.GetHashCode() );
          }
        }
      }
      catch( Exception ex )
      {
        ErrorMsg( "An error occured while running this shortcut:" +
                    Environment.NewLine + Environment.NewLine + ex.Message +
                    Environment.NewLine + Environment.NewLine + ex.StackTrace );
      }
    }

    //-------------------------------------------------------------------------

    private void newProjectWizardToolStripMenuItem_Click( object sender, EventArgs e )
    {
      NewProjectWizardForm form = new NewProjectWizardForm();
      form.ShowDialog();

      if( form.NewProject != null )
      {
        RefreshProjectList();

        projectList.SelectedItem = form.NewProject;
      }

      form.Dispose();
    }

    //-------------------------------------------------------------------------

    private void filterTxtBox_TextChanged( object sender, EventArgs e )
    {
      Project tmp = projectList.SelectedItem as Project;

      RefreshProjectList();

      if( tmp != null &&
          projectList.Items.Contains( tmp ) )
      {
        projectList.SelectedItem = tmp;
      }
    }

    //-------------------------------------------------------------------------

    private void clearFilterBtn_Click( object sender, EventArgs e )
    {
      filterTxtBox.Text = "";
      filterTxtBox.Focus();
    }

    //-------------------------------------------------------------------------

    private void mainForm_KeyPress( object sender, KeyPressEventArgs e )
    {
      if( filterTxtBox.Focused )
      {
        return;
      }

      char c = e.KeyChar;

      if( ( c >= 'a' && c <= 'z' ) ||
          ( c >= 'A' && c <= 'Z' ) ||
          ( c >= '0' && c <= '9' ) ||
          c == ' ' )
      {
        filterTxtBox.Text += c;
      }

      if( c ==  '\b' &&    // backspace
          filterTxtBox.Text.Length > 0 )
      {
        filterTxtBox.Text = filterTxtBox.Text.Substring( 0, filterTxtBox.Text.Length - 1 );
      }

      filterTxtBox.Focus();
      filterTxtBox.SelectionLength = 0;
      filterTxtBox.SelectionStart = filterTxtBox.Text.Length;
    }

    //-------------------------------------------------------------------------

    private void filterTxtBox_KeyPress( object sender, KeyPressEventArgs e )
    {
      if( e.KeyChar == '\r' &&    // return
          projectList.Items.Count > 0 )
      {
        filterTxtBox.Text = "";
      }
    }

    //-------------------------------------------------------------------------

    private void showProjectsBtn_Click( object sender, EventArgs e )
    {
      m_showArchivedProjects = !m_showArchivedProjects;

      showProjectsBtn.Text = ( m_showArchivedProjects ? "Hide Archived" : "Show All" );

      Project prj = projectList.SelectedItem as Project;

      RefreshProjectList();

      if( prj != null &&
          projectList.Items.Contains( prj ) )
      {
        projectList.SelectedItem = prj;
      }
    }

    //-------------------------------------------------------------------------

    private void archivedProjectChkBox_CheckedChanged( object sender, EventArgs e )
    {
      try
      {
        if( projectList.SelectedItem != null )
        {
          Project prj = projectList.SelectedItem as Project;
          prj.IsArchived = archivedProjectChkBox.Checked;
          prj.WriteToFile();

          RefreshProjectList();
        }
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
   }

    //-------------------------------------------------------------------------

    private void newContainerProjectToolStripMenuItem_Click( object sender, EventArgs e )
    {
      try
      {
        NewContainerProjectForm form = new NewContainerProjectForm();
        form.ShowDialog( this );
 
        if( form.NewProject != null )
        {
          RefreshProjectList();

          projectList.SelectedItem = form.NewProject;
        }

        form.Dispose();
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void scheduledTasksToolStripMenuItem1_Click( object sender, EventArgs e )
    {
      ScheduledTasks dlg = new ScheduledTasks();
      dlg.ShowDialog( this );
      dlg = null;
    }

    //-------------------------------------------------------------------------
  }
}