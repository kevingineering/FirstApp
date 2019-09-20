using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using SQLite;
using Xamarin.Forms;
using FirstApp.Models;

namespace FirstApp.Data
{
    public class CompletedWorkoutDBController
    {
        private static object locker = new object();
        private SQLiteConnection database;

        public CompletedWorkoutDBController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<CompletedWorkout>();
        }

        public IEnumerator<CompletedWorkout> GetCompletedWorkouts()
        {
            lock (locker)
            {
                if (database.Table<CompletedWorkout>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<CompletedWorkout>().GetEnumerator();
                }
            }
        }

        public CompletedWorkout GetCompletedWorkout(int ID)
        {
            lock (locker)
            {
                return database.Table<CompletedWorkout>().Where(i => i.ID == ID).FirstOrDefault();
            }
        }

        public int SaveCompletedWorkout(CompletedWorkout completedWorkout)
        {
            lock (locker)
            {
                if (completedWorkout.ID != 0)
                {
                    database.Update(completedWorkout);
                    return completedWorkout.ID;
                }
                else
                {
                    return database.Insert(completedWorkout);
                }
            }
        }

        public int DeleteCompletedWorkout(int ID)
        {
            lock (locker)
            {
                return database.Delete<CompletedWorkout>(ID);
            }
        }
    }
}
