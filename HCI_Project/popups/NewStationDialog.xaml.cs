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

namespace HCI_Project.popups
{
    public partial class NewStationDialog : Window, INotifyPropertyChanged
    {
        public static double LastPrice;
        public static TimeSpan LastTime;
        private double _Price;
        private TimeSpan _Time;
        public string StationName { get; set; }
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

        public NewStationDialog(bool isFirstStation, double lastPrice, TimeSpan lastTime)
        {
            InitializeComponent();
            DataContext = this;
            LastPrice = lastPrice;
            LastTime = lastTime;
            NameField.Focus();
            if (isFirstStation)
            {
                TimeField.IsEnabled = false;
                Binding binding = BindingOperations.GetBinding(TimeField, TimePicker.TextProperty);
                binding.ValidationRules.Clear();
                PriceField.IsEnabled = false;
                binding = BindingOperations.GetBinding(PriceField, TextBox.TextProperty);
                binding.ValidationRules.Clear();

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

        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string fullText = textBox.Text.Insert(textBox.SelectionStart, e.Text);
            e.Handled = !double.TryParse(fullText, out _);
        }
    }

    public class NotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrWhiteSpace((value ?? "").ToString()))
            {
                return new ValidationResult(false, "Field is required.");
            }
            else if (!Regex.IsMatch((string)value, @"\A[\p{L}\s]+\Z"))
            {
                return new ValidationResult(false, "Must contain only letters.");
            }
            return ValidationResult.ValidResult;
        }
    }

    public class StationPriceValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrWhiteSpace((value ?? "").ToString()))
            {
                return new ValidationResult(false, "Field is required.");
            }
            else if (!double.TryParse((string)value, out _))
            {
                return new ValidationResult(false, $"Must be a number.");
            }
            else if (double.Parse(value.ToString()) <= NewStationDialog.LastPrice)
            {
                return new ValidationResult(false, $"Must be greater than {NewStationDialog.LastPrice}.");
            }
            return ValidationResult.ValidResult;
        }
    }

    public class StationTimeValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrWhiteSpace((value ?? "").ToString()))
            {
                return new ValidationResult(false, "Field is required.");
            }
            else if (TimeSpan.Parse(value.ToString()) <= NewStationDialog.LastTime)
            {
                return new ValidationResult(false, $"Must be greater than {TimeSpan.Parse(NewStationDialog.LastTime.ToString())}.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
