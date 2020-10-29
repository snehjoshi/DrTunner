using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace finalProject
{
    public class RuleValidation : ValidationRule
    {
        private int minimumValue;
        private int maximumValue;
        private string phone;

        public int MinimumValue { get => minimumValue; set => minimumValue = value; }
        public int MaximumValue { get => maximumValue; set => maximumValue = value; }
        public string Phone { get => phone; set => phone = value; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int tempValidationNumber = 0;
            if (!int.TryParse(value.ToString(), out tempValidationNumber))
            {
                return new ValidationResult(false, "The input should be a number");
            }

            if (tempValidationNumber < minimumValue || tempValidationNumber > maximumValue)
            {
                return new ValidationResult(false, "This number is out of Range");
            }

            if (value.ToString() == "phone") {
                return new ValidationResult(false, "The input should be 10 digits");
            }

            
            return ValidationResult.ValidResult;
        }
    }
}
