using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using FirstApp.Models;

namespace FirstApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SummaryPage : ContentPage
    {
        public List<CompletedExercise> exerciseList;
        public SummaryPage(int ID)
        {
            InitializeComponent();
            CompletedWorkout completedWorkout = App.CompletedWorkoutDB.GetCompletedWorkout(ID);
            exerciseList = JsonConvert.DeserializeObject<List<CompletedExercise>>(completedWorkout.ExerciseListJSON);

            workoutName.Text = completedWorkout.Name;
            workoutDay.Text = completedWorkout.Date.ToString("MM/dd/yyyy");
            workoutTime.Text = completedWorkout.Date.ToString("h:mm tt");
            listView.ItemsSource = exerciseList;
            TestJson.Text = completedWorkout.ExerciseListJSON;
        }

        async void HomeButton(object sender, EventArgs e)
        //Navigates to main page
        {
            await Navigation.PopToRootAsync();
        }
    }
}