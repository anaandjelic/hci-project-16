﻿using HCI_Project.utils;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCI_Project.managerPages
{
    /// <summary>
    /// Interaction logic for NewTrainPage.xaml
    /// </summary>
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
            int count = 1;

            for (int i = 0; i < rows; i++)
            {
                TogglesGrid.RowDefinitions.Add(new RowDefinition());
                for (int j = 0; j < 4; j++)
                {
                    PackIcon icon = new PackIcon
                    {
                        Kind = count <= FirstClass ? PackIconKind.Ticket : PackIconKind.TicketOutline
                    };
                    StackPanel stackPanel = new StackPanel()
                    {
                        Orientation = Orientation.Horizontal,
                        Children = { icon, new TextBlock() { Text = count.ToString() } }
                    };
                    ToggleButton toggle = new ToggleButton
                    {
                        Style = Resources["MaterialDesignActionToggleButton"] as Style,
                        Content = stackPanel
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
    }
}
