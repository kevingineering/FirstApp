using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace FirstApp.Models
{
    public class CompletedWorkout
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }                         //ID for SQL database
        public int WorkoutID { get; set; }                  //ID of workout we are saving
        public string Name { get; set; }                    //Name of workout we are saving
        public string ExerciseListJSON { get; set; }        //A JSON list of completed exercises
        public DateTime Date { get; set; }                  //Date the workout was last completed
    }
}
