using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProject.Services
{
    public interface ISettingsData
    {
        void AddUser(string user);
        void EditMainGoal(string user, int newMainGoal);
        void EditExtraGoal(string user, int newExtraGoal);
        int GetMainGoal(string user);
        int GetExtraGoal(string user);
    }
}
