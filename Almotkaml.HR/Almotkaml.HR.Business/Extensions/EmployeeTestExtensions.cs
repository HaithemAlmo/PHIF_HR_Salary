using Almotkaml.HR.Domain;
using Almotkaml.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Almotkaml.Extensions;

namespace Almotkaml.HR.Business.Extensions
{

    public static class EmployeeTestSalaryExtensions
    {
        public static IEnumerable<EmployeeTestSalaryListItem> ToList(this IEnumerable<EmployeeTestSalary> EmployeeTest)
           => EmployeeTest.Select(d => new EmployeeTestSalaryListItem()
           {EmployeeTestSalaryId=d.EmployeeTestSalaryID,
           EmployeeTestName=d.GetFullName(),
           DayCount=d.DateCount(),
           Department=d.Department,

           DateStartJob=d.DateStartJob.FormatToString(),
           DateEndJob=d.DateEndJob.FormatToString(),
           MonthDate=d.MonthDate,
           Address=d.Address,
           Email=d.Email,
           Phone=d.Phone,


           });
        public static IEnumerable<EmployeeTestSalaryGridRow> ToGrid(this IEnumerable<EmployeeTestSalary> EmployeeTestSalary)
           => EmployeeTestSalary.Select(d => new EmployeeTestSalaryGridRow()
           {
               EmployeeTestSalaryId = d.EmployeeTestSalaryID,
               EmployeeTestName = d.GetFullName(),
               DayCount = d.DateCount(),
               Department = d.Department,
               TotalSalary=d.Totalsalary(),
               DateStartJob = d.DateStartJob.FormatToString(),
               DateEndJob = d.DateEndJob.FormatToString(),
               MonthDate = d.MonthDate,
               Address = d.Address,
               Email = d.Email,
               Phone = d.Phone,
               
           });
    }
}
