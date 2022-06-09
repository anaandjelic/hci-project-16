using HCI_Project.utils;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace HCI_Project.managerPages
{
    public partial class NewTrainPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string TrainName { get; set; }
        public int FirstClass { get; set; }
        public int SecondClass { get; set; }
        public NewTrainPage()
        {
            InitializeComponent();
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
                    /*
                    ToggleButton toggle = new ToggleButton
                    {
                        Style = count <= FirstClass ? (Style)FindResource("MaterialDesignFlatPrimaryToggleButton") : (Style)FindResource("MaterialDesignFlatToggleButton"),
                        Content = count,
                        IsChecked = true,
                        FontSize = 40,
                        Height = 120,
                        Width = 120
                    };
                    */
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

        private void CreateTrain(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(NameField) ||
               Validation.GetHasError(FirstClassField) ||
               Validation.GetHasError(SecondClassField))
            {
                return;
            }
            Database.AddTrain(TrainName, FirstClass, SecondClass);
        }

        private void CancelTrain(object sender, RoutedEventArgs e)
        {

        }
    }
}
