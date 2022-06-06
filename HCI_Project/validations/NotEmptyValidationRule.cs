using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace HCI_Project.validations
{

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
}
