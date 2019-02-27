using SeniorProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProject.Services
{
    public interface IStudentData
    {
        //IEnumerable<Student> GetAll();
        IEnumerable<Student> GetAllFromUser(string name);
        //Student GetByName(string name);
        Student GetStudentFromUser(string name, string student);
        Student GetById(int id);
        int GetStudentId(string user, string student);
        Student Add(Student student);
        void Delete(string user, string student);
        //bool Contains(string student);
        bool UserContains(string name, string student);
        void EditAddLifetimeTotal(string user, string student, int val);
        void EditAddCurrentTotal(string user, string student, int val);
        void EditDeleteLifetimeTotal(string user, string student, int val);
        void EditDeleteCurrentTotal(string user, string student, int val);
        void ResetCurrentTotal();
    }
}
