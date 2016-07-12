using System;
using System.Collections.Generic;
using System.Xml;

namespace alice
{
  public class TemplateXml
  {
    private string m_name = "unknown";
    private List< TemplateEntry > m_entries = new List< TemplateEntry >();
    private bool m_isArchived = false;

    //-------------------------------------------------------------------------

    public TemplateXml()
    {

    }

    //-------------------------------------------------------------------------

    public void LoadFromFile( string fullFilename )
    {
      XmlDocument xmlDoc = new XmlDocument();
      xmlDoc.Load( fullFilename );

      // template info
      XmlNodeList nodes = xmlDoc.GetElementsByTagName( "Info" );
      XmlElement infoElement = nodes[ 0 ] as XmlElement;
      m_name = infoElement.Attributes[ "name" ].Value;

      if( infoElement.HasAttribute( "isArchived" ) )
      {
        m_isArchived = Boolean.Parse( infoElement.Attributes[ "isArchived" ].Value );
      }

      // entries
      XmlNodeList entryElements = xmlDoc.GetElementsByTagName( "Entry" );

      foreach( XmlNode xmlNode in entryElements )
      {
        TemplateEntry newEntry = null;

        // get the type
        XmlElement entryElement = ( xmlNode as XmlElement );

        string type = entryElement.Attributes[ "type" ].Value;

        // create the specified type of entry
        switch( type )
        {
          case TemplateShortcutEntry.c_typeName:
            newEntry = new TemplateShortcutEntry( entryElement );
            break;

          case TemplateCommonValueCollectionEntry.c_typeName:
            newEntry = new TemplateCommonValueCollectionEntry( entryElement );
            break;
        }

        // add it to the list
        if( newEntry != null )
        {
          AddEntry( newEntry );
        }
      }
    }

    //-------------------------------------------------------------------------

    public void AddEntry( TemplateEntry newEntry )
    {
      // check name isn't already used
      foreach( TemplateEntry entry in m_entries )
      {
        if( entry.Description.ToLower() == newEntry.Description.ToLower() )
        {
          throw new Exception( "Entry '" + newEntry.Description + "' already exists." );
        }
      }
      
      // add it
      m_entries.Add( newEntry );
    }

    //-------------------------------------------------------------------------

    public void DeleteEntry( TemplateEntry entry )
    {
      m_entries.Remove( entry );
    }

    //-------------------------------------------------------------------------

    public void WriteToFile( string fullFilename )
    {
      XmlDocument xmlDoc = new XmlDocument();

      XmlElement rootElement = xmlDoc.CreateElement( "Root" );
      xmlDoc.AppendChild( rootElement );

      // template info
      XmlElement infoElement = xmlDoc.CreateElement( "Info" );
      rootElement.AppendChild( infoElement );

      XmlAttribute newAttrib = xmlDoc.CreateAttribute( "name" );
      newAttrib.Value = m_name;
      infoElement.Attributes.Append( newAttrib );

      newAttrib = xmlDoc.CreateAttribute( "isArchived" );
      newAttrib.Value = m_isArchived.ToString();
      infoElement.Attributes.Append( newAttrib );

      // add all entries to xml doc
      foreach( TemplateEntry entry in m_entries )
      {
        XmlElement newElement = xmlDoc.CreateElement( "Entry" );
        
        // type
        newAttrib = xmlDoc.CreateAttribute( "type" );
        newAttrib.Value = entry.GetTypeAsString();
        newElement.Attributes.Append( newAttrib );

        // description
        newAttrib = xmlDoc.CreateAttribute( "description" );
        newAttrib.Value = entry.Description;
        newElement.Attributes.Append( newAttrib );

        // child class attribs
        entry.AddXmlAttributes( xmlDoc, newElement );

        rootElement.AppendChild( newElement );
      }

      try
      {
        xmlDoc.Save( fullFilename );
      }
      catch
      {
        // Do nothing.
      }
    }

    //-------------------------------------------------------------------------

    public void SwapEntries( TemplateEntry e1, TemplateEntry e2 )
    {
      int index1 = m_entries.IndexOf( e1 );
      int index2 = m_entries.IndexOf( e2 );

      if( index1 >= 0 &&
          index2 >= 0 &&
          index1 != index2 )
      {
        if( index1 < index2 )
        {
          m_entries.Remove( e2 );
          m_entries.Insert( index1, e2 );

          m_entries.Remove( e1 );
          m_entries.Insert( index2, e1 );
        }
        else
        {
          m_entries.Remove( e1 );
          m_entries.Insert( index2, e1 );

          m_entries.Remove( e2 );
          m_entries.Insert( index1, e2 );
        }
      }
    }

    //-------------------------------------------------------------------------

    public string Name
    {
      get
      {
        return m_name;
      }

      set
      {
        m_name = value;
      }
    }

    //-------------------------------------------------------------------------

    public bool IsArchived
    {
      get
      {
        return m_isArchived;
      }

      set
      {
        m_isArchived = value;
      }
    }

    //-------------------------------------------------------------------------

    public List< TemplateShortcutEntry > FileShortcuts
    {
      get
      {
        List< TemplateShortcutEntry > list = new List< TemplateShortcutEntry >();

        foreach( TemplateEntry entry in m_entries )
        {
          if( entry.Type == TemplateEntry.EntryType.Type_Shortcut )
          {
            list.Add( entry as TemplateShortcutEntry );
          }
        }

        return list;
      }
    }

    //-------------------------------------------------------------------------

    public List< TemplateCommonValueCollectionEntry > CommonValueCollections
    {
      get
      {
        List< TemplateCommonValueCollectionEntry > list = new List< TemplateCommonValueCollectionEntry >();

        foreach( TemplateEntry entry in m_entries )
        {
          if( entry.Type == TemplateEntry.EntryType.Type_CommonValueCollection )
          {
            list.Add( entry as TemplateCommonValueCollectionEntry );
          }
        }

        return list;
      }
    }

    //-------------------------------------------------------------------------

    public void PostLoadEvent( Project project )
    {
      foreach( TemplateEntry entry in m_entries )
      {
        entry.PostLoadEvent( project );
      }
    }

    //-------------------------------------------------------------------------

    public TemplateEntry GetEntryWithDescription( string description )
    {
      foreach( TemplateEntry entry in m_entries )
      {
        if( entry.Description == description )
        {
          return entry;
        }
      }

      return null;
    }

    //-------------------------------------------------------------------------
  }
}
