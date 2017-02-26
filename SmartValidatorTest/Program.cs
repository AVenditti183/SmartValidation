using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartValidatorTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //SmartValidator.Validator.Validate<bool>(true);
            SmartValidator.Validator.Create();
            SmartValidator.Validator.AddValidation<a>(o => o.id == 1, "Uno non trovato");
            SmartValidator.Validator.AddValidation<a>(o => o.Testo == "Prova", "Non Prova");
            SmartValidator.Validator.AddValidation<b>(o => o.Idb == 1, "B non 1");
            var item = new a();
            item.id = 1;
            item.Testo = "Prova";

            var val = SmartValidator.Validator.Validate<a>(item);

            Console.WriteLine (val.IsValid.ToString() + " " + val.ErrorMessage);

            var itemb = new b();
            itemb.Idb = 2;
            var valb = SmartValidator.Validator.Validate<b>(itemb);
            Console.WriteLine(valb.IsValid.ToString() + " " + valb.ErrorMessage);
            Console.ReadKey();
        }
    }

    class a
    {
        public int id { get; set; }
        public string Testo { get; set; }
    }

    class b
    {
        public int Idb { get; set; }
        public DateTime Data { get; set; }
    }

    class modelinput
    {
        [Required]
        public int Id { get; set; }
        [StringLength (20)]
        public string PropertyA { get; set; }
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Error: Min 10 and Max 50 characters") ]
        public string PropertyB { get; set; }
        public List<int> CollectionProperty { get; set; }
    }

    class Main
    {
        public void InsertData(modelinput item)
        {
            SmartValidator.Validator.Create();
            SmartValidator.Validator.AddValidation<modelinput>(o => (o.PropertyA == "Test" && o.CollectionProperty.Count <= 10 && o.CollectionProperty.Any( e => e == 5) ) || o.PropertyB == "Demo", "Max 10 Elements");

            if (SmartValidator.Validator.Validate<modelinput>(item).IsValid)
            {
                // add here logic

            }
        }
    }
}
