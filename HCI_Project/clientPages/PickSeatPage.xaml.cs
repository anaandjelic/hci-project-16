using System.Windows;
using System.Windows.Controls;
using System;
using HCI_Project.utils;
using HCI_Project.utils.display;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace HCI_Project.clientPages
{
    public partial class PickSeatPage : Page
    {
        TimeTableDisplay SelectedTime;
        Station FromStation;
        Station ToStation;
        Frame ClientFrame;

        public PickSeatPage(TimeTableDisplay selection, string from, string to, Frame clientFrame)
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Page));
            SelectedTime = selection;
            ClientFrame = clientFrame;

            FromStation = selection.OriginalTimeTable.TrainLine.getStationByName(from);
            ToStation = selection.OriginalTimeTable.TrainLine.getStationByName(to);

            From.Text = FromStation.Name;
            To.Text = ToStation.Name;
            TrainName.Text = selection.Train;

            UpdateSeats();
        }

        private void ContinueClick(object sender, RoutedEventArgs e)
        {
            if (AvailableSeatsTable.SelectedItem == null)
            {
                MessageBox.Show("You must select the seat before proceeding.");
                return;
            }

            SeatDisplay selectedSeat = (SeatDisplay) AvailableSeatsTable.SelectedItem;

            if (selectedSeat.Occupied)
            {
                MessageBox.Show("The seat you selected is taken! Please select another seat.");
                return;
            }

            ClientFrame.Content = new ConfirmPurchasePage(SelectedTime, selectedSeat, FromStation, ToStation, ClientFrame);
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            ClientFrame.Content = new PurchasePage(ClientFrame);
        }

        private void UpdateSeats()
        {
            List<SeatDisplay> searchRes = Database.GetSeats(SelectedTime.OriginalTimeTable);

            AvailableSeatsTable.ItemsSource = searchRes;
            AvailableSeatsTable.Items.Refresh();
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header)
            {
                case "number":
                    e.Column.Header = "Number";
                    break;
                case "seatClass":
                    e.Column.Header = "Class";
                    break;
            }
        }
    }
}
