using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProject.ViewModels
{
    public class BehaviorEditModel
    {
        [Required(ErrorMessage = "Student Name required")]
        public string StudentName { get; set; }
        [Required(ErrorMessage = "Description of behavior required")]
        public string BehaviorName { get; set; }
        [Required(ErrorMessage = "A value is required")]
        public int Value { get; set; }
    }
}
