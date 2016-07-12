using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace alice
{
  public partial class PairedValueSetupForm : Form
  {
    private Dictionary< string, string > m_pairedValues;
    private List< string > m_readOnlyKeys;
    private string m_keyPrefix;

    //-------------------------------------------------------------------------

    public PairedValueSetupForm( Dictionary< string,string > pairedValues,
                                 List< string > readOnlyKeys,
                                 string keyPrefix )
    {
      try
      {
        InitializeComponent();

        m_pairedValues = new Dictionary< string,string >( pairedValues );
        m_readOnlyKeys = readOnlyKeys;
        m_keyPrefix = keyPrefix;

        RefreshList();
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void RefreshList()
    {
      try
      {
        varList.Items.Clear();

        foreach( string key in m_pairedValues.Keys )
        {
          string value;
          if( m_pairedValues.TryGetValue( key, out value ) )
          {
            varList.Items.Add( key + " -> " + value );
          }
        }
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private string ExtractKeyFromKeyValueString( string s )
    {
      try
      {
        return s.Substring( 0, s.IndexOf( " -> " ) );
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );

        return "";
      }
    }

    //-------------------------------------------------------------------------

    private void addBtn_Click( object sender, EventArgs e )
    {
      try
      {
        // validate name
        if( varNameTxt.Text == "" ||
            varNameTxt.Text.ToUpper() == m_keyPrefix )
        {
          MessageBox.Show( "Enter a name for the variable.",
                           "Missing Name",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Asterisk );
          return;
        }

        // validate value
        if( varValueTxt.Text == "" )
        {
          MessageBox.Show( "Enter a value for the variable.",
                           "Missing Value",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Asterisk );
          return;
        }

        // convert to caps
        varNameTxt.Text = varNameTxt.Text.ToUpper();

        // append 'alice' prefix
        if( varNameTxt.Text.Length <= m_keyPrefix.Length ||
            varNameTxt.Text.Substring( 0, m_keyPrefix.Length ) != m_keyPrefix )
        {
          varNameTxt.Text = m_keyPrefix + varNameTxt.Text;
        }

        // if it's already in the list remove it
        if( m_pairedValues.Keys.Contains( varNameTxt.Text ) )
        {
          m_pairedValues.Remove( varNameTxt.Text );
        }

        // add it
        m_pairedValues.Add( varNameTxt.Text, varValueTxt.Text );

        RefreshList();
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void deleteBtn_Click( object sender, EventArgs e )
    {
      try
      {
        if( varList.SelectedItem != null )
        {
          // get the var name
          string key = ( varList.SelectedItem as string );
          key = ExtractKeyFromKeyValueString( key );

          // any projects using it?
          foreach( Project prj in Program.g_projectManager.Projects )
          {
            foreach( TemplateShortcutEntry entry in prj.Template.FileShortcuts )
            {
              foreach( string value in entry.EnvironmentVars.Values )
              {
                if( value.ToUpper() == key.ToUpper() )
                {
                  MessageBox.Show( "This variable cannot be deleted as it is in use in project '" + prj.Name + "', " +
                                     "shortcut '" + entry.Description + "'.",
                                   "Cannot Delete Variable",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Asterisk );
                  return;
                }
              }
            }
          }

          // remove it
          m_pairedValues.Remove( key );

          RefreshList();
        }
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void okBtn_Click( object sender, EventArgs e )
    {
      
    }

    //-------------------------------------------------------------------------

    private void cancelBtn_Click( object sender, EventArgs e )
    {

    }

    //-------------------------------------------------------------------------

    private void varList_SelectedIndexChanged( object sender, EventArgs e )
    {
      try
      {
        string key = ( varList.SelectedItem as string );

        if( key == null )
        {
          return;
        }

        key = ExtractKeyFromKeyValueString( key );

        string value;
        m_pairedValues.TryGetValue( key, out value );

        varNameTxt.Text = key;
        varValueTxt.Text = value;

        varValueTxt.ReadOnly = m_readOnlyKeys.Contains( key );
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    public Dictionary< string, string > PairedValues
    {
      get
      {
        return m_pairedValues;
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

    private void varValueTxt_KeyPress( object sender, KeyPressEventArgs e )
    {
      if( e.KeyChar == 13 )
      {
        addBtn_Click( null, null );
      }
    }

    //-------------------------------------------------------------------------
  }
}
