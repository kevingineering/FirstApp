using System;
using SQLite;
using Xamarin.Forms;
using System.IO;
using FirstApp.Data;
using FirstApp.iOS.Data;

[assembly: Dependency(typeof(SQLiteiOS))]

namespace FirstApp.iOS.Data
{
    public class SQLiteiOS : ISQLite
    {
        public SQLiteiOS() { }

        public SQLiteConnection GetConnection()
        {
            string fileName = "firstapp.db3";
            string documentPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryPath = Path.Combine(documentPath, "..", "Library");
            var path = Path.Combine(libraryPath, fileName);
            var conn = new SQLite.SQLiteConnection(path);
            return conn;
        }
    }
}