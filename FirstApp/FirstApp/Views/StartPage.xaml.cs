using FirstApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using FirstApp.Views;

namespace FirstApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]

    public partial class StartPage : ContentPage
    {
        private ObservableCollection<Workout> WorkoutList = new ObservableCollection<Workout>(); //Collection of workouts

        async void WorkoutButton(object sender, EventArgs e)
        //Checks if workout already in session, navigates to workout day based on user response, no double clicking
        {
            bool response = true;
            /*if (App.InProgress)
            {
                response = await DisplayAlert("Workout in Progress", "Would you like to resume your in progress workout, or start a new workout?", "Resume", "New");
            }*/
            if (response)
            {
                var item = (Button)sender;
                var model = (Workout)item.CommandParameter;
                App.CurrentID = model.ID;
                await Navigation.PushAsync(new WorkoutDay(App.CurrentID));
            }
            else
            {
                await Navigation.PushAsync(new WorkoutDay(App.CurrentID));
            }
        }

        async void EditButton(object sender, EventArgs e)
        //Navigates to WorkoutEdit Screen
        {
            var item = (ImageButton)sender;
            var model = (Workout)item.CommandParameter;
            App.CurrentID = model.ID;
            await Navigation.PushAsync(new WorkoutPage(App.CurrentID));
        }

        async void CopyButton(object sender, EventArgs e)
        //Navigates to WorkoutEdit Screen
        {
            var item = (ImageButton)sender;
            var model = (Workout)item.CommandParameter;
            App.CurrentID = model.ID;
            await Navigation.PushAsync(new WorkoutPage(-2));
        }

        async public void DeleteButton(object sender, EventArgs e)
        {
            bool response = await DisplayAlert("Delete?", "Are you sure you want to delete this workout?", "Yes", "No");
            if (response)
            {
                var item = (ImageButton)sender;
                var model = (Workout)item.CommandParameter;
                WorkoutList.Remove(model);
                App.WorkoutDB.DeleteWorkout(model.ID);
                OnAppearing();
            }
        }

        async public void AddButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WorkoutPage(-1));
        }

        async public void HistoryButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WorkoutHistory());
        }

        public void Init()
        {
            var enumerator = App.WorkoutDB.GetWorkouts();
            if (enumerator == null)
            {

            }
            else
            {
                while (enumerator.MoveNext())
                {
                    WorkoutList.Add(enumerator.Current);
                }
            }
        }

        protected override void OnAppearing()
        {
            WorkoutList.Clear();
            Init();
            listView.ItemsSource = WorkoutList;
        }

        public StartPage()
        {
            InitializeComponent();
            //App.InProgress = false;
            Init();
            listView.ItemsSource = WorkoutList;
        }
    }
}
