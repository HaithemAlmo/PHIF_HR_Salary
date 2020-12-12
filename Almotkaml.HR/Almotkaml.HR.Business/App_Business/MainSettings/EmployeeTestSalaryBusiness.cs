using Almotkaml.HR.Abstraction;
using Almotkaml.HR.Business.Extensions;
using Almotkaml.HR.Domain;
using Almotkaml.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almotkaml.HR.Business.App_Business.MainSettings
{
    public class EmployeeTestSalaryBusiness : Business, IEmployeeTestSalaryBusiness
    {
        public EmployeeTestSalaryBusiness(HumanResource humanResource) : base(humanResource)
        {
        }

        private bool HavePermission(bool permission = true)
           => ApplicationUser.Permissions.EmployeeTestSalary && permission;

        public EmployeeTestSalaryModel Prepare()
        {
            if (!HavePermission(ApplicationUser.Permissions.EmployeeTest_Create))
                return Null<EmployeeTestSalaryModel>(RequestState.NoPermission);

            return new EmployeeTestSalaryModel()
            {
                CanCreate = ApplicationUser.Permissions.EmployeeTest_Create,
                CanEdit = ApplicationUser.Permissions.EmployeeTest_Edit,
                CanDelete = ApplicationUser.Permissions.EmployeeTest_Delete,
                CanAccsept=ApplicationUser.Permissions.EmployeeTest_Accsept,
                CanRefinesh=ApplicationUser.Permissions.EmployeeTest_Refinesh,
                EmployeeTestSalayGridRow = UnitOfWork.EmployeeTestSalary
                    .GetAll()
                    .ToGrid()
                    
                    ,
            };
        }

        public bool Select(EmployeeTestSalaryModel model)
        { 
            if (!HavePermission(ApplicationUser.Permissions.EmployeeTest_Edit))
                return Fail(RequestState.NoPermission);
            if (model.EmployeeTestId  <= 0)
                return Fail(RequestState.BadRequest);
            // var departmentId = employeeTest.DepartmentId ?? 0;
            var employeeTest = UnitOfWork.EmployeeTestSalary.Find(model.EmployeeTestId);

            if (employeeTest == null)
                return Fail(RequestState.NotFound);
            model.EmployeeTestName = employeeTest.GetFullName();
            model.FirstName = employeeTest.FirstName;
            model.FatherName = employeeTest.FatherName;
            model.Gender = employeeTest.Gender;
            model.NationalNumber = employeeTest.NationalNumber;
            model.Phone = employeeTest.Phone;
            model.Email = employeeTest.Email;
            model.DateEndJob = employeeTest.DateEndJob.ToString();
            model.DateStartJob = employeeTest.DateStartJob.ToString();
            model.GrandfatherName = employeeTest.GrandfatherName;
            model.Nationality = employeeTest.Nationality;
            model.StateEmployee = employeeTest.StateEmployee;
            model.Address = employeeTest.Address;
            model.MonthDate = employeeTest.MonthDate;
            model.Department = employeeTest.Department;
            model.TotalSalary = employeeTest.Totalsalary();
         //   model.DepartmentId = departmentId;


            return true;

        }

        public bool Create(EmployeeTestSalaryModel model)
        {
            if (!HavePermission(ApplicationUser.Permissions.EmployeeTest_Create))
                return Fail(RequestState.NoPermission);

            if (!ModelState.IsValid(model))
                return false;

            if (UnitOfWork.EmployeeTest.NameIsExisted(model.NationalNumber))
                return NameExisted();
            //var employeeTest = EmployeeTest.New(model.FirstName, model.FatherName, model.GrandfatherName, model.LastName
            //    , model.DateStartJob, model.DateEndJob, model.NationalNumber, model.Nationality, model.Address, model.Phone, model.Email, model.Gender, model.MonthDate, model.Department, model.StateEmployee);
            //UnitOfWork.EmployeeTest.Add(employeeTest);

            UnitOfWork.Complete(n => n.EmployeeTest_Create);

            return SuccessCreate();


        }

        public void Refresh(EmployeeTestSalaryModel model)
        {
        }

        public bool Edit(EmployeeTestSalaryModel model)

        {
            if (model.EmployeeTestId <= 0)
                return Fail(RequestState.BadRequest);

            if (!HavePermission(ApplicationUser.Permissions.EmployeeTest_Edit))
                return Fail(RequestState.NoPermission);

            if (!ModelState.IsValid(model))
                return false;

            var employeeTest = UnitOfWork.EmployeeTestSalary.Find(model.EmployeeTestId);

            if (employeeTest == null)
                return Fail(RequestState.NotFound);

            if (UnitOfWork.EmployeeTest.NameIsExisted(model.NationalNumber, model.EmployeeTestId))
                return NameExisted();
            //employeeTest.Modify(model.FirstName, model.FatherName, model.GrandfatherName, model.LastName
            //    , model.DateStartJob, model.DateEndJob, model.NationalNumber, model.Nationality, model.Address, model.Phone, model.Email, model.Gender, model.MonthDate, model.Department, model.StateEmployee);
            //UnitOfWork.Complete(n => n.EmployeeTest_Edit);

            return SuccessEdit();

        }

        public bool Delete(EmployeeTestSalaryModel model)
        {
            if (!HavePermission(ApplicationUser.Permissions.EmployeeTest_Delete))
                return Fail(RequestState.NoPermission);

            if (model.EmployeeTestId <= 0)
                return Fail(RequestState.BadRequest);

            var employeeTest = UnitOfWork.EmployeeTestSalary.Find(model.EmployeeTestId);

            if (employeeTest == null)
                return Fail(RequestState.NotFound);

            UnitOfWork.EmployeeTestSalary.Remove(employeeTest);

            if (!UnitOfWork.TryComplete(n => n.EmployeeTest_Delete))
                return Fail(UnitOfWork.Message);

            return SuccessDelete();
        }

        //public bool Refinesh(EmployeeTestModel model)
        //{




        //    return SuccessCreate();
        //}

        //public bool Accsept(EmployeeTestModel model)
        //{
        //    if (!HavePermission(ApplicationUser.Permissions.EmployeeTest_Accsept))
        //        return Fail(RequestState.NoPermission);

        //    //if (!ModelState.IsValid(model))
        //    //    return false;
        //    //  var employeeAll = UnitOfWork.Employees.GetAll();
        //    var employeeTest = UnitOfWork.EmployeeTestSalary.Find(model.EmployeeTestId);

           

   
        //    return SuccessCreate();
        //}

      
    }
}
