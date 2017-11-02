using CourseEnrollmentApp.Models.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseEnrollmentApp.Models
{
    public class Course
    {
        private bool _available;
        private int _maxNumberOfStudents;
        private int _currentNumOfStudents;

        public int CourseId { get; set; }

        [Display(Name = "Course Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Course Description")]
        [UIHint("Multilinetext")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Starting Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required]
        public DateTime StartingDate { get; set; }

        [Required]
        public int AddressId { get; set; }

        [Display(Name = "Course Location")]
        public Address Address { get; set; }

        [Display(Name = "Number of students")]
        [Required]
        [Range(minimum:3, maximum:30, ErrorMessage = "Min number of students is 3, and max is 30.")]
        public int MaxNumberOfStudents
        {
            get { return this._maxNumberOfStudents; }
            set { this._maxNumberOfStudents = value; }
        }

        public int CurrentNumOfStudents
        {
            get
            {
                return this._currentNumOfStudents;
            }
            set
            {
                this._currentNumOfStudents = value;

                if (this._currentNumOfStudents < 0)
                {
                    this._currentNumOfStudents = 0;
                }
            }
        }

        public bool Available
        {
            get
            {
                if (this._currentNumOfStudents == this._maxNumberOfStudents)
                {
                    return false;
                }
                else
                {
                    return this._available;
                }
            }
            set
            {
                this._available = value;
            }
        }

        [Required]
        public int TeacherId { get; set; }

        public Teacher Teacher { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }

        public Course()
        {
            Enrollments = new HashSet<Enrollment>();
        }
    }
}