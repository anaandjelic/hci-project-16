using HCI_Project.popups;
using HCI_Project.utils;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Controls;

namespace HCI_Project.managerPages
{
    public partial class NewTimeTablePage : Page
    {
        public ObservableCollection<TrainLine> TrainLines;
        TrainLine SelectedTrainLine;
        public NewTimeTablePage()
        {
            InitializeComponent();
            TrainLines = new ObservableCollection<TrainLine>(Database.GetTrainLines());
            TrainLineGrid.ItemsSource = TrainLines;
            DataContext = this;
        }

        private void CreateTimeTable(object sender, System.Windows.RoutedEventArgs e)
        {
            SelectedTrainLine = TrainLineGrid.SelectedItem as TrainLine;
            if (SelectedTrainLine == null)
            {
                var errorDialog = new MessageBoxCustom("You have to choose a train line.", MessageType.Error, MessageButtons.Ok);
                errorDialog.ShowDialog();
                return;
            }
            if (!(bool)MondayCheck.IsChecked && !(bool)TuesdayCheck.IsChecked && 
                !(bool)WednesdayCheck.IsChecked && !(bool)ThursdayCheck.IsChecked && 
                !(bool)FridayCheck.IsChecked && !(bool)SaturdayCheck.IsChecked && !(bool)SundayCheck.IsChecked)
            {
                var errorDialog = new MessageBoxCustom("You have to choose at least one day of the week.", MessageType.Error, MessageButtons.Ok);
                errorDialog.ShowDialog();
                return;
            }

            for (int i = 0; i < 14; i++)
            {
                var date = DateTime.Now.AddDays(i);
                var hour = TimeField.Time;
                date.AddHours(hour.Hour);
                date.AddMinutes(hour.Minute);

                if (date.DayOfWeek == DayOfWeek.Monday && (bool)MondayCheck.IsChecked)
                    Database.AddTimeTable(date, new DateTime(), SelectedTrainLine);
                else if (date.DayOfWeek == DayOfWeek.Tuesday && (bool)TuesdayCheck.IsChecked)
                    Database.AddTimeTable(date, new DateTime(), SelectedTrainLine);
                else if (date.DayOfWeek == DayOfWeek.Wednesday && (bool)WednesdayCheck.IsChecked)
                    Database.AddTimeTable(date, new DateTime(), SelectedTrainLine);
                else if (date.DayOfWeek == DayOfWeek.Thursday && (bool)ThursdayCheck.IsChecked)
                    Database.AddTimeTable(date, new DateTime(), SelectedTrainLine);
                else if (date.DayOfWeek == DayOfWeek.Friday && (bool)FridayCheck.IsChecked)
                    Database.AddTimeTable(date, new DateTime(), SelectedTrainLine);
                else if (date.DayOfWeek == DayOfWeek.Saturday && (bool)SaturdayCheck.IsChecked)
                    Database.AddTimeTable(date, new DateTime(), SelectedTrainLine);
                else if (date.DayOfWeek == DayOfWeek.Sunday && (bool)SundayCheck.IsChecked)
                    Database.AddTimeTable(date, new DateTime(), SelectedTrainLine);
            }
            var dialog = new MessageBoxCustom("You have sucessfully created a new timetable.", MessageType.Success, MessageButtons.Ok);
            dialog.ShowDialog();
        }
    }
}
