using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FirstApp.Data;

namespace FirstApp
{
    public partial class App : Application
    {
        public static int CurrentID { get; set; }
        //public static bool InProgress { get; set; }
        static WorkoutDBController WorkoutDatabase;
        static CompletedWorkoutDBController CompletedWorkoutDatabase;
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new StartPage());
        }

        public static WorkoutDBController WorkoutDB
        {
            get
            {
                if (WorkoutDatabase == null)
                {
                    WorkoutDatabase = new WorkoutDBController();
                }
                return WorkoutDatabase;
            }
        }

        public static CompletedWorkoutDBController CompletedWorkoutDB
        {
            get
            {
                if (CompletedWorkoutDatabase == null)
                {
                    CompletedWorkoutDatabase = new CompletedWorkoutDBController();
                }
                return CompletedWorkoutDatabase;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
