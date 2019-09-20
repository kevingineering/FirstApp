using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using SQLite;

namespace FirstApp.Models
{
    public class Workout : INotifyPropertyChanged
    {
        
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }                         //ID for SQL database
        public string name;                                 //Name of workout
        public string exerciseListJSON;                     //A JSON list of exercises in a workout that can be deserialized or stored in a database


        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = "") //automatically calls this method with name of calling method
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string Name                                  //Allows accessing and updating (getting and setting)
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name)); 
                // (nameof(Method)) is not necessary when [CallerMemberName] is used, but I'm leaving it in for personal reference
            }
        }            
        
        public string ExerciseListJSON
        {
            get { return exerciseListJSON; }
            set
            {
                exerciseListJSON = value;
                OnPropertyChanged();
            }
        }        

        public override string ToString()                   //Returns the name of an exercise
        {
            return Name;
        }
    }
}
