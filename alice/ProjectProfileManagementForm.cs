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
  public partial class ProjectProfileManagementForm : Form
  {
    private Project m_project;

    //-------------------------------------------------------------------------

    public ProjectProfileManagementForm( Project project )
    {
      try
      {
        m_project = project;

        InitializeComponent();

        RefreshProfileList();
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

    private void RefreshProfileList()
    {
      try
      {
        profileList.DataSource = null;
        profileList.DataSource = m_project.Template.CommonValueCollections;
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void newBtn_Click( object sender, EventArgs e )
    {
      try
      {
        // already exists? do nothing
        foreach( TemplateEntry entry in m_project.Template.CommonValueCollections )
        {
          if( entry.Description.ToLower() == "new profile" )
          {
            return;
          }
        }

        // create new profile
        TemplateCommonValueCollectionEntry newProfile = new
          TemplateCommonValueCollectionEntry();

        newProfile.Description = "New Profile";

        m_project.Template.AddEntry( newProfile );

        RefreshProfileList();
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void copyBtn_Click( object sender, EventArgs e )
    {
      try
      {
        TemplateCommonValueCollectionEntry source =
          ( profileList.SelectedItem as TemplateCommonValueCollectionEntry );

        if( source != null )
        {
          string newDescription = "Copy of " + source.Description;

          // already exists? do nothing
          foreach( TemplateEntry entry in m_project.Template.CommonValueCollections )
          {
            if( entry.Description.ToLower() == newDescription.ToLower() )
            {
              return;
            }
          }

          // create copy
          TemplateCommonValueCollectionEntry newProfile =
            source.Copy( newDescription );

          m_project.Template.AddEntry( newProfile );

          RefreshProfileList();
        }
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
        TemplateCommonValueCollectionEntry toDelete =
          ( profileList.SelectedItem as TemplateCommonValueCollectionEntry );

        if( toDelete != null &&
            m_project.Template.CommonValueCollections.Count > 1 )
        {
          // sure?
          if( MessageBox.Show( "Delete profile '" + toDelete.Description + "'?",
                               "Delete?",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Exclamation,
                               MessageBoxDefaultButton.Button2 ) == DialogResult.No )
          {
            return;
          }

          // delete it
          m_project.Template.DeleteEntry( toDelete );

          RefreshProfileList();
        }
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void nameTxt_KeyPress( object sender, KeyPressEventArgs e )
    {
      try
      {
        // on 'enter' key
        if( e.KeyChar != 13 )
        {
          return;
        }

        // blank? do nothing
        if( nameTxt.Text == "" )
        {
          return;
        }

        // already exists?
        foreach( TemplateCommonValueCollectionEntry entry in m_project.Template.CommonValueCollections )
        {
          if( entry.Description.ToLower() == nameTxt.Text.ToLower() )
          {
            MessageBox.Show( "Profile name '" + nameTxt.Text + "' already exists.",
                             "Error",
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Exclamation );
            return;
          }
        }

        // rename the profile
        ( profileList.SelectedItem as TemplateCommonValueCollectionEntry ).Description = nameTxt.Text;

        RefreshProfileList();
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void commonValuesBtn_Click( object sender, EventArgs e )
    {
      try
      {
        TemplateCommonValueCollectionEntry values =
          ( profileList.SelectedItem as TemplateCommonValueCollectionEntry );

        if( values == null )
        {
          return;
        }

        m_project.ActiveCommonValueCollection = values;

        PairedValueSetupForm form =
          new PairedValueSetupForm( m_project.CommonValues,
                                    new List< string >(),
                                    "PRJ_" );

        form.Text = "Project '" + m_project.Name + "' Common Values Setup";
        form.ShowDialog();

        if( form.DialogResult == DialogResult.OK )
        {
          m_project.CommonValues = form.PairedValues;
          m_project.WriteToFile();
        }

        form.Dispose();
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void closeBtn_Click( object sender, EventArgs e )
    {
      try
      {
        m_project.WriteToFile();

        Dispose();
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void profileList_SelectedIndexChanged( object sender, EventArgs e )
    {
      try
      {
        TemplateCommonValueCollectionEntry entry =
          ( profileList.SelectedItem as TemplateCommonValueCollectionEntry );

        if( entry != null )
        {
          nameTxt.Text = entry.Description;
        }
      }
      catch( Exception ex )
      {
        ErrorMsg( ex.Message );
      }
    }

    //-------------------------------------------------------------------------

    private void btnCancel_Click( object sender, EventArgs e )
    {
      Dispose();
    }

    //-------------------------------------------------------------------------
  }
}
