using HCI_Project.popups;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace HCI_Project.validations
{
    class PositiveNumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrWhiteSpace((value ?? "").ToString()))
            {
                return new ValidationResult(false, "Field is required.");
            }
            else if (!((string)value).All(char.IsDigit))
            {
                return new ValidationResult(false, $"Must be a number.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
