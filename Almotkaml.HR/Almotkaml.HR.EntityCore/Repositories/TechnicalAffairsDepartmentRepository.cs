﻿using Almotkaml.HR.Domain;
using Almotkaml.HR.EntityCore.Repositories;
using Almotkaml.HR.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almotkaml.HR.EntityCore.Repositories
{
    //class TechnicalAffairsDepartmentRepository
    //{
    //}

    public class TechnicalAffairsDepartmentRepository : Repository<TechnicalAffairsDepartment>, ITechnicalAffairsDepartmentRepository
    {
        private HrDbContext Context { get; }

        internal TechnicalAffairsDepartmentRepository(HrDbContext context)
            : base(context)
        {
            Context = context;
        }
        public IEnumerable<TechnicalAffairsDepartment> GetTechnicalAffairsDepartmentByEmployeeId(int employeeid)
        {
            return Context.TechnicalAffairsDepartments
                .Include(e => e.EntrantsAndReviewers)
                .Where(e => e.EntrantsAndReviewersId
                == employeeid);
        }

        public IEnumerable<TechnicalAffairsDepartment> GetEntrantsAndReviewersBy(int year, int month, bool b)
        {
            return Context.TechnicalAffairsDepartment.Include(t => t.EntrantsAndReviewers)

                .Where(e => e.YearWork == year && e.MonthWork == month && e.IsPaid == b);
        }


        public bool NameIsExisted(int entrantsAndReviewersId) => Context.TechnicalAffairsDepartments
            .Any(e => e.EntrantsAndReviewersId == entrantsAndReviewersId);


        public override IEnumerable<TechnicalAffairsDepartment> GetAll()
        {
            return Context.TechnicalAffairsDepartment
                 .Include(e => e.EntrantsAndReviewers);


        }

        public override TechnicalAffairsDepartment Find(object id)
        {
            return Context.TechnicalAffairsDepartment
                .Include(e => e.EntrantsAndReviewers)
                .FirstOrDefault(t => t.TechnicalAffairsDepartmentId == (long)id);
        }

        public TechnicalAffairsDepartment Find1(object id, object month, object year)
        {
            return Context.TechnicalAffairsDepartment
            .FirstOrDefault(e => e.EntrantsAndReviewersId == (long)id && e.MonthWork == (int)month && e.YearWork == (int)year);
        }



        public IEnumerable<TechnicalAffairsDepartment> Findispaid(object year, object month)
        {
            // return

            var technical = Context.TechnicalAffairsDepartment
            .Where(e => e.YearWork == (int)year && e.MonthWork == (int)month).ToList();

            return technical;
        }

        public bool CheckEntryByMonth(int employeeId, int month, int year)
        {
            return Context.TechnicalAffairsDepartments
                .Include(e => e.EntrantsAndReviewers)
                .Any(e => e.EntrantsAndReviewersId == employeeId && e.MonthWork == month && e.YearWork == year);
        }

        //return Context.TechnicalAffairsDepartment
        //    .Include(e => e.EntrantsAndReviewers).FirstOrDefault(t => t.TechnicalAffairsDepartmentId == (long)id and t=>t.te);

        //public IEnumerable<TechnicalAffairsDepartment> GetAbsentEmployeesBy(DateTime dateFrom, DateTime dateTo, AbsenceType absenceType)
        //{
        //    return Context.Absences
        //        .Include(e => e.Employee)
        //        .ThenInclude(s => s.SalaryInfo)
        //        .Include(e => e.Employee)
        //        .ThenInclude(e => e.JobInfo)
        //        .ThenInclude(j => j.Unit)
        //        .ThenInclude(u => u.Division)
        //        .ThenInclude(d => d.Department)
        //        .ThenInclude(d => d.Center)
        //        .Where(e => e.AbsenceType == absenceType
        //                && e.Date.Date >= dateFrom.Date
        //                && e.Date.Date <= dateTo.Date);
        //}

        //public bool NameIsExisted(string employeeName, int id) => Context.TechnicalAffairsDepartments
        //    .Any(e => e.EmployeeName == employeeName && e.EntrantsAndReviewersId != id);
    }
}
