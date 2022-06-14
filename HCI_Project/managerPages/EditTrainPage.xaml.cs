using HCI_Project.popups;
using HCI_Project.utils;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace HCI_Project.managerPages
{
    public partial class EditTrainPage : Page
    {
        public ObservableCollection<Train> Trains;
        public string TrainName { get; set; }
        public int FirstClass { get; set; }
        public int SecondClass { get; set; }
        public EditTrainPage()
        {
            InitializeComponent();
            Trains = new ObservableCollection<Train>(Database.GetTrains());
            TrainComboBox.ItemsSource = Trains;
            DataContext = this;
        }

        private void FirstClassChanged(object sender, TextChangedEventArgs e)
        {
            UpdateGrid();
        }

        private void SecondClassChanged(object sender, TextChangedEventArgs e)
        {
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            int totalSeats = FirstClass + SecondClass;
            int rows = (int)Math.Ceiling(totalSeats / 4.0);
            TogglesGrid.RowDefinitions.Clear();
            TogglesGrid.Children.Clear();
            int count = 1;

            for (int i = 0; i < rows; i++)
            {
                TogglesGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(140) });
                for (int j = 1; j < 5; j++)
                {
                    Button toggle = new Button
                    {
                        Style = count <= FirstClass ? (Style)FindResource("MaterialDesignFloatingActionDarkButton") : (Style)FindResource("MaterialDesignFloatingActionSecondaryButton"),
                        Content = count,
                        FontSize = 40,
                        Height = 120,
                        Width = 120
                    };

                    Grid.SetColumn(toggle, j);
                    Grid.SetRow(toggle, i);
                    TogglesGrid.Children.Add(toggle);

                    count++;
                    if (count > FirstClass + SecondClass)
                        break;
                }
            }
        }

        private void ChangeTrain(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Validation.GetHasError(NameField) ||
                    Validation.GetHasError(FirstClassField) ||
                    Validation.GetHasError(SecondClassField) ||
                    string.IsNullOrEmpty(TrainComboBox.Text))
                {
                    new MessageBoxCustom("You have to fill the form correctly.", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                    return;
                }

                var result = new MessageBoxCustom("You are about to change a train. This action will delete any excess tickets for departures that occur 5 days from now.\nDo you want to continue?", MessageType.Warning, MessageButtons.YesNo).ShowDialog();
                if ((bool)result)
                {
                    var train = TrainComboBox.SelectedItem as Train;
                    train.Name = TrainName;
                    train.FirstClassCapacity = FirstClass;
                    train.SecondClassCapacity = SecondClass;
                    Database.EditTrain(train);

                    new MessageBoxCustom($"You have successfully changed the train {TrainName}.{train.ID}.", MessageType.Success, MessageButtons.Ok).ShowDialog();

                    TrainComboBox.ItemsSource = new ObservableCollection<Train>();
                    TrainComboBox.ItemsSource = Trains;
                }
            }
            catch (Exception ex)
            {
                new MessageBoxCustom("An error has occured.", MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
        }

        private void DeleteTrain(object sender, RoutedEventArgs e)
        {
            try
            {
                var train = TrainComboBox.SelectedItem as Train;
                if (train == null)
                {
                    new MessageBoxCustom("You haven't selected any train.", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                }
                else
                {
                    var result = new MessageBoxCustom("You are about to delete a train. This action is irreversible and will be applied only to departures, train lines and tickets that occur 5 days from now.\nDo you want to continue?", MessageType.Warning, MessageButtons.YesNo).ShowDialog();
                    if ((bool)result)
                    {
                        train.Deleted = true;
                        Database.DeleteTrain(train);
                        TrainComboBox.ItemsSource = Database.GetTrains();
                    }
                }
            }
            catch (Exception ex)
            {
                new MessageBoxCustom("An error has occured.", MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var train = TrainComboBox.SelectedItem as Train;
            if (train == null)
            {
                NameField.Clear();
                FirstClassField.Text = "0";
                SecondClassField.Text = "0";
            }
            else
            {
                NameField.Text = train.Name;
                FirstClassField.Text = train.FirstClassCapacity.ToString();
                SecondClassField.Text = train.SecondClassCapacity.ToString();
            }
        }
    }
}
