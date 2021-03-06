﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using Xamarin.Forms;
using OpenMind.Modelos.SQLite;
using OpenMind.Droid;

[assembly: Dependency(typeof(SQLite_Android))]
namespace OpenMind.Droid
{
	public class SQLite_Android : ISQLite
	{
		public SQLite_Android() { }
		public SQLite.SQLiteConnection GetConnection()
		{
			var sqliteFilename = "OpenMindSQLite.db3";
			string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
			var path = Path.Combine(documentsPath, sqliteFilename);
			var conn = new SQLite.SQLiteConnection(path);
			return conn;
		}
	}
}