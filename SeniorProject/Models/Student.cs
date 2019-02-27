using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProject.Models
{
    public class Student
    {
        [Display(Name = "Student Name")]
        [Required(ErrorMessage = "Name is required")]
        public string StudentName { get; set; }
        public int LifetimeTotal { get; set; }
        public int CurrentTotal { get; set; }

        public int GraphValue { get; set; }
        [Key]
        public int Id { get; set; }
        public string User { get; set; }
    }
}
