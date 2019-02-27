using SeniorProject.Data;
using SeniorProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProject.Services
{
    public class SqlStudentData : IStudentData
    {
        private ApplicationDbContext _context;

        public SqlStudentData(ApplicationDbContext context)
        {
            _context = context;
        }

        public Student Add(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
            return student;
        }

        public void Delete(string user, string student)
        {
            Student removeStudent = GetStudentFromUser(user, student);
            if (removeStudent != null)
            {
                _context.Students.Remove(removeStudent);
                _context.SaveChanges();
            }
        }

        //public Student GetByName(string name)
        //{
        //    return _context.Students.FirstOrDefault(s => s.StudentName == name);
        //}

        public Student GetById(int id)
        {
            return _context.Students.FirstOrDefault(s => s.Id == id);
        }

        public int GetStudentId(string user, string student)
        {
            return GetStudentFromUser(user, student).Id;
        }

        //public IEnumerable<Student> GetAll()
        //{
        //    return _context.Students.OrderBy(s => s.StudentName);
        //}

        public IEnumerable<Student> GetAllFromUser(string name)
        {
            return _context.Students.Where(s => s.User == name);
        }

        //public bool Contains(string student)
        //{
        //    return _context.Students.ToList().Contains(GetByName(student));
        //}

        public Student GetStudentFromUser(string name, string student)
        {
            return _context.Students.Where(s => s.User == name).FirstOrDefault(s => s.StudentName == student);
        }

        public bool UserContains(string name, string student)
        {
            return _context.Students.Where(s => s.User == name).ToList().Contains(GetStudentFromUser(name, student));
        }

        public void EditAddLifetimeTotal(string user, string student, int val)
        {
            if (UserContains(user,student))
            {
                GetStudentFromUser(user, student).LifetimeTotal += val;
                _context.SaveChanges();
            }
        }

        public void EditAddCurrentTotal(string user, string student, int val)
        {
            if (UserContains(user, student))
            {
                GetStudentFromUser(user, student).CurrentTotal += val;
                GetStudentFromUser(user, student).GraphValue = Convert.ToInt32((GetStudentFromUser(user, student).CurrentTotal / 100.0) * 725.0) + 200;
                _context.SaveChanges();
            }
        }

        public void EditDeleteLifetimeTotal(string user, string student, int val)
        {
            if (UserContains(user, student))
            {
                GetStudentFromUser(user, student).LifetimeTotal -= val;
                _context.SaveChanges();
            }
        }

        public void EditDeleteCurrentTotal(string user, string student, int val)
        {
            if (UserContains(user, student))
            {
                GetStudentFromUser(user, student).CurrentTotal -= val;
                GetStudentFromUser(user, student).GraphValue = Convert.ToInt32((GetStudentFromUser(user, student).CurrentTotal / 100.0) * 725.0) + 200;
                _context.SaveChanges();
            }
        }

        public void ResetCurrentTotal()
        {
            foreach (var student in _context.Students)
            {
                student.CurrentTotal = 0;
                student.GraphValue = 200;
            }

            _context.SaveChanges();
        }
    }
}
