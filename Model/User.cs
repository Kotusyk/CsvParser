using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Model
{
    public class User
    {
        [CsvHelper.Configuration.Attributes.Ignore]
        public int Id { get; set; }
        public string Name { get; set; }

        [DateOfBirth(MinAge = 0, MaxAge = 120)]
        public DateTime DateOfBirth { get; set; }
        public bool Married { get; set; }
        [MaxLength(10)]
        public string Phone { get; set; }
        public decimal Salary { get; set; }
    }
    public class DateOfBirthAttribute : ValidationAttribute
    {
        public int MinAge { get; set; }
        public int MaxAge { get; set; }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            var val = (DateTime)value;

            if (val.AddYears(MinAge) > DateTime.Now)
                return false;

            return (val.AddYears(MaxAge) > DateTime.Now);
        }
    }
}
