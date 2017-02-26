using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartValidator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartValidator.Tests
{
    [TestClass()]
    public class ValidatorTests
    {


        [TestMethod()]
        public void ValidationFalse()
        {
            SmartValidator.Validator.Create();
            SmartValidator.Validator.AddValidation<a>(o => o.id == 1, "No Uno");

            var item = new a();
            item.id = 2;
            var valid = SmartValidator.Validator.Validate<a>(item);

            Assert.IsTrue(!valid.IsValid && valid.ErrorMessage == "No Uno");
        }
        [TestMethod()]
        public void ValidationTrue()
        {
            SmartValidator.Validator.Create();
            SmartValidator.Validator.AddValidation<a>(o => o.id == 1, "No Uno");

            var item = new a();
            item.id = 1;
            var valid = SmartValidator.Validator.Validate<a>(item);

            Assert.IsTrue(valid.IsValid && valid.ErrorMessage == "");
        }

        [TestMethod()]
        public void ValidationDataAnnotationsFalse()
        {
            SmartValidator.Validator.Create();

            var item = new b();
            item.Testo ="12345678901";
            var valid = SmartValidator.Validator.Validate<b>(item);

            Assert.IsTrue(!valid.IsValid && valid.ErrorMessage == "Massimo 10");
        }
        [TestMethod()]
        public void ValidationDataAnnotationsTrue()
        {
            SmartValidator.Validator.Create();

            var item = new b();
            item.Testo = "1234567890";
            var valid = SmartValidator.Validator.Validate<b>(item);

            Assert.IsTrue(valid.IsValid && valid.ErrorMessage == "");
        }


        [TestMethod()]
        public void ValidationMoreValidationFalse()
        {
            SmartValidator.Validator.Create();
            SmartValidator.Validator.AddValidation<a>(o => o.id == 1, "No Uno");
            SmartValidator.Validator.AddValidation<a>(o => o.Testo =="Test", "No Uno");

            var item = new a();
            item.id = 2;
            item.Testo = "";
            var valid = SmartValidator.Validator.Validate<a>(item);

            Assert.IsTrue(!valid.IsValid && valid.NumberOfError ==2);
        }
        [TestMethod()]
        public void ValidationMoreValidationTrue()
        {
            SmartValidator.Validator.Create();
            SmartValidator.Validator.AddValidation<a>(o => o.id == 1, "No Uno");
            SmartValidator.Validator.AddValidation<a>(o => o.Testo == "Test", "No Uno");

            var item = new a();
            item.id = 1;
            item.Testo = "Test";
            var valid = SmartValidator.Validator.Validate<a>(item);

            Assert.IsTrue(valid.IsValid && valid.ErrorMessage == "" && valid.NumberOfError ==0);
        }
        [TestMethod()]
        public void ValidationMoreTypeValidationFalse()
        {
            SmartValidator.Validator.Create();
            SmartValidator.Validator.AddValidation<a>(o => o.id == 1, "No Uno");
            SmartValidator.Validator.AddValidation<a>(o => o.Testo == "Test", "No Uno");

            var item = new a();
            item.id = 2;
            item.Testo = "";
            var valid = SmartValidator.Validator.Validate<a>(item);

            var itemb = new b();
            itemb.Testo = "12345678901";
            var validb = SmartValidator.Validator.Validate<b>(itemb);

            Assert.IsTrue(!valid.IsValid && valid.NumberOfError == 2 && !validb.IsValid && validb.NumberOfError ==1);
        }
        [TestMethod()]
        public void ValidationMoreTypeValidationTrue()
        {
            SmartValidator.Validator.Create();
            SmartValidator.Validator.AddValidation<a>(o => o.id == 1, "No Uno");
            SmartValidator.Validator.AddValidation<a>(o => o.Testo == "Test", "No Uno");

            var item = new a();
            item.id = 1;
            item.Testo = "Test";
            var valid = SmartValidator.Validator.Validate<a>(item);

            var itemb = new b();
            itemb.Testo = "1234567890";
            var validb = SmartValidator.Validator.Validate<b>(itemb);

            Assert.IsTrue(valid.IsValid && valid.ErrorMessage == "" && valid.NumberOfError == 0 && validb.IsValid && validb.NumberOfError ==0);
        }

        [TestMethod()]
        public void ValidationNotInitialize()
        {
            try
            {
                SmartValidator.Validator.Validate <bool>(true);
            }
            catch 
            {
                Assert.IsTrue(true );
            }


        }
    }

    class a
    {
        public int id { get; set; }
        public string Testo { get; set; }
    }

    class b
    {
        public int id { get; set; }

        [StringLength (10,ErrorMessage ="Massimo 10")]
        public string Testo { get; set; }
    }
}