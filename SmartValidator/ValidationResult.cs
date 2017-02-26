using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartValidator
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            IsValid = true;
            ErrorMessage = "";
            NumberOfError = 0;
        }
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }
        public short NumberOfError { get; set; }
    }
}
