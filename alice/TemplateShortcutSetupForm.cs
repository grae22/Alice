using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace alice
{
  public partial class TemplateShortcutSetupForm : Form
  {
    private Dictionary< string, string > m_args;
    private Dictionary< string, bool > m_argStates;
    private Dictionary< string, string > m_vars;
    private Dictionary< string, bool > m_varStates;

    //-------------------------------------------------------------------------

    public TemplateShortcutSetupForm( Project project,
                                      bool canChangeProject,
                                      TemplateShortcutEntry entry )
    {
      try
      {
        InitializeComponent();

        projectCbo.Enabled = canChangeProject;

        m_args = new Dictionary< string, string >( entry.Arguments );
        m_argStates = new Dictionary< string, bool >( entry.ArgumentStates );
        m_vars = new Dictionary< string, string >( entry.EnvironmentVars );
        m_varStates = new Dictionary< string, bool >( entry.EnvironmentVarStates );

        shortcutNameTxt.Text = entry.Description;
        groupCbo.Text = entry.UiGroupName;
        fullFilenameTxt.Text = entry.Filename;
        confirmBeforeRunningChkBox.Checked = entry.ConfirmBeforeRunning;
        scheduledRunChkBox.Checked = entry.ScheduledRunEnabled;

        try
        {
          scheduledRunTimePicker.Value = entry.NextScheduledRunTime;
        }
        catch
        {
          // do nothing
        }

        PopulateProjects( project );
        PopulateGroups( project );
        PopulateCommonValues();
        PopulateLinkedShortcutsLists( project, entry );

        RefreshArgsList();
        RefreshVarsList();
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void PopulateProjects( Project selectedProject )
    {
      try
      {
        foreach( Project prj in Program.g_projectManager.Projects )
        {
          projectCbo.Items.Add( prj );
        }

        projectCbo.SelectedItem = selectedProject;
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void PopulateGroups( Project prj )
    {
      try
      {
        foreach( string s in prj.UiGroups )
        {
          groupCbo.Items.Add( s );
        }
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void PopulateCommonValues()
    {
      try
      {
        foreach( string key in ( projectCbo.SelectedItem as Project ).CommonValues.Keys )
        {
          fileCbo.Items.Add( key );
          argValueCbo.Items.Add( key );
          varValueCbo.Items.Add( key );
        }

        foreach( string key in Program.g_projectManager.CommonValues.Keys )
        {
          fileCbo.Items.Add( key );
          argValueCbo.Items.Add( key );
          varValueCbo.Items.Add( key );
        }
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void PopulateLinkedShortcutsLists( Project project,
                                               TemplateShortcutEntry entry )
    {
      // linked shortcuts
      foreach( string desc in entry.LinkedShortcuts )
      {
        TemplateShortcutEntry tmpEntry = project.Template.GetEntryWithDescription( desc ) as TemplateShortcutEntry;

        if( tmpEntry == entry || tmpEntry == null )
        {
          continue;
        }

        linkedShortcutList.Items.Add( tmpEntry );
      }

      // unlinked shortcuts
      foreach( TemplateShortcutEntry tmpEntry in project.Template.FileShortcuts )
      {
        if( tmpEntry == entry )
        {
          continue;
        }

        if( linkedShortcutList.Items.Contains( tmpEntry ) == false )
        {
          shortcutsList.Items.Add( tmpEntry );
        }
      }
    }

    //-------------------------------------------------------------------------

    private void fileBrowseBtn_Click( object sender, EventArgs e )
    {
      try
      {
        // show the 'open file' dialog
        OpenFileDialog dlg = new OpenFileDialog();
        dlg.CheckFileExists = true;
        dlg.Multiselect = false;
        dlg.ShowDialog();

        if( dlg.FileName != null )
        {
          fullFilenameTxt.Text = dlg.FileName;
        }
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void addArgBtn_Click( object sender, EventArgs e )
    {
      try
      {
        // validate name
        if( argNameTxt.Text == "" )
        {
          MessageBox.Show( "Enter an argument name.",
                           "Missing Argument Name",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Exclamation );
          return;
        }

        // already exists in list? remove it
        if( m_args.ContainsKey( argNameTxt.Text ) )
        {
          m_args.Remove( argNameTxt.Text );
          m_argStates.Remove( argNameTxt.Text );
        }

        // add to the list
        m_args.Add( argNameTxt.Text, argValueCbo.Text );
        m_argStates.Add( argNameTxt.Text, true );

        RefreshArgsList();
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void removeArgBtn_Click( object sender, EventArgs e )
    {
      try
      {
        if( argList.SelectedItem != null )
        {
          string key = ( argList.SelectedItem as string );

          key = ExtractKeyFromCombinedKeyValueString( key );

          m_args.Remove( key );
          m_argStates.Remove( key );

          RefreshArgsList();
        }
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void RefreshArgsList()
    {
      try
      {
        argList.Items.Clear();

        foreach( string key in m_args.Keys )
        {
          bool isArgEnabled;
          m_argStates.TryGetValue( key, out isArgEnabled );
          
          string value;
          if( m_args.TryGetValue( key, out value ) )
          {
            if( value != "" )
            {
              argList.Items.Add( key + " -> " + value, isArgEnabled );
            }
            else
            {
              argList.Items.Add( key, isArgEnabled );
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

    private void addVarBtn_Click( object sender, EventArgs e )
    {
      try
      {
        // validate name
        if( varNameTxt.Text == "" )
        {
          MessageBox.Show( "Enter an environment var name.",
                           "Missing Environment Var Name",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Exclamation );
          return;
        }

        // validate value
        if( varValueCbo.Text == "" )
        {
          MessageBox.Show( "Enter an environment var value.",
                           "Missing Environment Var Value",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Exclamation );
          return;
        }

        // convert to caps
        varNameTxt.Text = varNameTxt.Text.ToUpper();

        // already exists in list? remove it
        if( m_vars.ContainsKey( varNameTxt.Text ) )
        {
          m_vars.Remove( varNameTxt.Text );
          m_varStates.Remove( varNameTxt.Text );
        }

        // add to the list
        m_vars.Add( varNameTxt.Text, varValueCbo.Text );
        m_varStates.Add( varNameTxt.Text, true );

        RefreshVarsList();
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void removeVarBtn_Click( object sender, EventArgs e )
    {
      try
      {
        if( varList.SelectedItem != null )
        {
          string key = ( varList.SelectedItem as string );

          key = ExtractKeyFromCombinedKeyValueString( key );

          m_vars.Remove( key );
          m_varStates.Remove( key );

          RefreshVarsList();
        }
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void RefreshVarsList()
    {
      try
      {
        varList.Items.Clear();

        foreach( string key in m_vars.Keys )
        {
          bool isVarEnabled;
          m_varStates.TryGetValue( key, out isVarEnabled );
          
          string value;
          if( m_vars.TryGetValue( key, out value ) )
          {
            varList.Items.Add( key + " -> " + value, isVarEnabled );
          }
        }
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private string ExtractKeyFromCombinedKeyValueString( string s )
    {
      try
      {
        string key = s;

        int i = key.IndexOf( " -> " );

        if( i > 0 ) // not every arg has a value
        {
          key = key.Substring( 0, i );
        }

        return key;
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );

        return "";
      }
    }

    //-------------------------------------------------------------------------

    private void okBtn_Click( object sender, EventArgs e )
    {
      try
      {
        // validate name
        if( shortcutNameTxt.Text == "" )
        {
          MessageBox.Show( "Enter an name for the shortcut.",
                           "Missing Shortcut Name",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Exclamation );
          return;
        }

        // validate file AND/OR linked shortcuts
        if( fullFilenameTxt.Text == "" && linkedShortcutList.Items.Count == 0 )
        {
          MessageBox.Show( "Select a file for the shortcut OR add linked-shortcuts.",
                           "Incomplete Shortcut Setup",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Exclamation );
          return;
        }

        // success
        UpdateArgEnabledStates();
        UpdateVarEnabledStates();

        DialogResult = DialogResult.OK;
        Hide();
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void viewArgStringBtn_Click( object sender, EventArgs e )
    {
      try
      {
        UpdateArgEnabledStates();

        Project prj = ( projectCbo.SelectedItem as Project );

        string filePath = prj.GetCommonValue( fullFilenameTxt.Text, false );

        string argStr = TemplateShortcutEntry.GetArgumentString( m_args, m_argStates, prj );

        MessageBox.Show( filePath + " " + argStr,
                         "Argument String",
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Information );
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void copyToClipboardArgStringBtn_Click( object sender, EventArgs e )
    {
      try
      {
        UpdateArgEnabledStates();

        Project prj = ( projectCbo.SelectedItem as Project );

        string filePath = prj.GetCommonValue( fullFilenameTxt.Text, false );

        string argStr = TemplateShortcutEntry.GetArgumentString( m_args, m_argStates, prj );

        Clipboard.SetText( filePath + " " + argStr );
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void viewVarStringBtn_Click( object sender, EventArgs e )
    {
      try
      {
        Project prj = ( projectCbo.SelectedItem as Project );

        string varStr = "";

        foreach( string key in m_vars.Keys )
        {
          bool enabled;
          if( m_varStates.TryGetValue( key, out enabled ) &&
              enabled )
          {
            string value;
            if( m_vars.TryGetValue( key, out value ) )
            {
              value = prj.GetCommonValue( value, true );

              varStr += key + " -> " + value + Environment.NewLine;
            }
          }
        }

        MessageBox.Show( varStr,
                         "Environment Variables",
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Information );
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void argList_SelectedIndexChanged( object sender, EventArgs e )
    {
      try
      {
        if( argList.SelectedItem != null )
        {
          string key = ( argList.SelectedItem as string );
          string value = "";

          key = ExtractKeyFromCombinedKeyValueString( key );
          m_args.TryGetValue( key, out value );

          argValueCbo.SelectedIndexChanged -= argValueCbo_SelectedIndexChanged;

          argNameTxt.Text = key;
          argValueCbo.Text = value;

          argValueCbo.SelectedIndexChanged += argValueCbo_SelectedIndexChanged;
        }
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void varList_SelectedIndexChanged( object sender, EventArgs e )
    {
      try
      {
        if( varList.SelectedItem != null )
        {
          string key = ( varList.SelectedItem as string );
          string value = "";

          key = ExtractKeyFromCombinedKeyValueString( key );
          m_vars.TryGetValue( key, out value );

          varValueCbo.SelectedIndexChanged -= varValueCbo_SelectedIndexChanged;

          varNameTxt.Text = key;
          varValueCbo.Text = value;

          varValueCbo.SelectedIndexChanged += varValueCbo_SelectedIndexChanged;
        }
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    public string Description
    {
      get
      {
        return shortcutNameTxt.Text;
      }
    }

    //-------------------------------------------------------------------------

    public string Filename
    {
      get
      {
        return fullFilenameTxt.Text;
      }
    }

    //-------------------------------------------------------------------------

    public Dictionary< string, string > Arguments
    {
      get
      {
        return m_args;
      }
    }

    //-------------------------------------------------------------------------

    public Dictionary< string, string > EnvironmentVars
    {
      get
      {
        return m_vars;
      }
    }

    //-------------------------------------------------------------------------

    public Dictionary< string, bool > ArgumentStates
    {
      get
      {
        return m_argStates;
      }
    }

    //-------------------------------------------------------------------------

    public Dictionary< string, bool > EnvironmentVarStates
    {
      get
      {
        return m_varStates;
      }
    }

    //-------------------------------------------------------------------------

    private void argList_ItemCheck( object sender, ItemCheckEventArgs e )
    {

    }

    //-------------------------------------------------------------------------

    private void UpdateArgEnabledStates()
    {
      try
      {
        m_argStates.Clear();

        for( int i = 0; i < argList.Items.Count; i++ )
        {
          string key = ( argList.Items[ i ] as string );

          key = ExtractKeyFromCombinedKeyValueString( key );

          bool isChecked = argList.CheckedItems.Contains( argList.Items[ i ] );
          
          m_argStates.Add( key, isChecked );
        }
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void UpdateVarEnabledStates()
    {
      try
      {
        m_varStates.Clear();

        for( int i = 0; i < varList.Items.Count; i++ )
        {
          string key = ( varList.Items[ i ] as string );

          key = ExtractKeyFromCombinedKeyValueString( key );

          bool isChecked = varList.CheckedItems.Contains( varList.Items[ i ] );
          
          m_varStates.Add( key, isChecked );
        }
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void fileCbo_SelectedIndexChanged( object sender, EventArgs e )
    {
      fullFilenameTxt.Text = fileCbo.Text;
    }

    //-------------------------------------------------------------------------

    private void fileCbo_KeyPress( object sender, KeyPressEventArgs e )
    {
      if( e.KeyChar == 13 )
      {
        fullFilenameTxt.Text = fileCbo.Text;
      }
    }

    //-------------------------------------------------------------------------

    private void argValueCbo_SelectedIndexChanged( object sender, EventArgs e )
    {
      addArgBtn_Click( null, null );
    }

    //-------------------------------------------------------------------------

    private void argNameTxt_KeyPress( object sender, KeyPressEventArgs e )
    {
      if( e.KeyChar == 13 )
      {
        addArgBtn_Click( null, null );
      }
    }

    //-------------------------------------------------------------------------

    private void argValueCbo_KeyPress( object sender, KeyPressEventArgs e )
    {
      if( e.KeyChar == 13 )
      {
        addArgBtn_Click( null, null );
      }
    }

    //-------------------------------------------------------------------------

    private void varValueCbo_SelectedIndexChanged( object sender, EventArgs e )
    {
      addVarBtn_Click( null, null );
    }

    //-------------------------------------------------------------------------

    private void varValueCbo_KeyPress( object sender, KeyPressEventArgs e )
    {
      if( e.KeyChar == 13 )
      {
        addVarBtn_Click( null, null );
      }
    }

    //-------------------------------------------------------------------------

    public Project Project
    {
      get
      {
        return ( projectCbo.SelectedItem as Project );
      }
    }

    //-------------------------------------------------------------------------

    public string Group
    {
      get
      {
        return groupCbo.Text;
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

    private void addShortcutBtn_Click( object sender, EventArgs e )
    {
      if( shortcutsList.SelectedItem != null )
      {
        linkedShortcutList.Items.Add( shortcutsList.SelectedItem );
        shortcutsList.Items.Remove( shortcutsList.SelectedItem );
      }
    }

    //-------------------------------------------------------------------------

    private void removeShortcutBtn_Click( object sender, EventArgs e )
    {
      if( linkedShortcutList.SelectedItem != null )
      {
        shortcutsList.Items.Add( linkedShortcutList.SelectedItem );
        linkedShortcutList.Items.Remove( linkedShortcutList.SelectedItem );
      }
    }

    //-------------------------------------------------------------------------

    public List< string > LinkedShortcuts
    {
      get
      {
        List< string > tmpList = new List< string >();

        foreach( object ob in linkedShortcutList.Items )
        {
          tmpList.Add( ( ob as TemplateEntry ).Description );
        }

        return tmpList;
      }
    }

    //-------------------------------------------------------------------------

    public bool ConfirmBeforeRunning
    {
      get
      {
        return confirmBeforeRunningChkBox.Checked;
      }
    }

    //-------------------------------------------------------------------------

    public bool ScheduledRunEnabled
    {
      get
      {
        return scheduledRunChkBox.Checked;
      }
    }

    //-------------------------------------------------------------------------

    public DateTime ScheduledRunTime
    {
      get
      {
        return scheduledRunTimePicker.Value;
      }
    }

    //-------------------------------------------------------------------------
  }
}
