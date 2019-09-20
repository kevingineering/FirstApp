using FirstApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

//MVVM Design - https://www.youtube.com/watch?v=GZDQptTQZsk

namespace FirstApp.ViewModel
{
    public class WorkoutPageViewModel : INotifyPropertyChanged
    {
        public string friend = "Kevin";
        public Workout workout;
        public ObservableCollection<Exercise> exerciseList = new ObservableCollection<Exercise>();
        public int selected;

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = "") //automatically calls this method with name of calling method
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public WorkoutPageViewModel(int ID)
        {
            if (ID != -1 && ID != -2)
            {
                workout = App.WorkoutDB.GetWorkout(ID);
                exerciseList = JsonConvert.DeserializeObject<ObservableCollection<Exercise>>(workout.ExerciseListJSON);
            }
            else if (ID == -1)
            {
                workout = new Workout();
                exerciseList.Add(new Exercise
                {
                    Name = "Exercise",
                    Weight = 0,
                    Sets = 3,
                    LowReps = 8,
                    HighReps = 12,
                    Rest = 180,
                    SetTime = 20
                });
            }
            else if (ID == -2)
            {
                workout = new Workout();
                Workout copyWorkout = App.WorkoutDB.GetWorkout(App.CurrentID);
                exerciseList = JsonConvert.DeserializeObject<ObservableCollection<Exercise>>(copyWorkout.ExerciseListJSON);
                workout.Name = copyWorkout.Name + " - copy";
            }
        }
    }
}
