using SeniorProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProject.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Behavior> Behaviors { get; set; }

    }
}
