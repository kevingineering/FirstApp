using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using SQLite;
using Xamarin.Forms;
using FirstApp.Models;

namespace FirstApp.Data
{
    //This class creates database, reads data from it, writes data to it, and deletes data from it.
    //For help setting up SQL database - https://www.youtube.com/watch?v=z9JgdcguBqQ
    public class WorkoutDBController
    {
        private static object locker = new object();
        private SQLiteConnection database;

        public WorkoutDBController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<Workout>();
        }

        public IEnumerator<Workout> GetWorkouts()
        {
            lock(locker)
            {
                if (database.Table<Workout>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<Workout>().GetEnumerator();
                }
            }
        }

        public Workout GetWorkout(int ID)
        {
            lock(locker)
            {
                return database.Table<Workout>().Where(i => i.ID == ID).FirstOrDefault();
            }
        }

        public int SaveWorkout(Workout workout)
        {
            lock (locker)
            {
                if (workout.ID != 0)
                {
                    database.Update(workout);
                    return workout.ID;
                }
                else
                {
                    return database.Insert(workout);
                }
            }
        }

        public int DeleteWorkout(int ID)
        {
            lock (locker)
            {
                return database.Delete<Workout>(ID);
            }
        }
    }
}
