using System;
using System.IO;
using System.Collections.Generic;

namespace alice
{
  public class Project
  {
    private TemplateXml m_template;
    private string m_filename = "";
    private TemplateCommonValueCollectionEntry m_activeCommonValueCollection;

    //-------------------------------------------------------------------------

    public Project( string filename )
    {
      m_template = new TemplateXml();
      m_filename = filename;
    }

    //-------------------------------------------------------------------------

    public void LoadFromFile()
    {
      m_template.LoadFromFile( ProjectManager.c_projectFolderPath + @"\" + m_filename );

      RefreshActiveCommonValueCollection();

      m_template.PostLoadEvent( this );
    }

    //-------------------------------------------------------------------------

    public void WriteToFile()
    {
      m_template.WriteToFile( ProjectManager.c_projectFolderPath + @"\" + m_filename );
    }

    //-------------------------------------------------------------------------

    public void DeleteFile()
    {
      File.Delete( ProjectManager.c_projectFolderPath + @"\" + m_filename );  
    }

    //-------------------------------------------------------------------------

    public Project Copy( string newName )
    {
      // create copy's filename
      string filename = newName + "." + ProjectManager.c_projectExt;
      string copyFullFilename = ProjectManager.c_projectFolderPath + @"\" + filename;

      // write this project's template to a new file
      m_template.WriteToFile( copyFullFilename );

      // create new project object
      Project newPrj = new Project( filename );
      newPrj.LoadFromFile();
      newPrj.Name = newName;
      newPrj.WriteToFile();

      return newPrj;
    }

    //-------------------------------------------------------------------------

    private void RefreshActiveCommonValueCollection()
    {
      // find the active collection
      foreach( TemplateCommonValueCollectionEntry entry in m_template.CommonValueCollections )
      {
        if( entry.IsActive )
        {
          m_activeCommonValueCollection = entry;
          break;
        }
      }

      // none marked as being active?
      if( m_activeCommonValueCollection == null )
      {
        // use the first one
        if( m_template.CommonValueCollections.Count > 0 )
        {
          m_activeCommonValueCollection = m_template.CommonValueCollections[ 0 ];
        }
        else
        {
          m_activeCommonValueCollection = new TemplateCommonValueCollectionEntry();
          m_activeCommonValueCollection.Description = "Default";

          m_template.AddEntry( m_activeCommonValueCollection );

          WriteToFile();
        }
      }
    }

    //-------------------------------------------------------------------------

    // Returns the common value's value if the value is for a common value,
    // otherwise just returns the given value.
    //
    // simpleCompare == true : just compares entire strings
    // simpleCompare == false : checks within strings to find common value keys

    public string GetCommonValue( string value, bool simpleCompare )
    {
      // try this project
      foreach( string commonKey in CommonValues.Keys )
      {
        if( simpleCompare )
        {
          if( commonKey.ToUpper() == value.ToUpper() )
          {
            CommonValues.TryGetValue( commonKey, out value );
            return value;
          }
        }
        else
        {
          string valueUpper = value.ToUpper();

          if( valueUpper.Contains( commonKey.ToUpper() ) )
          {
            string commonKeyValue;
            CommonValues.TryGetValue( commonKey, out commonKeyValue );

            int index = valueUpper.IndexOf( commonKey.ToUpper() );

            value = value.Remove( index, commonKey.Length );
            value = value.Insert( index, commonKeyValue );

            return value;
          }
        }
      }

      // try the global store
      foreach( string commonKey in Program.g_projectManager.CommonValues.Keys )
      {
        if( simpleCompare )
        {
          if( commonKey.ToUpper() == value.ToUpper() )
          {
            Program.g_projectManager.CommonValues.TryGetValue( commonKey, out value );
            return value;
          }
        }
        else
        {
          string valueUpper = value.ToUpper();

          if( valueUpper.Contains( commonKey.ToUpper() ) )
          {
            string commonKeyValue;
            Program.g_projectManager.CommonValues.TryGetValue( commonKey, out commonKeyValue );

            int index = valueUpper.IndexOf( commonKey.ToUpper() );

            value = value.Remove( index, commonKey.Length );
            value = value.Insert( index, commonKeyValue );

            return value;
          }
        }
      }

      return value;
    }

    //-------------------------------------------------------------------------

    // Syncs all collections to the active collection.

    public void SyncCommonValueCollections()
    {
      // if the active collection has keys the others don't, add them to the others
      foreach( TemplateCommonValueCollectionEntry entry in m_template.CommonValueCollections )
      {
        if( entry == m_activeCommonValueCollection )
        {
          continue;
        }

        foreach( string key in m_activeCommonValueCollection.Values.Keys )
        {
          if( entry.Values.ContainsKey( key ) == false )
          {
            string value;
            if( m_activeCommonValueCollection.Values.TryGetValue( key, out value ) )
            {
              entry.Values.Add( key, value );
            }
          }
        }
      }

      // if the other collections have keys that aren't in the active collection,
      // remove them from the other collections.
      foreach( TemplateCommonValueCollectionEntry entry in m_template.CommonValueCollections )
      {
        if( entry == m_activeCommonValueCollection )
        {
          continue;
        }

        List< string > keysToRemove = new List< string >();

        foreach( string key in entry.Values.Keys )
        {
          if( m_activeCommonValueCollection.Values.ContainsKey( key ) == false )
          {
            keysToRemove.Add( key );
          }
        }

        foreach( string key in keysToRemove )
        {
          entry.Values.Remove( key );
        }
      }
    }

    //-------------------------------------------------------------------------

    public override string ToString()
    {
      return Name;
    }

    //-------------------------------------------------------------------------

    public TemplateXml Template
    {
      get
      {
        return m_template;
      }
    }

    //-------------------------------------------------------------------------

    public string Name
    {
      get
      {
        return m_template.Name;
      }

      set
      {
        m_template.Name = value;
      }
    }

    //-------------------------------------------------------------------------

    public bool IsArchived
    {
      get
      {
        return m_template.IsArchived;
      }

      set
      {
        m_template.IsArchived = value;
      }
    }

    //-------------------------------------------------------------------------

    public TemplateCommonValueCollectionEntry ActiveCommonValueCollection
    {
      get
      {
        return m_activeCommonValueCollection;
      }

      set
      {
        m_activeCommonValueCollection = value;

        // set other collections as inactive & specified collection as active
        foreach( TemplateCommonValueCollectionEntry entry in m_template.CommonValueCollections )
        {
          entry.IsActive = ( entry == m_activeCommonValueCollection );
        }
      }
    }

    //-------------------------------------------------------------------------

    public Dictionary< string, string > CommonValues
    {
      get
      {
        if( m_activeCommonValueCollection != null )
        {
          return m_activeCommonValueCollection.Values;
        }
        else
        {
          return new Dictionary< string, string >();
        }
      }

      set
      {
        m_activeCommonValueCollection.Values = value;

        SyncCommonValueCollections();
      }
    }

    //-------------------------------------------------------------------------

    public List< string > UiGroups
    {
      get
      {
        List< string > list = new List< string >();

        foreach( TemplateShortcutEntry entry in m_template.FileShortcuts )
        {
          if( entry.UiGroupName != "" &&
              list.Contains( entry.UiGroupName ) == false )
          {
            list.Add( entry.UiGroupName );
          }
        }

        return list;
      }
    }

    //-------------------------------------------------------------------------
  }
}