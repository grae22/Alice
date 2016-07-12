using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace alice
{
  static class Program
  {
    public static string g_path = Application.StartupPath;
    public static char g_driveLetter = Application.StartupPath[ 0 ];
    public static ProjectManager g_projectManager;

    //-------------------------------------------------------------------------

    [STAThread]
    static void Main( string[] args )
    {
      using( Mutex mutex = new Mutex( false, "Global\\Alice" ) )
      {
        if( mutex.WaitOne( 0, true ) == false )
        {
          MessageBox.Show( "Another instance of Alice is already running.",
                           "Already Running",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Exclamation );
          return;
        }

        if( args.Length > 0 )
        {
          g_projectManager = new ProjectManager( args[ 0 ] );
        }
        else
        {
          g_projectManager = new ProjectManager( "" );
        }

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new mainForm());

        g_projectManager.SaveSettings();

        mutex.ReleaseMutex();
      }
    }

    //-------------------------------------------------------------------------
  }
}
