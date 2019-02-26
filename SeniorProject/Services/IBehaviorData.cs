﻿using SeniorProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProject.Services
{
    public interface IBehaviorData
    {
        IEnumerable<Behavior> GetAll();
        Behavior Get(int id);
        Behavior GetByName(string behavior, string student);
        Behavior Add(Behavior behavior);
        void Delete(string behavior, string student);
        void DeleteAll();
    }
}
