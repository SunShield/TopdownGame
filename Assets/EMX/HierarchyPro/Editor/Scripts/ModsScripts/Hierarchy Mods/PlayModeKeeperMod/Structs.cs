﻿#define DISABLE_PING

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace EMX.HierarchyPlugin.Editor.Mods.PlayModeSaver
{
    [Serializable]
    public class KeeperDataFieldValue : IEquatable<KeeperDataFieldValue>
    {

        [SerializeField]
        public int FILEID = -1;
        [SerializeField]
        public string GUID;

        public bool Equals( KeeperDataFieldValue other )
        {
            return this == other;
        }

        public static bool operator ==( KeeperDataFieldValue a, KeeperDataFieldValue b )
        {
            if ( (object)a == null && (object)b == null )
                return true;
            return (object)a != null && (object)b != null && a.FILEID == b.FILEID && a.GUID == b.GUID;
        }

        public static bool operator !=( KeeperDataFieldValue a, KeeperDataFieldValue b )
        {
            return !(a == b);
        }

        public override bool Equals( object obj )
        {
            return this.Equals( obj as KeeperDataFieldValue );
        }

        public override int GetHashCode( )
        {
            return FILEID != -1 ? this.FILEID  : this.GUID.GetHashCode();
        }
    }



    public class Serializer
    {

        public static string SERIALIZE_SINGLE( object ob )
        {
            using ( var stream = new MemoryStream() )
            {
                var bin = new BinaryFormatter();
                bin.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;
                bin.Serialize( stream, ob );
                var result = stream.GetBuffer();
                return Convert.ToBase64String( result );
            }
        }

        public static T DESERIALIZE_SINGLE<T>( string ser )
        {

            var bin = new BinaryFormatter();
            bin.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;
            bin.Binder = new MyBinder();

            var bytes = Convert.FromBase64String(ser);
            using ( var stream = new MemoryStream( bytes ) )
            {
                var result = bin.Deserialize(stream);
                if ( result == null ) return default( T );
                return (T)result;
            }
        }
    }

    internal sealed class MyBinder : SerializationBinder
    {
        static Dictionary<string, Type> cache = new Dictionary<string, Type>();
        public override Type BindToType( string assemblyName, string typeName )
        {
            if ( cache.ContainsKey( typeName ) ) return cache[ typeName ];

            Type ttd = null;
            // try
            {
                string toassname = assemblyName.Split(',')[0];
                Assembly[] asmblies = AppDomain.CurrentDomain.GetAssemblies();
                foreach ( Assembly ass in asmblies )
                {
                    if ( ass.FullName.Split( ',' )[ 0 ] == toassname )
                    {
                        ttd = ass.GetType( typeName );
                        break;
                    }
                }
            }
            /* catch (System.Exception e)
             {
                 throw new Exception(e.Message);
             }*/
            cache.Add( typeName, ttd );

            return ttd;
        }
    }


    // ***************** //
    // ***************** //
    // ***************** //
    // ***************** //
    // ***************** //
    [Serializable]
    internal class KeeperDataItem_SerizeHelper
    {
        public KeeperDataItem_SerizeHelper( )
        {

        }
        public KeeperDataItem_SerizeHelper( int count )
        {
            keys = new int[ count ];
            values = new KeeperDataItem[ count ];
        }
        public KeeperDataItem_SerizeHelper( Dictionary<int, KeeperDataItem> field_records ) : this( field_records.Count )
        {
            Fill( field_records );
        }

        internal void Fill( Dictionary<int, KeeperDataItem> field_records )
        {
            var i = 0;
            foreach ( var type in field_records )
            {
                keys[ i ] = type.Key;
                values[ i ] = type.Value;
                i++;
            }
        }

        [SerializeField]
        internal int[] keys;
        [SerializeField]
        internal KeeperDataItem[] values;
    }

    [Serializable]
    public class KeeperData : ISerializable
    {
        public KeeperData( )
        {   /*  Marshal.GetFunctionPointerForDelegate(GetObjectData);
              Marshal.poin
            
              System.Diagnostics.Stopwatch w = new System.Diagnostics.Stopwatch();
              Action*/
        }
        /* [SerializeField]
         internal bool ALL;
         [SerializeField]
         internal Dictionary<int, Dictionary<int, string>> field_records = new Dictionary<int, Dictionary<int, string>>();*/
        [NonSerialized]
        public Dictionary<int, string> comp_to_Type = new Dictionary<int, string>();
        [NonSerialized]
        public Dictionary<int, KeeperDataItem> field_records = new Dictionary<int, KeeperDataItem>();

        [SecurityPermissionAttribute( SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter )]
        public void GetObjectData( SerializationInfo info, StreamingContext context )
        {
            int[] comp_to_Type_keys = new int[comp_to_Type.Count];
            string[] comp_to_Type_values = new string[comp_to_Type.Count];
            int i = 0;
            foreach ( var type in comp_to_Type )
            {
                comp_to_Type_keys[ i ] = type.Key;
                comp_to_Type_values[ i ] = type.Value;
                i++;
            }

            KeeperDataItem_SerizeHelper data = new KeeperDataItem_SerizeHelper(field_records);


            //  info.se
            info.AddValue( "comp_to_Type keys", comp_to_Type_keys, typeof( int[] ) );
            info.AddValue( "comp_to_Type values", comp_to_Type_values, typeof( string[] ) );

            info.AddValue( "field_records", Serializer.SERIALIZE_SINGLE( data ), typeof( string ) );
        }

        public KeeperData( SerializationInfo info, StreamingContext context )
        {
            int[] comp_to_Type_keys = null;
            string[] comp_to_Type_values = null;
            KeeperDataItem_SerizeHelper field_records_data = null;

            try
            {
                comp_to_Type_keys = (int[])info.GetValue( "comp_to_Type keys", typeof( int[] ) );
                comp_to_Type_values = (string[])info.GetValue( "comp_to_Type values", typeof( string[] ) );

                field_records_data = Serializer.DESERIALIZE_SINGLE<KeeperDataItem_SerizeHelper>( (string)info.GetValue( "field_records", typeof( string ) ) );
            }
            catch
            {

            }


            comp_to_Type = new Dictionary<int, string>();
            for ( int i = 0 ; i < comp_to_Type_keys.Length ; i++ )
                comp_to_Type.Add( comp_to_Type_keys[ i ], comp_to_Type_values[ i ] );

            field_records = new Dictionary<int, KeeperDataItem>();
            for ( int i = 0 ; i < field_records_data.keys.Length ; i++ )
            {
                if ( field_records_data.values[ i ] == null ) continue;
                // MonoBehaviour.print(field_records_keys[i]);
                field_records.Add( field_records_data.keys[ i ], field_records_data.values[ i ] );
            }
        }
    }
    [Serializable]
    public class KeeperDataUnityJsonData : IEquatable<KeeperDataUnityJsonData>
    {
        [SerializeField]
        public int index;
        [SerializeField]
        public string default_json;
        [SerializeField]
        public string[] fields_name;
        /*[Obsolete]
        [SerializeField]
        public int[] fields_value;*/
        [SerializeField]
        public KeeperDataFieldValue[] fields_new_value;

        public static bool operator ==( KeeperDataUnityJsonData a1, KeeperDataUnityJsonData a2 )
        {
            return KeeperDataUnityJsonData.StaticEquals( a1, a2 );
        }

        public static bool operator !=( KeeperDataUnityJsonData a1, KeeperDataUnityJsonData a2 )
        {
            return !KeeperDataUnityJsonData.StaticEquals( a1, a2 );
        }

        public static bool StaticEquals( KeeperDataUnityJsonData x, KeeperDataUnityJsonData y )
        {
            if ( (object)x == null && (object)y == null )
                return true;
            return (object)x != null && (object)y != null && x.Equals( y );
        }

        public bool Equals( KeeperDataUnityJsonData x, KeeperDataUnityJsonData y )
        {
            return KeeperDataUnityJsonData.StaticEquals( x, y );
        }

        public bool Equals( KeeperDataUnityJsonData other )
        {
            if ( other == (KeeperDataUnityJsonData)null )
                return false;
            if ( this.fields_new_value == null && other.fields_new_value == null )
                return this.default_json == other.default_json;
            if ( this.fields_new_value == null || other.fields_new_value == null || this.fields_new_value.Length != other.fields_new_value.Length )
                return false;
            for ( int index = 0 ; index < this.fields_new_value.Length ; ++index )
            {
                if ( this.fields_new_value[ index ] != other.fields_new_value[ index ] )
                    return false;
            }
            return this.default_json == other.default_json;
        }

        public override bool Equals( object obj )
        {
            return this.Equals( obj as KeeperDataUnityJsonData );
        }

        bool IEquatable<KeeperDataUnityJsonData>.Equals(
          KeeperDataUnityJsonData other )
        {
            return this.Equals( other );
        }

        public override int GetHashCode( )
        {
            return base.GetHashCode();
        }
    }

    [Serializable]
    public class KeeperDataItem : ISerializable
    {
        [NonSerialized]
        public Dictionary<int, KeeperDataUnityJsonData> records = new Dictionary<int, KeeperDataUnityJsonData>();
        [NonSerialized]
        public int GameObject_ParentID;
        [NonSerialized]
        public int GameObject_SiblingPos;
        [NonSerialized]
        public string GameObject_Name;
        [NonSerialized]
        public bool GameObject_Active;
        [NonSerialized]
        public bool ALL;

        public bool GAMEOBJECT_EQUAL( KeeperDataItem other )
        {
            return this.GameObject_ParentID == other.GameObject_ParentID && this.GameObject_SiblingPos == other.GameObject_SiblingPos && this.GameObject_Name == other.GameObject_Name && this.GameObject_Active == other.GameObject_Active;
        }

        public KeeperDataItem( )
        {
        }

        [SecurityPermission( SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter )]
        public void GetObjectData( SerializationInfo info, StreamingContext context )
        {
            int[] numArray = new int[this.records.Count];
            KeeperDataUnityJsonData[] dataUnityJsonDataArray = new KeeperDataUnityJsonData[this.records.Count];
            int index = 0;
            foreach ( KeyValuePair<int, KeeperDataUnityJsonData> record in this.records )
            {
                numArray[ index ] = record.Key;
                dataUnityJsonDataArray[ index ] = record.Value;
                ++index;
            }
            info.AddValue( "GameObject_ParentID", (object)this.GameObject_ParentID, typeof( int ) );
            info.AddValue( "GameObject_SiblingPos", (object)this.GameObject_SiblingPos, typeof( int ) );
            info.AddValue( "GameObject_Name", (object)this.GameObject_Name, typeof( string ) );
            info.AddValue( "GameObject_Active", (object)this.GameObject_Active, typeof( bool ) );
            info.AddValue( "ALL", (object)this.ALL, typeof( bool ) );
            info.AddValue( "records keys", (object)numArray, typeof( int[] ) );
            info.AddValue( "records values", (object)Serializer.SERIALIZE_SINGLE( (object)dataUnityJsonDataArray ), typeof( string ) );
        }

        public KeeperDataItem(
          int GameObject_ParentID,
          int GameObject_SiblingPos,
          string GameObject_Name,
          bool GameObject_Active )
        {
            this.GameObject_ParentID = GameObject_ParentID;
            this.GameObject_SiblingPos = GameObject_SiblingPos;
            this.GameObject_Name = GameObject_Name;
            this.GameObject_Active = GameObject_Active;
        }

        public KeeperDataItem( SerializationInfo info, StreamingContext context )
        {
            try
            {
                try
                {
                    this.GameObject_ParentID = (int)info.GetValue( nameof( GameObject_ParentID ), typeof( int ) );
                }
                catch
                {
                    this.GameObject_ParentID = (int)(int)info.GetValue( nameof( GameObject_ParentID ), typeof( int ) );
                }
                this.GameObject_SiblingPos = (int)info.GetValue( nameof( GameObject_SiblingPos ), typeof( int ) );
                this.GameObject_Name = (string)info.GetValue( nameof( GameObject_Name ), typeof( string ) );
                this.GameObject_Active = (bool)info.GetValue( nameof( GameObject_Active ), typeof( bool ) );
                this.ALL = (bool)info.GetValue( nameof( ALL ), typeof( bool ) );
            }
            catch
            {
            }
            try
            {
                int[] numArray = (int[]) info.GetValue("records keys", typeof (int[]));
                KeeperDataUnityJsonData[] dataUnityJsonDataArray = Serializer.DESERIALIZE_SINGLE<KeeperDataUnityJsonData[]>((string) info.GetValue("records values", typeof (string)));
                this.records.Clear();
                for ( int index = 0 ; index < numArray.Length ; ++index )
                    this.records.Add( numArray[ index ], dataUnityJsonDataArray[ index ] );
            }
            catch
            {
            }
        }
    }
}
