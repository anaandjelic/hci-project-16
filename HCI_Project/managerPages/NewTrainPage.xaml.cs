using HCI_Project.utils;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using ToastNotifications;
using ToastNotifications.Core;
using ToastNotifications.Messages;

namespace HCI_Project.managerPages
{
    public partial class NewTrainPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string TrainName { get; set; }
        public int FirstClass { get; set; }
        public int SecondClass { get; set; }
        private bool isTutorialCreateTrain;
        private Frame MainFrame;
        private Notifier managerNotifier;
        private int brojac = 0;
        int brojacPrve = 0;

        public NewTrainPage(bool isTutorialCreateTrain,Frame MainFrame,Notifier notifier)
        {
            InitializeComponent();
            DataContext = this;
            this.isTutorialCreateTrain = isTutorialCreateTrain;
            this.MainFrame = MainFrame;
            this.managerNotifier = notifier;

            if(isTutorialCreateTrain)
            {
                this.FirstClassField.IsEnabled = false;
                this.SecondClassField.IsEnabled = false;
                this.CreateBtn.IsEnabled = false;
                this.cancelTrain.IsEnabled = false;
                string message = "U prvom text polju popunjavate zeljeni naziv voza, npr Koja";
                notifications(message, "Information");
                this.NameField.Focus();
            }
        }

        private void FirstClassChanged(object sender, TextChangedEventArgs e)
        {
            UpdateGrid();

            if (isTutorialCreateTrain)
            {
                bool isNumeric = int.TryParse(FirstClassField.Text.ToString(), out int n);
                if (isNumeric && n>=10)
                {
                    this.FirstClassField.Text = n.ToString();
                    this.FirstClassField.IsEnabled = false;
                    this.SecondClassField.IsEnabled = true;
                    this.SecondClassField.Focus();

                    string message = "U sledecem polju odredjujete broj sedista druge klase. Unos mora biti ceo broj, npr 33";
                    notifications(message, "Information");
                }
            }

        }

        private void SecondClassChanged(object sender, TextChangedEventArgs e)
        {
            UpdateGrid();
            if (isTutorialCreateTrain)
            {
                bool isNumeric = int.TryParse(SecondClassField.Text.ToString(), out int n);
                if (isNumeric && n >= 10)
                {
                    this.SecondClassField.Text = n.ToString();
                    this.SecondClassField.IsEnabled = false;
                    this.CreateBtn.IsEnabled = true;
                    //this.cancelTrain.IsEnabled = true;

                    string message = "U ovom koraku potvrdjujemo kreiranje naseg voza klikom na dugme Create";
                    notifications(message, "Information");
                }
            }
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
            if (isTutorialCreateTrain)
            {
                string message = "Ovim zavrsavamo Tutorijal za kreiranmje vozova";
                notifications(message, "Success");
                MessageBox.Show("Klikom na ok se vracate na login starnicu");
                this.NavigationService.GoBack();
            }
        }

        private void CancelTrain(object sender, RoutedEventArgs e)
        {

        }

        private void notifications(string message, string tip)
        {
            var optionsMax = new MessageOptions
            {
                FontSize = 25,
                FreezeOnMouseEnter = true,
                UnfreezeOnMouseLeave = true
            };
            if (tip == "Success")
                this.managerNotifier.ShowSuccess(message, optionsMax);
            else if (tip == "Information")
                this.managerNotifier.ShowInformation(message, optionsMax);
        }

        private void NameField_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(isTutorialCreateTrain)
            {
                this.brojac++;
                if(this.brojac>=4)
                {
                    this.NameField.Text = "Koja";
                    this.NameField.IsEnabled = false;
                    this.FirstClassField.IsEnabled = true;
                    this.FirstClassField.Focus();
                    this.brojac = 0;

                    string message = "U sledecem polju odredjujete broj sedista prve klase. Unos mora biti ceo broj, npr 23";
                    notifications(message, "Information");
                }
            }
        }
    }
}
