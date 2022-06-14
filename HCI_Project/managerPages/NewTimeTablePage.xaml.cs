using HCI_Project.popups;
using HCI_Project.utils;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;
using HCI_Project.utils.display;
using System.Collections.Generic;
using ToastNotifications;
using ToastNotifications.Messages;
using ToastNotifications.Core;

namespace HCI_Project.managerPages
{
    public partial class NewTimeTablePage : Page
    {
        public ObservableCollection<TrainLineDisplay> TrainLines;
        private TrainLine SelectedTrainLine;
        private bool isTutorialCreateTrainTable;
        private Frame MainFrame;
        private Notifier managerNotifier;
        private int brojac = 0;

        public NewTimeTablePage(bool isTutorialCreateTrainTable, Frame MainFrame, Notifier notifier)
        {
            InitializeComponent();
            var x = Database.GetUnConfiguredTrainLines();
            UpdateDataGrid();
            DataContext = this;

            this.isTutorialCreateTrainTable = isTutorialCreateTrainTable;
            this.MainFrame = MainFrame;
            this.managerNotifier = notifier;

            if(isTutorialCreateTrainTable)
            {
                this.TimeField.IsEnabled = false;
                this.CreateBtn.IsEnabled = false;
                this.FridayCheck.IsEnabled = false;
                this.MondayCheck.IsEnabled = false;
                this.SaturdayCheck.IsEnabled = false;
                this.SundayCheck.IsEnabled = false;
                this.ThursdayCheck.IsEnabled = false;
                this.TuesdayCheck.IsEnabled = false;
                this.WednesdayCheck.IsEnabled = false;
                this.cancelBUTTON.IsEnabled = false;
                this.SearchTextBox.IsEnabled = false;
                this.SearchBtn.IsEnabled = false;
                string message = "Select your train";
                notifications(message, "Information");
            }
        }

        private void CreateTimeTable(object sender, RoutedEventArgs e)
        {
            try
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

                if(isTutorialCreateTrainTable)
                {
                    string message = "This marks the end of Tutorial";
                    notifications(message, "Success");
                    new MessageBoxCustom("You have sucessfully created a new timetable. By clicking the ok button you will be returned to the last page you were on", MessageType.Success, MessageButtons.Ok).ShowDialog();
                    //this.NavigationService.GoBack();
                    MainFrame.Content = new ManagerPage(MainFrame);
                }
                else
                    new MessageBoxCustom("You have sucessfully created a new timetable.", MessageType.Success, MessageButtons.Ok).ShowDialog();
                ClearForm();
                UpdateDataGrid();
            }
            catch(Exception ex)
            {
                new MessageBoxCustom("There has been an error.", MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
        }

        private void CancelTimeTable(object sender, RoutedEventArgs e)
        {
            var result = new MessageBoxCustom("This action will undo all your changes.Do you want to continue?", MessageType.Warning, MessageButtons.YesNo).ShowDialog();
            if (!(bool)result)
                return;

            ClearForm();
        }

        private void ClearForm()
        {
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

        private void notifications(string message, string tip)
        {
            var optionsMax = new MessageOptions
            {
                FontSize = 30,
                FreezeOnMouseEnter = true,
                UnfreezeOnMouseLeave = true
            };
            if (tip == "Success")
                this.managerNotifier.ShowSuccess(message, optionsMax);
            else if (tip == "Information")
                this.managerNotifier.ShowInformation(message, optionsMax);
        }

        private void TrainLineGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (isTutorialCreateTrainTable)
            {
                if (brojac == 0)
                {
                    string message = "Select two working days";
                    notifications(message, "Information");
                }
                this.TrainLineGrid.IsEnabled = false;
                this.FridayCheck.IsEnabled = true;
                this.MondayCheck.IsEnabled = true;
                //this.SaturdayCheck.IsEnabled = true;
                //this.SundayCheck.IsEnabled = true;
                this.ThursdayCheck.IsEnabled = true;
                this.TuesdayCheck.IsEnabled = true;
                this.WednesdayCheck.IsEnabled = true;
            }
        }

        private void MondayCheck_Checked(object sender, RoutedEventArgs e)
        {
            forCheckBoxes();
            this.MondayCheck.IsEnabled = false;
        }

        private void TuesdayCheck_Checked(object sender, RoutedEventArgs e)
        {
            forCheckBoxes();
            this.TuesdayCheck.IsEnabled = false;
        }

        private void WednesdayCheck_Checked(object sender, RoutedEventArgs e)
        {
            forCheckBoxes();
            this.WednesdayCheck.IsEnabled = false;
        }

        private void ThursdayCheck_Checked(object sender, RoutedEventArgs e)
        {
            forCheckBoxes();
            this.ThursdayCheck.IsEnabled = false;
        }

        private void FridayCheck_Checked(object sender, RoutedEventArgs e)
        {
            forCheckBoxes();
            this.FridayCheck.IsEnabled = false;
        }

        private void forCheckBoxes()
        {
            if (isTutorialCreateTrainTable)
            {
                brojac++;
                if (brojac == 2)
                {
                    string message = "Choose desired time";
                    notifications(message, "Information");

                    this.FridayCheck.IsEnabled = false;
                    this.MondayCheck.IsEnabled = false;
                    //this.SaturdayCheck.IsEnabled = false;
                    //this.SundayCheck.IsEnabled = false;
                    this.ThursdayCheck.IsEnabled = false;
                    this.TuesdayCheck.IsEnabled = false;
                    this.WednesdayCheck.IsEnabled = false;

                    this.TimeField.IsEnabled = true;

                    message = "Click Create button to create train table";
                    notifications(message, "Information");
                    this.CreateBtn.IsEnabled = true;
                }
            }
        }
    }
}
