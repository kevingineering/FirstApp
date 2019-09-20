using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FirstApp.Models;
using Newtonsoft.Json;

namespace FirstApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkoutPageOld : ContentPage
    {
        public Workout workout;
        public ObservableCollection<Exercise> exerciseList = new ObservableCollection<Exercise>();
        public int selected;

        async public void SaveButton(object sender, EventArgs e)
        //Saves changes made to a workout
        {
            if (exerciseList.Count() == 0)
            {
                bool response = await DisplayAlert("Empty Workout", "Empty workouts will not be saved. Are you sure you want to leave this page?", "Yes", "No");
                if (response)
                {
                    await Navigation.PopAsync();
                }
            }
            else
            {
                Console.WriteLine(exerciseList);
                workout.ExerciseListJSON = JsonConvert.SerializeObject(exerciseList);
                App.WorkoutDB.SaveWorkout(workout);
                await Navigation.PopAsync();
            }
        }

        async public void CancelButton(object sender, EventArgs e)
        //Cancels changes made to a workout
        {
            bool response = await DisplayAlert("Cancel", "Your changes will not be saved. Are you sure you want to cancel?", "Yes", "No");
            if (response)
            {
                await Navigation.PopAsync();
            }
        }

        public void AddExerciseButton(object sender, EventArgs e)
        //Adds new exercise at end of workout list
        {
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

        public void DeleteButton(object sender, EventArgs e)
        {
            var item = (Button)sender;
            var model = (Exercise)item.CommandParameter;
            exerciseList.Remove(model);
        }

        public WorkoutPageOld(int ID)
        {
            InitializeComponent();
            if(ID != -1 && ID != -2)
            {
                workout = App.WorkoutDB.GetWorkout(ID);
                exerciseList = JsonConvert.DeserializeObject<ObservableCollection<Exercise>>(workout.ExerciseListJSON);
            }
            else if(ID == -1)
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
            else if(ID == -2)
            {
                workout = new Workout();
                Workout copyWorkout = App.WorkoutDB.GetWorkout(App.CurrentID);
                exerciseList = JsonConvert.DeserializeObject<ObservableCollection<Exercise>>(copyWorkout.ExerciseListJSON);
                workout.Name = copyWorkout.Name + " - copy";
            }
            BindingContext = this;
        }

        //Methods for changing entry fields

        public void TitleEntryCompleted(object sender, EventArgs e)
        {
            workout.Name = ((Entry)sender).Text;
        }

        public void NameEntryCompleted(object sender, EventArgs e)
        {
            exerciseList[selected].Name = ((Entry)sender).Text;
        }

        public void WeightEntryCompleted(object sender, EventArgs e)
        {
            bool success = Int32.TryParse(((Entry)sender).Text, out int temp);
            if (success)
            {
                exerciseList[selected].Weight = temp;
            }   
        }

        public void SetsEntryCompleted(object sender, EventArgs e)
        {
            bool success = Int32.TryParse(((Entry)sender).Text, out int temp);
            if (success)
            {
                exerciseList[selected].Sets = temp;
            }
        }

        public void LowRepsEntryCompleted(object sender, EventArgs e)
        {
            bool success = Int32.TryParse(((Entry)sender).Text, out int temp);
            if (success)
            {
                exerciseList[selected].LowReps = temp;
            }
        }

        public void RestEntryCompleted(object sender, EventArgs e)
        {
            bool success = Int32.TryParse(((Entry)sender).Text, out int temp);
            if (success)
            {
                exerciseList[selected].Rest = temp;
            }
        }

        public void SetTimeEntryCompleted(object sender, EventArgs e)
        {
            bool success = Int32.TryParse(((Entry)sender).Text, out int temp);
            if (success)
            {
                exerciseList[selected].SetTime = temp;
            }
        }

        public void HighRepsEntryCompleted(object sender, EventArgs e)
        {
            bool success = Int32.TryParse(((Entry)sender).Text, out int temp);
            if (success)
            {
                exerciseList[selected].HighReps = temp;
            }
        }

        private void ListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            //selected = e.ItemIndex;
            //TitleEntry.Placeholder = selected.ToString();
        }
    }
}
