using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace FirstApp.Models
{
    public class Exercise : INotifyPropertyChanged
    {
        public string name;                 //Name of exercise
        public int weight;                  //Weight used during exercise
        public int sets;                    //Number of sets to complete with this exercise
        public int lowReps;                 //Low end of rep range goal for this exercise
        public int highReps;                //High end of rep range goal for this exercise
        public int rest;                    //Amount of rest between sets of this exercise
        public int setTime;                 //Amount of time taken to complete all reps for this exercise
        public List<int> achievedReps;      //A list of the number of reps achieved

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = "") //automatically calls this method with name of calling method
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public Exercise()
        {
            Name = " ";
            Weight = 0;
            Sets = 3;
            LowReps = 5;
            HighReps = 8;
            Rest = 180;
            SetTime = 20;
            AchievedReps = new List<int>();
        }

        public string Name                                  
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public int Weight
        {
            get { return weight; }
            set
            {
                weight = value;
                OnPropertyChanged();
            }
        }

        public int Sets
        {
            get { return sets; }
            set
            {
                sets = value;
                OnPropertyChanged();
            }
        }

        public int LowReps
        {
            get { return lowReps; }
            set
            {
                lowReps = value;
                OnPropertyChanged();
            }
        }

        public int HighReps
        {
            get { return highReps; }
            set
            {
                highReps = value;
                OnPropertyChanged();
            }
        }

        public int Rest
        {
            get { return rest; }
            set
            {
                rest = value;
                OnPropertyChanged();
            }
        }

        public int SetTime
        {
            get { return setTime; }
            set
            {
                setTime = value;
                OnPropertyChanged();
            }
        }
          
        public List<int> AchievedReps
        {
            get { return achievedReps; }
            set
            {
                achievedReps = value;
                OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
