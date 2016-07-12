using System;
using System.Xml;
using System.IO;

namespace alice
{
  //---------------------------------------------------------------------------

  class MasterConfig
  {
    //-------------------------------------------------------------------------

    private string m_libraryPath = "";
    private string m_vehicleName = "";
    private string m_modelFullFilename = "";
    private string m_modelPath = "";
    private string m_worldFullFilename = "";
    private string m_worldPath = "";
    private string m_rootFolder = "";

    //-------------------------------------------------------------------------

    public static string LibraryPath
    {
      get
      {
        string libraryPath = Program.g_driveLetter + ":\\dev\\main\\library\\";

        if( Directory.Exists( libraryPath ) == false )
        {
          libraryPath = Program.g_driveLetter + ":\\dev\\main\\art\\";

          if( Directory.Exists( libraryPath ) == false )
          {
            throw new Exception( "MasterConfig::LibraryPath : Failed to find library path." );
          }
        }

        return libraryPath;
      }
    }

    //-------------------------------------------------------------------------

    public MasterConfig( string fullFilename )
    {
      //-- Figure out the library path.
      m_libraryPath = LibraryPath;

      if( Directory.Exists( m_libraryPath ) == false )
      {
        m_libraryPath = Program.g_driveLetter + ":\\dev\\main\\art\\";

        if( Directory.Exists( m_libraryPath ) == false )
        {
          throw new Exception( "MasterConfig::MasterConfig() : Failed to find library path." );
        }
      }
        
      //-- Load the doc.
      XmlDocument xmlDoc = new XmlDocument();
      xmlDoc.Load( fullFilename );

      //-- Vehicle name.
      XmlElement vehicleElement = xmlDoc.SelectSingleNode( ".//MasterConfig/VehicleCollection/Vehicle" ) as XmlElement;

      if( vehicleElement != null &&
          vehicleElement.HasAttribute( "displayName" ) )
      {
        m_vehicleName = vehicleElement.Attributes[ "displayName" ].Value;
      }

      //-- Model.
      XmlElement modelFileElement = xmlDoc.SelectSingleNode( ".//MasterConfig/VehicleCollection/Vehicle/SimObject_Vehicle/SimObject/File" ) as XmlElement;

      if( modelFileElement != null &&
          modelFileElement.HasAttribute( "relFilename" ) )
      {
        m_modelFullFilename = m_libraryPath + modelFileElement.Attributes[ "relFilename" ].Value;

        try
        {
          FileInfo info = new FileInfo( m_modelFullFilename );
          m_modelPath = info.DirectoryName;
        }
        catch
        {

        }
      }

      //-- World.
      XmlElement worldFileElement = xmlDoc.SelectSingleNode( ".//MasterConfig/WorldCollection/World" ) as XmlElement;

      if( worldFileElement != null &&
          worldFileElement.HasAttribute( "relFilename" ) )
      {
        m_worldFullFilename = m_libraryPath + worldFileElement.Attributes[ "relFilename" ].Value;

        try
        {
          FileInfo info = new FileInfo( m_worldFullFilename );
          m_worldPath = info.DirectoryName;
        }
        catch
        {

        }
      }

      //-- Root folder.
      XmlElement rootFolderElement = xmlDoc.SelectSingleNode( ".//MasterConfig/Project/RootFolder" ) as XmlElement;

      if( rootFolderElement != null &&
          rootFolderElement.HasAttribute( "absPath" ) )
      {
        m_rootFolder = rootFolderElement.Attributes[ "absPath" ].Value;
      }
    }

    //-------------------------------------------------------------------------

    public string VehicleName
    {
      get
      {
        return m_vehicleName;
      }
    }

    //-------------------------------------------------------------------------

    public string ModelPath
    {
      get
      {
        return m_modelPath;
      }
    }

    //-------------------------------------------------------------------------

    public string WorldPath
    {
      get
      {
        return m_worldPath;
      }
    }

    //-------------------------------------------------------------------------

    public string RootFolder
    {
      get
      {
        return m_rootFolder;
      }
    }

    //-------------------------------------------------------------------------
  }

  //---------------------------------------------------------------------------
}
