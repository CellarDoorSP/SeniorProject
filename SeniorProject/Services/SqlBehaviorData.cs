using SeniorProject.Data;
using SeniorProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProject.Services
{
    public class SqlBehaviorData : IBehaviorData
    {
        private ApplicationDbContext _context;

        public SqlBehaviorData(ApplicationDbContext context)
        {
            _context = context;
        }

        public Behavior Add(Behavior behavior)
        {
            _context.Behavior.Add(behavior);
            _context.SaveChanges();
            return behavior;
        }

        public void Delete(string behavior, string student)
        {
            Behavior removeBehavior = GetByName(behavior, student);
            if (removeBehavior != null)
            {
                _context.Behavior.Remove(removeBehavior);
                _context.SaveChanges();
            }
        }

        public Behavior Get(int id)
        {
            return _context.Behavior.FirstOrDefault(s => s.Id == id);
        }

        public Behavior GetByName(string behavior, string student)
        {
            IQueryable<Behavior> behaviorsMatched = _context.Behavior.Where(s => s.BehaviorName == behavior);
            return behaviorsMatched.FirstOrDefault(b => b.StudentName == student);
        }

        public IEnumerable<Behavior> GetAll()
        {
            return _context.Behavior;
        }

        public void DeleteAll()
        {
            var behaviors = _context.Behavior;
            foreach (var behavior in behaviors)
            {
                _context.Behavior.Remove(behavior);
            }
            _context.SaveChanges();
        }
    }
}
