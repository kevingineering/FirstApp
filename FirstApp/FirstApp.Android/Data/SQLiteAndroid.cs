using FirstApp.Data;
using Xamarin.Forms;
using SQLite;
using FirstApp.Droid.Data;
using System.IO;

[assembly: Dependency(typeof(SQLiteAndroid))]

namespace FirstApp.Droid.Data
{
    public class SQLiteAndroid : ISQLite
    {
        public SQLiteAndroid() { }

        public SQLiteConnection GetConnection()
        {
            var fileName = "firstapp.db3";
            string documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            var path = Path.Combine(documentPath, fileName);

            var conn = new SQLite.SQLiteConnection(path);
            return conn;
        }


    }
}