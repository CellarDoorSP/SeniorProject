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

        public void Delete(string student)
        {
            Student removeStudent = GetByName(student);
            if (removeStudent != null)
            {
                _context.Students.Remove(removeStudent);
                _context.SaveChanges();
            }
        }

        public Student GetByName(string name)
        {
            return _context.Students.FirstOrDefault(s => s.StudentName == name);
        }

        public Student GetById(int id)
        {
            return _context.Students.FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<Student> GetAll()
        {
            return _context.Students.OrderBy(s => s.StudentName);
        }

        public bool Contains(string student)
        {
            return _context.Students.ToList().Contains(GetByName(student));
        }

        public void EditAddLifetimeTotal(string student, int val)
        {
            if (Contains(student))
            {
                GetByName(student).LifetimeTotal += val;
                _context.SaveChanges();
            }
        }

        public void EditAddCurrentTotal(string student, int val)
        {
            if (Contains(student))
            {
                GetByName(student).CurrentTotal += val;
                GetByName(student).GraphValue = Convert.ToInt32((GetByName(student).CurrentTotal / 100.0) * 725.0) + 200;
                _context.SaveChanges();
            }
        }

        public void EditDeleteLifetimeTotal(string student, int val)
        {
            if (Contains(student))
            {
                GetByName(student).LifetimeTotal -= val;
                _context.SaveChanges();
            }
        }

        public void EditDeleteCurrentTotal(string student, int val)
        {
            if (Contains(student))
            {
                GetByName(student).CurrentTotal -= val;
                GetByName(student).GraphValue = Convert.ToInt32((GetByName(student).CurrentTotal / 100.0) * 725.0) + 200;
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
