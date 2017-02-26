using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartValidator
{
    class ValidatorGeneric<TObject>
    {
        private List<ValidatorGenericItem<TObject>> lst;
        public ValidatorGeneric()
        {
            lst = new List<ValidatorGenericItem<TObject>>();
        }
        public ValidatorGeneric(Func<TObject, bool> validation, string messageError)
        {
            lst = new List<ValidatorGenericItem<TObject>>();
            lst.Add(new SmartValidator.ValidatorGenericItem<TObject>(validation, messageError));
        }
        public void Add(Func<TObject, bool> validation, string ErrorMessage)
        {
            lst.Add(new SmartValidator.ValidatorGenericItem<TObject>(validation, ErrorMessage));
        }

        public ValidationResult Validate(TObject item)
        {
            var valid = new ValidationResult();

            foreach (var validation in lst)
            {
                if (!validation.validation(item))
                {
                    valid.ErrorMessage += (valid.ErrorMessage.Length > 0 ? " " : "") + validation.ErrorMessage;
                    valid.IsValid = false;
                    valid.NumberOfError++;
                }
            }

            ValidateDataAnnotations(item, valid);

            return valid;
        }
        private ValidationResult ValidateDataAnnotations(TObject item, ValidationResult valid)
        {
            var resultDA = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            System.ComponentModel.DataAnnotations.Validator.TryValidateObject(item, new System.ComponentModel.DataAnnotations.ValidationContext(item), resultDA, true);

            foreach (var validationDA in resultDA)
            {
                valid.ErrorMessage += (valid.ErrorMessage.Length > 0 ? " " : "") + validationDA.ErrorMessage;
                valid.IsValid = false;
                valid.NumberOfError++;
            }
            return valid;
        }

        public ValidationResult ValidateOnlyDataAnnotations(TObject item)
        {

            return ValidateDataAnnotations(item, new ValidationResult());
        }

    }
}
