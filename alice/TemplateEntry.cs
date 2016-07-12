using System;
using System.Xml;

namespace alice
{
  public abstract class TemplateEntry
  {
    public enum EntryType
    {
      Type_Unknown,
      Type_Shortcut,
      Type_FileToMonitor,
      Type_CommonValue,
      Type_CommonValueCollection
    };

    protected EntryType m_type = EntryType.Type_Unknown;
    private string m_description = "";
    private int m_lastActionHashCode = -1;

    //-----------------------------------------------------------------------

    public TemplateEntry()
    {

    }

    //-----------------------------------------------------------------------

    public TemplateEntry( XmlElement element )
    {
      // description
      m_description = element.Attributes[ "description" ].Value;
    }

    //-----------------------------------------------------------------------

    public override string ToString()
    {
      return m_description;
    }

    //-----------------------------------------------------------------------

    public string GetTypeAsString()
    {
      switch( m_type )
      {
        case EntryType.Type_Shortcut:
          return TemplateShortcutEntry.c_typeName;

        case EntryType.Type_FileToMonitor:
          return "fileToMonitor";

        case EntryType.Type_CommonValueCollection:
          return TemplateCommonValueCollectionEntry.c_typeName;

        default:
          throw new Exception( "Unknown template entry type id." );
      }
    }

    //-----------------------------------------------------------------------

    public abstract void PostLoadEvent( Project project );

    //-----------------------------------------------------------------------

    public virtual bool PerformAction( Project project,
                                       int actionHashCode )
    {
      // prevent infinite-loops via linked-shortcuts
      if( m_lastActionHashCode == actionHashCode )
      {
        return false;
      }

      m_lastActionHashCode = actionHashCode;

      return true;
    }

    //-----------------------------------------------------------------------

    public virtual void AddXmlAttributes( XmlDocument xmlDoc, XmlElement element )
    {

    }

    //-----------------------------------------------------------------------

    public EntryType Type
    {
      get
      {
        return m_type;
      }

      set
      {
        m_type = value;
      }
    }

    //-----------------------------------------------------------------------

    public string Description
    {
      get
      {
        return m_description;
      }

      set
      {
        m_description = value;
      }
    }

    //-----------------------------------------------------------------------
  }
}