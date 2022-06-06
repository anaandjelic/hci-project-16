using HCI_Project.popups;
using System;
using System.Globalization;
using System.Windows.Controls;

namespace HCI_Project.validations
{
    class StationTimeValidationRule : ValidationRule
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
