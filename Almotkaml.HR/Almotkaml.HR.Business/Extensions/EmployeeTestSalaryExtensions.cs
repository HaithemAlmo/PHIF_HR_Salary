using Almotkaml.HR.Domain;
using Almotkaml.Extensions;
using Almotkaml.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Almotkaml.HR.Business.Extensions
{

    public static class EmployeeTestExtensions
    {
        public static IEnumerable<EmployeeTestListItem> ToList(this IEnumerable<EmployeeTest> EmployeeTest)
           => EmployeeTest.Select(d => new EmployeeTestListItem()
           {EmployeeTestId=d.EmployeeTestId,
           EmployeeTestName=d.GetFullName(),
           DayCount=d.DateCount(),
           Department=d.Department,

           DateStartJob=d.DateStartJob.FormatToString(),
           DateEndJob=d.DateEndJob.FormatToString(),
           BirthDate=d.BirthDate,
           Address=d.Address,
           Email=d.Email,
           Phone=d.Phone,


           });
        public static IEnumerable<EmployeeTestGridRow> ToGrid(this IEnumerable<EmployeeTest> EmployeeTest)
           => EmployeeTest.Select(d => new EmployeeTestGridRow()
           {
               EmployeeTestId = d.EmployeeTestId,
               EmployeeTestName = d.GetFullName(),
               DayCount = d.DateCount(),
               Department = d.Department,

               DateStartJob = d.DateStartJob.FormatToString(),
               DateEndJob = d.DateEndJob.FormatToString(),
               BirthDate = d.BirthDate,
               Address = d.Address,
               Email = d.Email,
               Phone = d.Phone,
           });
    }
}
