using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartValidator
{
    public static class Validator

    {
        private static Dictionary<Type,object > dic;

        public static void Create()
        {
            dic = new Dictionary<Type  ,object >();
        }

        public static void AddValidation<TObject>(Func<TObject , bool> validation,string ErrorMessage)
        {
            if (dic == null)
                throw new Exception("Smart Validator Not initialize");
            if ( dic.ContainsKey(typeof( TObject) ))
            {
              ((ValidatorGeneric<TObject>)  dic[typeof(TObject)]).Add(validation,ErrorMessage );
            }
            else
            {
                dic.Add(typeof(TObject), new ValidatorGeneric<TObject>( validation,ErrorMessage  ));
            }

        }

        public static ValidationResult Validate<TObject>(TObject Item)
        {
            if (dic == null)
                throw new Exception("Smart Validator Not initialize");
            return dic.ContainsKey(typeof(TObject)) ? ((ValidatorGeneric<TObject>)dic[typeof(TObject)]).Validate(Item) : new ValidatorGeneric<TObject>().ValidateOnlyDataAnnotations(Item);
        }

    }

    

    

   
}
