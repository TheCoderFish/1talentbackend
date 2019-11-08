using System;
using System.ComponentModel.DataAnnotations;

namespace OneTalentResignation.DTO.ValidationAttributes
{
    public class ValidateDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime inputDate = Convert.ToDateTime(value);

            DateTime presentDate = DateTime.Now;

            int result = DateTime.Compare(inputDate, presentDate);

            if (result < 0)
            {
                return new ValidationResult("Date Should be greater than today");
            }
            return ValidationResult.Success;
        }
    }
}
