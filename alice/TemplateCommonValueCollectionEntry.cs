using System;
using System.Xml;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;

namespace alice
{
  public class TemplateCommonValueCollectionEntry : TemplateEntry
  {
    public const string c_typeName = "commonValueCollection";

    private Dictionary< string, string > m_values = new Dictionary< string, string >();
    private bool m_isActive = false;

    //-------------------------------------------------------------------------

    public TemplateCommonValueCollectionEntry()
    {
      m_type = EntryType.Type_CommonValueCollection;
    }

    //-------------------------------------------------------------------------

    public TemplateCommonValueCollectionEntry( XmlElement element )
    :
    base( element )
    {
      m_type = EntryType.Type_CommonValueCollection;

      // load collection from xml
      XmlNodeList valueNodes = element.GetElementsByTagName( "Value" );

      foreach( XmlNode node in valueNodes )
      {
        XmlElement valueElement = ( node as XmlElement );

        if( valueElement.HasAttribute( "name" ) &&
            valueElement.HasAttribute( "value" ) )
        {
          m_values.Add( valueElement.Attributes[ "name" ].Value,
                        valueElement.Attributes[ "value" ].Value );
        }
      }
    }

    //-------------------------------------------------------------------------

    public override void PostLoadEvent( Project project )
    {
      
    }

    //-------------------------------------------------------------------------

    public override bool PerformAction( Project prj, int actionHashCode )
    {
      if( base.PerformAction( prj, actionHashCode ) == false )
      {
        return true;
      }

      return true;
    }

    //-----------------------------------------------------------------------

    public override void AddXmlAttributes( XmlDocument xmlDoc, XmlElement element )
    {
      base.AddXmlAttributes( xmlDoc, element );

      foreach( string key in m_values.Keys )
      {
        string value;
        if( m_values.TryGetValue( key, out value ) )
        {
          XmlElement valueElement = xmlDoc.CreateElement( "Value" );
          element.AppendChild( valueElement );

          XmlAttribute newAttrib = xmlDoc.CreateAttribute( "name" );
          newAttrib.Value = key;
          valueElement.Attributes.Append( newAttrib );

          newAttrib = xmlDoc.CreateAttribute( "value" );
          newAttrib.Value = value;
          valueElement.Attributes.Append( newAttrib );
        }
      }
    }

    //-------------------------------------------------------------------------

    public TemplateCommonValueCollectionEntry Copy( string newDescription )
    {
      TemplateCommonValueCollectionEntry copy = new TemplateCommonValueCollectionEntry();
      copy.Description = newDescription;
      copy.Values = new Dictionary< string, string >( m_values );

      return copy;
    }

    //-------------------------------------------------------------------------

    public Dictionary< string, string > Values
    {
      get
      {
        return m_values;
      }

      set
      {
        m_values = value;
      }
    }

    //-------------------------------------------------------------------------

    public bool IsActive
    {
      get
      {
        return m_isActive;
      }

      set
      {
        m_isActive = value;
      }
    }

    //-------------------------------------------------------------------------
  }
}
