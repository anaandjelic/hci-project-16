using System.Windows;
using System.Windows.Controls;
using System;
using HCI_Project.utils;
using HCI_Project.utils.display;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Controls.Primitives;
using System.Linq;

namespace HCI_Project.clientPages
{
    public partial class PickSeatPage : Page
    {
        TimeTableDisplay SelectedTime;
        Station FromStation;
        Station ToStation;
        Frame ClientFrame;
        List<ToggleButton> seats = new List<ToggleButton>();

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

            DrawSeats();
        }

        private void ContinueClick(object sender, RoutedEventArgs e)
        {
            var selected = seats.Where(x => (bool)x.IsChecked).FirstOrDefault() as ToggleButton;
            if (selected == null)
            {
                MessageBox.Show("You must select the seat before proceeding.");
                return;
            }

            SeatDisplay selectedSeat = new SeatDisplay(int.Parse(selected.Content.ToString()), int.Parse(selected.Content.ToString()) < SelectedTime.OriginalTimeTable.TrainLine.Train.FirstClassCapacity ? "first" : "second", true);


            ClientFrame.Content = new ConfirmPurchasePage(SelectedTime, selectedSeat, FromStation, ToStation, ClientFrame);
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to cancel the process of buying/reserving a ticket? Your progress will be lost.", "Confirm cancellation", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                ClientFrame.Content = new PurchasePage(ClientFrame);
        }

        private void DrawSeats()
        {
            var train = Database.GetTrain(SelectedTime.Train);
            int totalSeats = train.FirstClassCapacity + train.SecondClassCapacity;
            int rows = (int)Math.Ceiling(totalSeats / 4.0);
            TogglesGrid.RowDefinitions.Clear();
            TogglesGrid.Children.Clear();
            int count = 1;

            for (int i = 0; i < rows; i++)
            {
                TogglesGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(140) });
                for (int j = 1; j < 5; j++)
                {
                    ToggleButton toggle = new ToggleButton
                    {
                        Style = count <= train.FirstClassCapacity ? (Style)FindResource("MaterialDesignFlatPrimaryToggleButton") : (Style)FindResource("MaterialDesignFlatToggleButton"),
                        Content = count,
                        IsChecked = Database.SeatIsFree(i, j, SelectedTime),
                        FontSize = 40,
                        Height = 120,
                        Width = 120
                    };

                    Grid.SetColumn(toggle, j);
                    Grid.SetRow(toggle, i);

                    seats.Add(toggle);
                    TogglesGrid.Children.Add(toggle);

                    count++;
                    if (count > train.FirstClassCapacity + train.SecondClassCapacity)
                        break;
                }
            }
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
