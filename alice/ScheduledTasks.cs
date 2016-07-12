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
  public partial class ScheduledTasks : Form
  {
    //-------------------------------------------------------------------------

    public ScheduledTasks()
    {
      InitializeComponent();
      PopulateEntriesList();

      projectLbl.Text = "";
      nextRunLbl.Text = "";
    }

    //-------------------------------------------------------------------------

    private void PopulateEntriesList()
    {
      entryList.Items.Clear();

      foreach( Project prj in Program.g_projectManager.Projects )
      {
        foreach( TemplateShortcutEntry entry in prj.Template.FileShortcuts )
        {
          if( entry.ScheduledRunEnabled )
          {
            entryList.Items.Add( entry );
          }
        }
      }

      RefreshDisplayedInfo();
    }

    //-------------------------------------------------------------------------

    private void closeBtn_Click( object sender, EventArgs e )
    {
      Dispose();
    }

    //-------------------------------------------------------------------------

    private void entryList_SelectedIndexChanged( object sender, EventArgs e )
    {
      RefreshDisplayedInfo();
    }

    //-------------------------------------------------------------------------

    private void resetBtn_Click( object sender, EventArgs e )
    {
      if( entryList.SelectedItem == null )
      {
        return;
      }

      TemplateShortcutEntry entry = entryList.SelectedItem as TemplateShortcutEntry;

      if( entry != null )
      {
        DateTime runTime =
          new DateTime( DateTime.Now.Year,
                        DateTime.Now.Month,
                        DateTime.Now.Day,
                        runTimePicker.Value.Hour,
                        runTimePicker.Value.Minute,
                        0 );

        entry.NextScheduledRunTime = runTime;

        while( entry.NextScheduledRunTime < DateTime.Now )
        {
          entry.NextScheduledRunTime = entry.NextScheduledRunTime.AddDays( 1.0 );
        }

        entry.Project.WriteToFile();
      }

      RefreshDisplayedInfo();
    }

    //-------------------------------------------------------------------------

    private void RefreshDisplayedInfo()
    {
      if( entryList.SelectedItem == null )
      {
        projectLbl.Text = "";
        nextRunLbl.Text = "";
        return;
      }

      TemplateShortcutEntry entry = entryList.SelectedItem as TemplateShortcutEntry;

      if( entry != null )
      {
        projectLbl.Text = entry.Project.Name;
        nextRunLbl.Text = entry.NextScheduledRunTime.ToString();
        runTimePicker.Value = entry.NextScheduledRunTime;
      }
    }

    //-------------------------------------------------------------------------

    private void removeBtn_Click( object sender, EventArgs e )
    {
      if( entryList.SelectedItem == null )
      {
        return;
      }

      TemplateShortcutEntry entry = entryList.SelectedItem as TemplateShortcutEntry;

      if( entry != null )
      {
        entry.ScheduledRunEnabled = false;
        entry.Project.WriteToFile();
      }

      PopulateEntriesList();
    }

    //-------------------------------------------------------------------------
  }
}
