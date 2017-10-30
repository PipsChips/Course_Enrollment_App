using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseEnrollmentApp.Models.Validations
{
    public class CourseValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is Course)
            {
                var course = (Course)value;

                if (course.MaxNumberOfStudents <= course.CurrentNumOfStudents || course.Available == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}