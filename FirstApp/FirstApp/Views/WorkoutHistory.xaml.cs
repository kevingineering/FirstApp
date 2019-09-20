using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FirstApp.Data;
using FirstApp.Models;
using System.Collections.ObjectModel;

namespace FirstApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkoutHistory : ContentPage
    {
        private ObservableCollection<CompletedWorkout> CompletedWorkoutList = new ObservableCollection<CompletedWorkout>(); //Collection of workouts

        async public void DeleteButton(object sender, EventArgs e)
        {
            bool response = await DisplayAlert("Delete?", "Are you sure you want to delete this workout?", "Yes", "No");
            if (response)
            {
                var item = (ImageButton)sender;
                var model = (CompletedWorkout)item.CommandParameter;
                CompletedWorkoutList.Remove(model);
                App.CompletedWorkoutDB.DeleteCompletedWorkout(model.ID);
                OnAppearing();
            }
        }

        async void SummaryButton(object sender, EventArgs e)
        {
            var item = (Button)sender;
            var model = (CompletedWorkout)item.CommandParameter;
            await Navigation.PushAsync(new SummaryPage(model.ID));
        }

        public void Init()
        {
            var enumerator = App.CompletedWorkoutDB.GetCompletedWorkouts();
            if (enumerator == null)
            {

            }
            else
            {
                while (enumerator.MoveNext())
                {
                    CompletedWorkoutList.Add(enumerator.Current);
                }
            }
        }

        protected override void OnAppearing()
        {
            CompletedWorkoutList.Clear();
            Init();
            listView.ItemsSource = CompletedWorkoutList;
        }

        public WorkoutHistory()
        {
            InitializeComponent();
            Init();
            listView.ItemsSource = CompletedWorkoutList;
        }
    }
}