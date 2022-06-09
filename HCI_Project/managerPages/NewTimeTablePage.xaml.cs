using HCI_Project.popups;
using HCI_Project.utils;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;

namespace HCI_Project.managerPages
{
    public partial class NewTimeTablePage : Page
    {
        public ObservableCollection<TrainLine> TrainLines;
        private TrainLine SelectedTrainLine;

        public NewTimeTablePage()
        {
            InitializeComponent();
            var x = Database.GetUnConfiguredTrainLines();
            UpdateDataGrid();
            DataContext = this;
        }

        private void CreateTimeTable(object sender, RoutedEventArgs e)
        {
            SelectedTrainLine = TrainLineGrid.SelectedItem as TrainLine;
            if (SelectedTrainLine == null)
            {
                new MessageBoxCustom("You have to choose a train line.", MessageType.Error, MessageButtons.Ok).ShowDialog();
                return;
            }
            if (!(bool)MondayCheck.IsChecked && !(bool)TuesdayCheck.IsChecked && 
                !(bool)WednesdayCheck.IsChecked && !(bool)ThursdayCheck.IsChecked && 
                !(bool)FridayCheck.IsChecked && !(bool)SaturdayCheck.IsChecked && !(bool)SundayCheck.IsChecked)
            {
                new MessageBoxCustom("You have to choose at least one day of the week.", MessageType.Error, MessageButtons.Ok).ShowDialog();
                return;
            }

            var config = Database.AddConfiguration(SelectedTrainLine, new TimeSpan(TimeField.Time.Hour, TimeField.Time.Minute, 0), 
                (bool)MondayCheck.IsChecked, (bool)TuesdayCheck.IsChecked, 
                (bool)WednesdayCheck.IsChecked, (bool)ThursdayCheck.IsChecked, 
                (bool)FridayCheck.IsChecked, (bool)SaturdayCheck.IsChecked, (bool)SundayCheck.IsChecked);

            for (int i = 0; i < 14; i++)
            {
                var date = DateTime.Now.AddDays(i).Date;
                var hour = TimeField.Time;
                date = date.AddHours(hour.Hour);
                date = date.AddMinutes(hour.Minute);

                if (DateEqualsCheckedDay(date))
                    Database.AddTimeTable(date, new DateTime(), SelectedTrainLine, config);
            }

            new MessageBoxCustom("You have sucessfully created a new timetable.", MessageType.Success, MessageButtons.Ok).ShowDialog();
            UpdateDataGrid();
        }

        private void CancelTimeTable(object sender, RoutedEventArgs e)
        {
            var result = new MessageBoxCustom("This action will undo all your changes.\nDo you want to continue?", MessageType.Warning, MessageButtons.YesNo).ShowDialog();
            if (!(bool)result)
                return;

            TimeField.Time = new DateTime();
            MondayCheck.IsChecked = false;
            TuesdayCheck.IsChecked = false;
            WednesdayCheck.IsChecked = false;
            ThursdayCheck.IsChecked = false;
            FridayCheck.IsChecked = false;
            SaturdayCheck.IsChecked = false;
            SundayCheck.IsChecked = false;
            SelectedTrainLine = null;
            TrainLineGrid.UnselectAll();
        }

        private void UpdateDataGrid()
        {
            TrainLines = new ObservableCollection<TrainLine>(Database.GetUnConfiguredTrainLines());
            TrainLineGrid.ItemsSource = TrainLines;
        }

        private bool DateEqualsCheckedDay(DateTime date)
        {
            return (date.DayOfWeek == DayOfWeek.Monday && (bool)MondayCheck.IsChecked) ||
                   (date.DayOfWeek == DayOfWeek.Tuesday && (bool)TuesdayCheck.IsChecked) ||
                   (date.DayOfWeek == DayOfWeek.Wednesday && (bool)WednesdayCheck.IsChecked) ||
                   (date.DayOfWeek == DayOfWeek.Thursday && (bool)ThursdayCheck.IsChecked) ||
                   (date.DayOfWeek == DayOfWeek.Friday && (bool)FridayCheck.IsChecked) ||
                   (date.DayOfWeek == DayOfWeek.Saturday && (bool)SaturdayCheck.IsChecked) ||
                   (date.DayOfWeek == DayOfWeek.Sunday && (bool)SundayCheck.IsChecked);
        }
    }
}
