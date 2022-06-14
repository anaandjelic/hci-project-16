using HCI_Project.popups;
using HCI_Project.utils;
using HCI_Project.utils.display;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace HCI_Project.managerPages
{
    public partial class EditTimeTablePage : Page
    {
        public ObservableCollection<TrainLineDisplay> TrainLines;
        private TrainLine SelectedTrainLine;

        public EditTimeTablePage()
        {
            InitializeComponent();
            UpdateDataGrid();
            DataContext = this;
        }

        private void ChangeTimeTable(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = new MessageBoxCustom("This change will be applied on departures that leave at least 5 days from now. Do you want to continue?", MessageType.Warning, MessageButtons.YesNo).ShowDialog();
                if (!(bool)result) { return; }


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

                var originalTime = Database.GetConfiguration(SelectedTrainLine).DepartureTime;
                var config = Database.AddConfiguration(SelectedTrainLine, new TimeSpan(TimeField.Time.Hour, TimeField.Time.Minute, 0),
                    (bool)MondayCheck.IsChecked, (bool)TuesdayCheck.IsChecked,
                    (bool)WednesdayCheck.IsChecked, (bool)ThursdayCheck.IsChecked,
                    (bool)FridayCheck.IsChecked, (bool)SaturdayCheck.IsChecked, (bool)SundayCheck.IsChecked);

                for (int i = 0; i < 14; i++)
                {
                    var date = DateTime.Now.AddDays(i).Date;
                    var time = TimeField.Time;
                    date = date.AddHours(time.Hour);
                    date = date.AddMinutes(time.Minute);

                    TrainTimeTable timetable = Database.GetTimeTable(config, date);

                    // new timetable is added
                    if (timetable == null && DateEqualsCheckedDay(date))
                    {
                        Database.AddTimeTable(date, config);
                    }
                    // timetable changes
                    else if (timetable != null && DateEqualsCheckedDay(date))
                    {
                        timetable.DepartureDate = date;
                        Database.ChangeTimeTable(timetable, time - originalTime);
                    }
                    // timetable is deleted
                    else if (timetable != null && DateDoesntEqualCheckedDay(date))
                    {
                        Database.DeleteTimetable(timetable);
                    }
                }


                new MessageBoxCustom("You have sucessfully updated this timetable.", MessageType.Success, MessageButtons.Ok).ShowDialog();
                UpdateDataGrid();
            }
            catch (Exception ex)
            {
                new MessageBoxCustom("An error has occured.", MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
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
        private bool DateDoesntEqualCheckedDay(DateTime date)
        {
            return (date.DayOfWeek == DayOfWeek.Monday && !(bool)MondayCheck.IsChecked) ||
                   (date.DayOfWeek == DayOfWeek.Tuesday && !(bool)TuesdayCheck.IsChecked) ||
                   (date.DayOfWeek == DayOfWeek.Wednesday && !(bool)WednesdayCheck.IsChecked) ||
                   (date.DayOfWeek == DayOfWeek.Thursday && !(bool)ThursdayCheck.IsChecked) ||
                   (date.DayOfWeek == DayOfWeek.Friday && !(bool)FridayCheck.IsChecked) ||
                   (date.DayOfWeek == DayOfWeek.Saturday && !(bool)SaturdayCheck.IsChecked) ||
                   (date.DayOfWeek == DayOfWeek.Sunday && !(bool)SundayCheck.IsChecked);
        }

        private void FillForm()
        {
            SelectedTrainLine = (TrainLineDisplay)TrainLineGrid.SelectedItem == null ? null : ((TrainLineDisplay)TrainLineGrid.SelectedItem).OriginalTrainLine;
            if (SelectedTrainLine == null)
            {
                TimeField.Time = new DateTime();
                MondayCheck.IsChecked = false;
                TuesdayCheck.IsChecked = false;
                WednesdayCheck.IsChecked = false;
                ThursdayCheck.IsChecked = false;
                FridayCheck.IsChecked = false;
                SaturdayCheck.IsChecked = false;
                SundayCheck.IsChecked = false;
            }
            else
            {
                var config = Database.GetConfiguration(SelectedTrainLine);
                TimeField.Time = new DateTime() + config.DepartureTime;
                MondayCheck.IsChecked = config.Monday;
                TuesdayCheck.IsChecked = config.Tuesday;
                WednesdayCheck.IsChecked = config.Wednesday;
                ThursdayCheck.IsChecked = config.Thursday;
                FridayCheck.IsChecked = config.Friday;
                SaturdayCheck.IsChecked = config.Saturday;
                SundayCheck.IsChecked = config.Sunday;
            }
        }

        private void DeleteTimeTable(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = new MessageBoxCustom("This action is irreversible and will impact tickets and departures 5 days from now. Do you want to continue?", MessageType.Warning, MessageButtons.YesNo).ShowDialog();
                if (!(bool)result) { return; }

                SelectedTrainLine = ((TrainLineDisplay)TrainLineGrid.SelectedItem).OriginalTrainLine;
                var config = Database.GetConfiguration(SelectedTrainLine);
                Database.DeleteConfiguration(config);
                UpdateDataGrid();
                FillForm();
            }
            catch (Exception ex)
            {
                new MessageBoxCustom("An error has occured.", MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
        }

        private void SelectionChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            FillForm();
        }

        private void UpdateDataGrid()
        {
            var trainLineDisplays = new List<TrainLineDisplay>();
            Database.GetConfiguredTrainLines().ForEach(x => trainLineDisplays.Add(new TrainLineDisplay(x)));
            TrainLines = new ObservableCollection<TrainLineDisplay>(trainLineDisplays);
            TrainLineGrid.ItemsSource = TrainLines;
        }

        private void SearchTimeTable(object sender, RoutedEventArgs e)
        {
            string enteredValues = SearchTextBox.Text.ToString().Trim().ToLower();
            string[] stationsNames = enteredValues.Split();
            TrainLines = new ObservableCollection<TrainLineDisplay>(Database.SearchConfigured(stationsNames));
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
