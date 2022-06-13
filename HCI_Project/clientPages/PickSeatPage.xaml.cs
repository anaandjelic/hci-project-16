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
        AvailableSeatDisplay SelectedSeat;
        Frame ClientFrame;

        public PickSeatPage(TimeTableDisplay selection, string from, string to, Frame clientFrame)
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Page));
            SelectedTime = selection;
            ClientFrame = clientFrame;
            
            From.Text = selection.OriginalTimeTable.TrainLine.getStationByName(from).Name;
            To.Text = selection.OriginalTimeTable.TrainLine.getStationByName(to).Name;

            UpdateSeats();
        }

        private void ContinueClick(object sender, RoutedEventArgs e)
        {
            if (AvailableSeatsTable.SelectedItem == null)
            {
                MessageBox.Show("You must select the seat before proceeding.");
                return;
            }

            AvailableSeatDisplay selectedSeat = (AvailableSeatDisplay) AvailableSeatsTable.SelectedItem;
            MessageBox.Show($"Odabro si {selectedSeat.number} svaka cast");
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            ClientFrame.Content = new PurchasePage(ClientFrame);
        }

        private void UpdateSeats()
        {
            List<AvailableSeatDisplay> searchRes = Database.GetAvailableSeats(SelectedTime.OriginalTimeTable);

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
