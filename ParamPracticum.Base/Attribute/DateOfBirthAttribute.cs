﻿using System.ComponentModel.DataAnnotations;

namespace ParamPracticum.Base
{
    public class DateOfBirthAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                DateTime date = Convert.ToDateTime(value);
                var max = DateTime.Now.AddYears(-15);
                var min = DateTime.Now.AddYears(-100);
                var msg = string.Format($"Please enter a value between {min:MM/dd/yyyy} and {max:MM/dd/yyyy}");
                if (date < min || date > max)
                    return new ValidationResult(msg);
                else
                    return ValidationResult.Success;
            }
            catch (Exception)
            {
                return new ValidationResult("Invalid Date of Birth");
            }
        }
    }
}
