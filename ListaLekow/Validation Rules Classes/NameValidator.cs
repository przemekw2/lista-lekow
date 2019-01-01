using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ListaLekow.Validation_Rules_Classes
{
    public class NameValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null)
                return new ValidationResult(false, "Wartość nie może być pusta.");
            else
            {
                if (value.ToString().Length < 3)
                    return new ValidationResult
                    (false, "Nazwa leku musi mieć minimum 3 znaki.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
