using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Globalization;

namespace ListaLekow.Validation_Rules_Classes
{
    class DawkowanieValidator : ValidationRule
    {

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo) 
        {
            if (value == null)
                return new ValidationResult(false, "Wartość nie może być pusta.");
            else
            {
                double dblValue;
                string strValue = value.ToString();

                if (Double.TryParse(strValue, System.Globalization.NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out dblValue)) { }
                else
                {
                    return new ValidationResult
                    (false, "Wartość musi być liczbą.");
                }
                return ValidationResult.ValidResult;
            }
        }

    }
}
