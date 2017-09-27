using System;
using System.Collections.Generic;
using System.Text;
using OpenMind.Modelos.SQLite;
using OpenMind.iOS;
using Xamarin.Forms;
using System.IO;

[assembly: Dependency(typeof(SQLite_iOS))]
namespace OpenMind.iOS
{
    public class SQLite_iOS : ISQLite
    {
        
        public SQLite_iOS() 
        {
            System.Diagnostics.Debug.WriteLine("entro aca");
        }
        public SQLite.SQLiteConnection GetConnection()
        {
            var sqliteFilename = "OpenMindSQLite.db3";
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryPath = Path.Combine(documentsPath, "..", "Library");
            var path = Path.Combine(libraryPath, sqliteFilename);
            var conn = new SQLite.SQLiteConnection(path);
            return conn;
        }
    }
}