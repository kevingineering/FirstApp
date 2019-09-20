using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

//For reference: https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/behaviors/creating

namespace FirstApp.Models
{
    public class UnitsBehavior : Behavior<Entry> 
    {
        protected override void OnAttachedTo(Entry entry) //Sets up our behavior
        {
            entry.Completed += OnEntryTextCompleted;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry) //Cleans up our behavior
        {
            entry.Completed -= OnEntryTextCompleted;
            base.OnDetachingFrom(entry);
        }

        private void OnEntryTextCompleted(object sender, EventArgs args)
        {
            //((Entry)sender).Text = "";
        }
    }
}
