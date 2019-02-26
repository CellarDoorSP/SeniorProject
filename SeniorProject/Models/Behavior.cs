using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProject.Models
{
    public class Behavior
    {
        public int Id { get; set; }
        [Display(Name = "Behavior Description")]
        [Required(ErrorMessage = "Description of behavior required")]
        public string BehaviorName { get; set; }
        [Display(Name = "Behavior Value")]
        [Required(ErrorMessage = "A value is required")]
        public int Value { get; set; }
        [Display(Name = "Student Name")]
        [Required(ErrorMessage = "Student Name required")]
        public string StudentName { get; set; }
    }
}
