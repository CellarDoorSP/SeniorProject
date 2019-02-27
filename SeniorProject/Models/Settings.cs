using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProject.Models
{
    public class Settings
    {
        [Key]
        public string User { get; set; }
        public int MainGoal { get; set; }
        public int ExtraGoal { get; set; }
    }
}
