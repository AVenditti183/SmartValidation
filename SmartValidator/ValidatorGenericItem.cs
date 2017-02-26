using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartValidator
{
    class ValidatorGenericItem<TObject>
    {
        public ValidatorGenericItem(Func<TObject, bool> validation, string ErrorMessage)
        {
            this.validation = validation;
            this.ErrorMessage = ErrorMessage;
        }
        public Func<TObject, bool> validation;
        public string ErrorMessage;
    }
}
