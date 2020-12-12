using Almotkaml.HR.Repository;
using Almotkaml.HR.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Almotkaml.HR.EntityCore.Repositories
{
    public class EmployeeTestSalaryRepository : Repository<EmployeeTestSalary>, IEmployeeTestSalaryRepository
    {
        private HrDbContext Context { get; }

        internal EmployeeTestSalaryRepository(HrDbContext context)
            : base(context)
        {
            Context = context;
        }
        public IEnumerable<EmployeeTestSalary> GetGride()
        {
            return Context.EmployeeTestSalary

                 .Include(e => e.Department);




        }
        public bool NameIsExisted(string nationalNumber) => Context.EmployeeTestSalary
            .Any(e => e.NationalNumber == nationalNumber);

        public bool NameIsExisted(string nationalNumber, int id) => Context.EmployeeTestSalary
            .Any(e => e.NationalNumber == nationalNumber && e.EmployeeTestId != id);
    }
}
