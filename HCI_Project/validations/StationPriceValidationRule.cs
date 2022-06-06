using HCI_Project.popups;
using System.Globalization;
using System.Windows.Controls;

namespace HCI_Project.validations
{
    class StationPriceValidationRule : ValidationRule
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
}
