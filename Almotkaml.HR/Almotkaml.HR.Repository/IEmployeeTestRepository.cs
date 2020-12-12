﻿using Almotkaml.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Almotkaml.HR.Domain;

namespace Almotkaml.HR.Repository
{
    public interface IEmployeeTestRepository : IRepository<EmployeeTest> {

        IEnumerable<EmployeeTest> GetGride();
        bool NameIsExisted(string Name);
        bool NameIsExisted(string Name,int id);
    }
    
}
