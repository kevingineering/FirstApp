using SQLite;

//SQLite Information - http://bsubramanyamraju.blogspot.com/2018/03/xamarinforms-mvvm-sqlite-sample-for.html - See also Bert Bosch Xamarin YouTube tutorial referenced in WorkoutDBController

namespace FirstApp.Data
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
