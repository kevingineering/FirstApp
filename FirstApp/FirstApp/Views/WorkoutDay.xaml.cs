using FirstApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FirstApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkoutDay : ContentPage
    {
        //Variables

        private string pauseorplay = "PlaySymbol.png";          //Determines state of pause/play button
        private Workout workout;                                //The workout we are doing
        private int currentSet = 1;                             //Determines which set we are on
        private int currentExercise = 0;                        //Determines which exercise we are on
        private int currentExerciseFollower = 0;                //Keeps track of reps with current Exercise
        private int count = 0;                                  //Determines time on timer
        private bool running = false;                           //Determines if timer is running
        private bool resttime = false;                          //Determines if we are lifting or resting
        private bool starter = false;                           //Stops later functions from beginning until workout has started
        private int TotalReps = 0;                              //Reps completed in the last exercise
        private bool RepIsInt = false;                          //Ensure input is an integer
        private List<Exercise> exerciseList = new List<Exercise>();  //A list of exercises in the workout
        private bool repsFinished = true;                       //Ensures reps chave been input after workout is finished
        private CompletedWorkout completedWorkout = new CompletedWorkout();  //A summary that is assembled when we finish a workout

        //Player for audio effects - reference https://devblogs.microsoft.com/xamarin/adding-sound-xamarin-forms-app/
        private Plugin.SimpleAudioPlayer.ISimpleAudioPlayer player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;

        //Methods

        private async void OnStopButtonClicked(object sender, EventArgs e)
        //Timer stops, message displays
        //if message is confirmed, another message is displayed and we navigate to main page
        //if message is dismissed, timer starts running
        {
            running = false;
            bool response = await DisplayAlert("End Workout?", "Are you sure you want to end your workout? Your progres will not be saved, and this workout will not be included in your workout history.", "Yes", "No");
            if (response)
            {
                //await DisplayAlert("In Progress", "Your workout progress will be saved until the app is closed or a new workout has begun.", "Dismiss");
                await Navigation.PopAsync();
            }
            else
            {
                running = true;
            }
        }

        private void OnPauseButtonClicked(object sender, EventArgs e)
        //Pause/play image changes, timer starts/stops
        {
            if (pauseorplay == "PauseSymbol.png")
            {
                PauseOrPlay.Source = "PlaySymbol.png";
                pauseorplay = "PlaySymbol.png";
                running = false;
            }
            else
            {
                PauseOrPlay.Source = "PauseSymbol.png";
                pauseorplay = "PauseSymbol.png";
                running = true;
            }
        }

        private void OnFastForwardButtonClicked(object sender, EventArgs e)
        //If between sets, timer jumps to five seconds before exercise starts
        //if during set, jumps to rest time
        //if less than five seconds before exercise starts, jumps to exercise
        {
            int temp = exerciseList[currentExercise].Rest - 5;
            if (resttime && count < temp)
            {
                count = temp;
            }
            else if (resttime)
            {
                count = temp + 5;
            }
            else if (starter)
            {
                count = 0;
                resttime = true;
                currentSet++;
                UpdateExercises();
            }
        }

        private void TimedEvent()
        //Runs once a second has methods described within
        {
            int temp;
            
            //determines if resting or lifting, sets timer, adds sound just before and after lifting
            if (resttime)
            {
                temp = exerciseList[currentExercise].Rest - count;
                if (temp <= 3 && temp >= 1)
                {
                    player.Load("Beep.mp3");
                    player.Play();
                }
                if (temp == 0)
                {
                    player.Load("Bell.mp3");
                    player.Play();
                }
            }
            else
            {
                temp = exerciseList[currentExercise].SetTime - count;
                if (temp == 0)
                {
                    player.Load("Bell.mp3");
                    player.Play();
                }
            }
            AppTimer.Text = temp.ToString();

            //If timer is zero, changes from resting to lifting and vice versa, resets timer
            if (temp <= 0)
            {
                if (resttime)
                {
                    resttime = false;
                    count = 0;
                }
                else
                {
                    resttime = true;
                    count = 0;
                    currentSet++;
                    UpdateExercises();
                }
            }

            //Inputs zero if user does not input reps before next exercise
            if (temp == 3 && RepsTotal.IsVisible == true)
            {
                RepsTotal.IsVisible = false;
                exerciseList[currentExerciseFollower].AchievedReps.Add(0);
                currentExerciseFollower = currentExercise;
                repsFinished = true;
            }

            count++;
        }

        private void UpdateExercises()
        //Called when timer hits zero after lifting or fastforward
        //Either progresses one set or moves to next exercise
        //If no more exercises, calls end workout method
        {
            if (starter) //ensure workout has begun, bring up reps input screen
            {

                BlackPage.IsVisible = true;
                RepsTotal.IsVisible = true;
                RepsEntry.Focus();
            }
            if (currentSet > exerciseList[currentExercise].Sets)
            {
                currentSet = 1;
                currentExercise++;
            }
            if (currentExercise >= exerciseList.Count)
            {
                currentExercise = 0;
                EndWorkout();
            }
            else
            {
                Status.Text = exerciseList[currentExercise].Name;
                Weight.Text = "Weight: " + exerciseList[currentExercise].Weight + " lbs";
                Set.Text = "Set: " + (currentSet) + " of " + exerciseList[currentExercise].Sets;
                RepRange.Text = "Goal: " + exerciseList[currentExercise].LowReps + " to " + exerciseList[currentExercise].HighReps + " reps";
            }
        }

        private void RepsCompleted(object sender, EventArgs e)
        //Parses the reps input during the workout and ensures the input value is an int (non-decimal)
        {
            string reps = ((Entry)sender).Text;
            try
            {
                TotalReps = Convert.ToInt32(reps);
                RepIsInt = true;
            }
            catch (Exception ex) { }
        }

        private void RepButtonClicked(object sender, EventArgs e)
        //Records the number of reps to the exercise list 
        {
            if (RepIsInt)
            {
                if(TotalReps < 0)
                {
                    TotalReps = 0;
                }
                RepsTotal.IsVisible = false;
                BlackPage.IsVisible = false;
                exerciseList[currentExerciseFollower].AchievedReps.Add(TotalReps);
                currentExerciseFollower = currentExercise;
                repsFinished = true;
            }
        }

        async void EndWorkout()
        //TODO - Once reps are finished, displays an alert and navigates to summary page
        {
            repsFinished = false;
            while (!repsFinished)
            {
                await Task.Delay(500); //wait for reps to be input
            }
            BlackPage.IsVisible = true;
            MakeCompletedWorkout();
            await DisplayAlert(" ", "You completed your workout! Good job!", "Dismiss");
            //App.InProgress = false;
            await Navigation.PushAsync(new SummaryPage(completedWorkout.ID));
        }

        private void MakeCompletedWorkout()
        //Saves information to database
        {
            completedWorkout.WorkoutID = workout.ID;
            completedWorkout.Date = DateTime.Now;
            completedWorkout.Name = workout.Name;
            List<CompletedExercise> completedExerciseList = new List<CompletedExercise>();
            foreach(Exercise ex in exerciseList)
            {
                completedExerciseList.Add(new CompletedExercise(ex.Name, ex.Weight, ex.AchievedReps));
            }
            completedWorkout.ExerciseListJSON = JsonConvert.SerializeObject(completedExerciseList);
            App.CompletedWorkoutDB.SaveCompletedWorkout(completedWorkout);
        }

        private void StartWorkout()
        //Displays alert that lets someone start a workout
        {
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                if (running)
                {
                    int temp = 5 - count;
                    AppTimer.Text = temp.ToString();
                    count++;
                    if (temp <= 0)
                    {
                        player.Load("Bell.mp3");
                        player.Play();
                        starter = true;
                        count = 0;
                        //App.InProgress = true;
                        return false;
                    }
                    else
                    {
                        if (temp > 0 && temp <= 3)
                        {
                            player.Load("Beep.mp3");
                            player.Play();
                        }
                        return true;

                    }
                }
                else
                {
                    return true;
                }
            });
        }

        public WorkoutDay(int ID)
        //Initializes the page, variables, and timer
        {
            InitializeComponent();
            

            PauseOrPlay.Source = pauseorplay;
            AppTimer.Text = "5";

            workout = App.WorkoutDB.GetWorkout(ID);
            exerciseList = JsonConvert.DeserializeObject<List<Exercise>>(workout.ExerciseListJSON);
            //WorkoutName.Text = workout.Name;

            UpdateExercises();

            StartWorkout();

            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                if (running && starter)
                {
                    TimedEvent();
                }
                if (repsFinished)
                    return true;
                else
                    return false;
            });
        }
    }
}