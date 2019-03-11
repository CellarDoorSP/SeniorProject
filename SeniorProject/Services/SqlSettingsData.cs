using SeniorProject.Data;
using SeniorProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProject.Services
{
    public class SqlSettingsData : ISettingsData
    {
        private ApplicationDbContext _context;

        public SqlSettingsData(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddUser(string user)
        {
            Settings newSettings = new Settings();
            newSettings.User = user;
            _context.Add(newSettings);
            _context.SaveChanges();
        }

        public void EditExtraGoal(string user, int newExtraGoal)
        {
            var userSettings = _context.Settings.FirstOrDefault(u => u.User == user);
            userSettings.ExtraGoal = newExtraGoal;
            _context.SaveChanges();
        }

        public void EditMainGoal(string user, int newMainGoal)
        {
            var userSettings = _context.Settings.FirstOrDefault(u => u.User == user);
            userSettings.ExtraGoal = newMainGoal;
            _context.SaveChanges();
        }

        public int GetExtraGoal(string user)
        {
            return _context.Settings.FirstOrDefault(u => u.User == user).ExtraGoal;
        }

        public int GetMainGoal(string user)
        {
            return _context.Settings.FirstOrDefault(u => u.User == user).MainGoal;
        }
    }
}
