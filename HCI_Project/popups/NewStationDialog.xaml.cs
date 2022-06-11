using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ToastNotifications;
using ToastNotifications.Core;
using ToastNotifications.Messages;

namespace HCI_Project.popups
{
    public partial class NewStationDialog : Window, INotifyPropertyChanged
    {
        public static double LastPrice;
        public static TimeSpan LastTime;
        private double _Price;
        private TimeSpan _Time;
        public string StationName { get; set; }

        private Notifier managerNotifier;
        private bool isTutorialCreateTrainLine;

        public double Price
        {
            get => PriceField.IsEnabled && PriceField.Text.Length != 0 ? _Price : 0;
            set => _Price = value;
        }
        public TimeSpan Time
        {
            get => TimeField.IsEnabled && TimeField.SelectedTime != null ? _Time : new TimeSpan(0, 0, 0);
            set => _Time = value;
        }

        public NewStationDialog(bool isFirstStation, double lastPrice, TimeSpan lastTime, bool isTutorialCreateTrainLine, Notifier notifier)
        {
            InitializeComponent();
            DataContext = this;
            LastPrice = lastPrice;
            LastTime = lastTime;
            NameField.Focus();

            this.isTutorialCreateTrainLine = isTutorialCreateTrainLine;
            this.managerNotifier = notifier;

            if (isFirstStation)
            {
                if(isTutorialCreateTrainLine)
                {
                    string message = "Uneti naziv stanice";
                    notifications(message, "Information");

                    message = "Pritisnuti Confirm dugme za nastavak";
                    notifications(message, "Information");
                }
                TimeField.IsEnabled = false;
                Binding binding = BindingOperations.GetBinding(TimeField, TimePicker.TextProperty);
                binding.ValidationRules.Clear();
                PriceField.IsEnabled = false;
                binding = BindingOperations.GetBinding(PriceField, TextBox.TextProperty);
                binding.ValidationRules.Clear();

            }
            else
            {
                if(isTutorialCreateTrainLine)
                {
                    string message = "Uneti naziv stanice, cenu puta i vreme trajanja voznje";
                    notifications(message, "Information");

                    message = "Pritisnuti Confirm dugme za nastavak";
                    notifications(message, "Information");
                }
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(NameField) ||
               (Validation.GetHasError(PriceField) && PriceField.IsEnabled) ||
               (Validation.GetHasError(TimeField) && TimeField.IsEnabled))
            {
                return;
            }
            DialogResult = true;
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

    }
}
