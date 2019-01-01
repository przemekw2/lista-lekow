using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ListaLekow.Validation_Rules_Classes
{
    class OpakowanieValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null)
                return new ValidationResult(false, "Wartość nie może być pusta.");
            else
            {
                int intValue;
                string strValue = value.ToString();

                if (int.TryParse(strValue, out intValue)){}
                else
                {
                    return new ValidationResult
                    (false, "Wartość musi być liczbą.");
                }

            }
            return ValidationResult.ValidResult;
        }
    }
}
