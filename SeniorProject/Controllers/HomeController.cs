using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models;
using SeniorProject.Services;
using SeniorProject.ViewModels;

namespace SeniorProject.Controllers
{
    public class HomeController : Controller
    {
        private IStudentData _studentData;
        private IBehaviorData _behaviorData;
        //private ISettingsData _settingsData;

        public HomeController(IStudentData studentData, IBehaviorData behaviorData) // ISettingsData settingsData)
        {
            _studentData = studentData;
            _behaviorData = behaviorData;
            //_settingsData = settingsData;
        }

        public IActionResult Index()
        {
            var model = new HomeIndexViewModel();
            //model.Students = _studentData.GetAll();
            model.Students = _studentData.GetAllFromUser(User.Identity.Name);
            model.Behaviors = _behaviorData.GetAll();

            return View(model);
        }

        [HttpGet]
        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStudent(StudentEditModel model)
        {
            if(User.Identity.Name == null)
            {
                ViewBag.Message = "Must be logged in";
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                if (_studentData.UserContains(User.Identity.Name, model.StudentName))
                {
                    ViewBag.Message = "Cannot have duplicate student names";
                    return View();
                }

                var newStudent = new Student();
                newStudent.StudentName = model.StudentName;
                newStudent.User = User.Identity.Name;                

                newStudent = _studentData.Add(newStudent);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult DeleteStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteStudent(StudentEditModel model)
        {
            if (User.Identity.Name == null)
            {
                ViewBag.Message = "Must be logged in";
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                if (!_studentData.UserContains(User.Identity.Name, model.StudentName))
                {
                    ViewBag.Message = "Student must already be added";
                    return View();
                }

                int studentId = _studentData.GetStudentId(User.Identity.Name, model.StudentName);               

                foreach (var behavior in _behaviorData.GetAll())
                {       
                    if (behavior.StudentId == _studentData.GetStudentId(User.Identity.Name, model.StudentName))
                    {
                        _studentData.EditDeleteCurrentTotal(User.Identity.Name, model.StudentName, _behaviorData.GetByName(behavior.BehaviorName, studentId).Value);
                        _studentData.EditDeleteLifetimeTotal(User.Identity.Name, model.StudentName, _behaviorData.GetByName(behavior.BehaviorName, studentId).Value);
                        
                        _behaviorData.Delete(behavior.BehaviorName, studentId);
                    }
                }

                _studentData.Delete(User.Identity.Name, model.StudentName);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult AddBehavior()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBehavior(BehaviorEditModel model)
        {
            if (User.Identity.Name == null)
            {
                ViewBag.Message = "Must be logged in";
                return RedirectToAction(nameof(Index));
            }

            if (!_studentData.UserContains(User.Identity.Name, model.StudentName))
            {
                ViewBag.Message = "Student name must already be added";
                return View();
            }

            if (ModelState.IsValid)
            {
                var newBehavior = new Behavior();
                newBehavior.BehaviorName = model.BehaviorName;
                newBehavior.StudentName = model.StudentName;
                newBehavior.StudentId = _studentData.GetStudentId(User.Identity.Name, model.StudentName);
                newBehavior.Value = model.Value;

                newBehavior = _behaviorData.Add(newBehavior);

                _studentData.EditAddCurrentTotal(User.Identity.Name, model.StudentName, model.Value);
                _studentData.EditAddLifetimeTotal(User.Identity.Name, model.StudentName, model.Value);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult DeleteBehavior()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteBehavior(BehaviorEditModel model)
        {
            if (User.Identity.Name == null)
            {
                ViewBag.Message = "Must be logged in";
                return RedirectToAction(nameof(Index));
            }

            if (!_studentData.UserContains(User.Identity.Name, model.StudentName))
            {
                ViewBag.Message = "Student name must already be added";
                return View();
            }            

            if (ModelState.IsValid)
            {
                int studentId = _studentData.GetStudentId(User.Identity.Name, model.StudentName);

                if (_behaviorData.GetByName(model.BehaviorName, studentId) == null)
                {
                    ViewBag.Message = "Behavior must already be added";
                    return View();
                }

                _studentData.EditDeleteCurrentTotal(User.Identity.Name, model.StudentName, _behaviorData.GetByName(model.BehaviorName, studentId).Value);
                _studentData.EditDeleteLifetimeTotal(User.Identity.Name, model.StudentName, _behaviorData.GetByName(model.BehaviorName, studentId).Value);

                _behaviorData.Delete(model.BehaviorName, studentId);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        public IActionResult ResetCurrent()
        {
            _studentData.ResetCurrentTotal();
            _behaviorData.DeleteAll();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var model = _studentData.GetById(id);
            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        //[HttpGet]
        //public IActionResult EditGoal()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult EditGoal(int newGoal)
        //{
        //    _settingsData.AddGoal(newGoal);

        //    return RedirectToAction(nameof(Index));
        //}

        //if (ModelState.IsValid)
        //{
        //    var newStudent = new Student();
        //    newStudent.StudentName = model.StudentName;
        //    if (_studentData.GetAll().Count() > 0)
        //    {
        //        newStudent.Id = _studentData.GetAll().Max(m => m.Id) + 1;
        //    }
        //    else
        //    {
        //        newStudent.Id = 1;
        //    }

        //    newStudent = _studentData.Add(newStudent);

        //    return RedirectToAction(nameof(Index));
        //}
        //else
        //{
        //    return View();
        //}
        //}








        public IActionResult Attendance()
        {
            ViewData["Message"] = "Where you will keep attendance.";

            return View();
        }
        public IActionResult Planner()
        {
            ViewData["Message"] = "Where you will be able to plan schedules.";

            return View();
        }

        public IActionResult Badges()
        {
            ViewData["Message"] = "Where you will be able to assign kids badges.";

            return View();
        }

        public IActionResult Stats()
        {
            ViewData["Message"] = "Where you will view stats about your kiddos.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
