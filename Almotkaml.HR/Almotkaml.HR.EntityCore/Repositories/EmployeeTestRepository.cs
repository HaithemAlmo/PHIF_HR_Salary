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
    public class EmployeeTestRepository : Repository<EmployeeTest>, IEmployeeTestRepository
    {
        private HrDbContext Context { get; }

        internal EmployeeTestRepository(HrDbContext context)
            : base(context)
        {
            Context = context;
        }
        public IEnumerable<EmployeeTest> GetGride()
        {
            return Context.EmployeeTest
                 .Include(e => e.Department);




        }
        public bool NameIsExisted(string nationalNumber) => Context.EmployeeTest
            .Any(e => e.NationalNumber == nationalNumber);

        public bool NameIsExisted(string nationalNumber, int id) => Context.EmployeeTest
            .Any(e => e.NationalNumber == nationalNumber && e.EmployeeTestId != id);
    }
}
