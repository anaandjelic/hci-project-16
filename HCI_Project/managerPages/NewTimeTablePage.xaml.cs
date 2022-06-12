using HCI_Project.popups;
using HCI_Project.utils;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;
using HCI_Project.utils.display;
using System.Collections.Generic;

namespace HCI_Project.managerPages
{
    public partial class NewTimeTablePage : Page
    {
        public ObservableCollection<TrainLineDisplay> TrainLines;
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
            SelectedTrainLine = (TrainLineDisplay)TrainLineGrid.SelectedItem == null ? null : ((TrainLineDisplay)TrainLineGrid.SelectedItem).OriginalTrainLine;
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
                    Database.AddTimeTable(date, config);
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
            var trainLineDisplays = new List<TrainLineDisplay>();
            Database.GetUnConfiguredTrainLines().ForEach(x => trainLineDisplays.Add(new TrainLineDisplay(x)));
            TrainLines = new ObservableCollection<TrainLineDisplay>(trainLineDisplays);
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

        private void SearchTimeTable(object sender, RoutedEventArgs e)
        {
            string enteredValues = SearchTextBox.Text.ToString().Trim().ToLower();
            string[] stationsNames = enteredValues.Split();
            TrainLines = new ObservableCollection<TrainLineDisplay>(Database.SearchUnConfigured(stationsNames));
            TrainLineGrid.ItemsSource = TrainLines;
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.TimeSpan))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "hh\\:mm";
            switch (e.Column.Header)
            {
                case "TravelTime":
                    e.Column.Header = "Travel time";
                    break;
                case "OriginalTrainLine":
                    e.Column.Visibility = Visibility.Hidden;
                    break;
            }
        }
    }
}
