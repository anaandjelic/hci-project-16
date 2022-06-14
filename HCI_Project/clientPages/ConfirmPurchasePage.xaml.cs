using System.Windows;
using System.Windows.Controls;
using System;
using HCI_Project.utils;
using HCI_Project.utils.display;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace HCI_Project.clientPages
{
    public partial class ConfirmPurchasePage : Page
    {
        TimeTableDisplay SelectedTime;
        SeatDisplay SelectedSeat;
        Station FromStation;
        Station ToStation;
        Frame ClientFrame;

        public ConfirmPurchasePage(TimeTableDisplay selectedTime, SeatDisplay selectedSeat, Station fromStation, Station toStation, Frame clientFrame)
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Page));
            SelectedTime = selectedTime;
            SelectedSeat = selectedSeat;
            FromStation = fromStation;
            ToStation = toStation;
            ClientFrame = clientFrame;

            FillLabels();
        }

        private void FillLabels()
        {
            From.Text = FromStation.Name;
            To.Text = ToStation.Name;
            Date.Text = SelectedTime.Departure.ToLongDateString();
            Departure.Text = SelectedTime.Departure.ToShortTimeString();
            Arive.Text = SelectedTime.Arrival.ToShortTimeString();
            Seat.Text = $"{SelectedSeat.Number}, {SelectedSeat.SeatClass} class";
            TrainName.Text = SelectedTime.Train;
            Price.Text = $"{SelectedTime.Price} RSD";
        }

        private void ConfirmReserve(object sender, RoutedEventArgs e)
        { 
            Database.AddTicket(SelectedTime.Price, SelectedSeat.Number, SelectedSeat.SeatClass, false, 
                FromStation, ToStation, SelectedTime.Departure, SelectedTime.Arrival, SelectedTime.OriginalTimeTable);
            MessageBox.Show("Reservation successful!");

            ClientFrame.Content = new TicketsPage(ClientFrame);
        }

        private void ConfirmPurchase(object sender, RoutedEventArgs e)
        {
            Database.AddTicket(SelectedTime.Price, SelectedSeat.Number, SelectedSeat.SeatClass, true,
                FromStation, ToStation, SelectedTime.Departure, SelectedTime.Arrival, SelectedTime.OriginalTimeTable);
            MessageBox.Show("Purchase successful!");
            ClientFrame.Content = new TicketsPage(ClientFrame);
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            ClientFrame.Content = new PurchasePage(ClientFrame);
        }
    }
}
