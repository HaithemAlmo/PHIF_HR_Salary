using Almotkaml.HR.Abstraction;
using Almotkaml.HR.Business.Extensions;
using Almotkaml.HR.Domain;
using Almotkaml.HR.Models;
using System;
using Almotkaml.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almotkaml.HR.Business.App_Business.MainSettings
{
    public class EmployeeTestBusiness : Business, IEmployeeTestBusiness
    {
        public EmployeeTestBusiness(HumanResource humanResource) : base(humanResource)
        {
        }

        private bool HavePermission(bool permission = true)
           => ApplicationUser.Permissions.EmployeeTest && permission;

        public EmployeeTestModel Prepare()
        {
            if (!HavePermission(ApplicationUser.Permissions.EmployeeTest_Create))
                return Null<EmployeeTestModel>(RequestState.NoPermission);

            return new EmployeeTestModel()
            {
                CanCreate = ApplicationUser.Permissions.EmployeeTest_Create,
                CanEdit = ApplicationUser.Permissions.EmployeeTest_Edit,
                CanDelete = ApplicationUser.Permissions.EmployeeTest_Delete,
                CanAccsept=ApplicationUser.Permissions.EmployeeTest_Accsept,
                CanRefinesh=ApplicationUser.Permissions.EmployeeTest_Refinesh,
                EmployeeTestGridRow = UnitOfWork.EmployeeTest
                    .GetAll()
                    .ToGrid()
                    
                    ,
            };
        }

        public bool Select(EmployeeTestModel model)
        { 
            if (!HavePermission(ApplicationUser.Permissions.EmployeeTest_Edit))
                return Fail(RequestState.NoPermission);
            if (model.EmployeeTestId  <= 0)
                return Fail(RequestState.BadRequest);
            // var departmentId = employeeTest.DepartmentId ?? 0;
            var employeeTest = UnitOfWork.EmployeeTest.Find(model.EmployeeTestId);

            if (employeeTest == null)
                return Fail(RequestState.NotFound);
            model.EmployeeTestName = employeeTest.GetFullName();
            model.FirstName = employeeTest.FirstName;
            model.FatherName = employeeTest.FatherName;
            model.Gender = employeeTest.Gender;
            model.NationalNumber = employeeTest.NationalNumber;
            model.Phone = employeeTest.Phone;
            model.Email = employeeTest.Email;
            model.DateEndJob = employeeTest.DateEndJob.FormatToString();
            model.DateStartJob = employeeTest.DateStartJob.FormatToString();
            model.GrandfatherName = employeeTest.GrandfatherName;
            model.Nationality = employeeTest.Nationality;
            model.StateEmployee = employeeTest.StateEmployee;
            model.Address = employeeTest.Address;
            model.BirthDate = employeeTest.BirthDate;
            model.Department = employeeTest.Department;
            model.SalaryTest = employeeTest.Totalsalary();
         //   model.DepartmentId = departmentId;


            return true;

        }

        public bool Create(EmployeeTestModel model)
        {
            if (!HavePermission(ApplicationUser.Permissions.EmployeeTest_Create))
                return Fail(RequestState.NoPermission);

            if (!ModelState.IsValid(model))
                return false;

            if (UnitOfWork.EmployeeTest.NameIsExisted(model.NationalNumber))
                return NameExisted();
            var employeeTest = EmployeeTest.New(model.FirstName, model.FatherName, model.GrandfatherName, model.LastName
                , DateTime.Parse(model.DateStartJob), DateTime.Parse(model.DateEndJob), model.NationalNumber, model.Nationality, model.Address, model.Phone, model.Email,model.Gender, model.BirthDate, model.Department, model.StateEmployee);
            UnitOfWork.EmployeeTest.Add(employeeTest);

            UnitOfWork.Complete(n => n.EmployeeTest_Create);

            return SuccessCreate();


        }

        public void Refresh(EmployeeTestModel model)
        {
        }

        public bool Edit(EmployeeTestModel model)

        {
            if (model.EmployeeTestId <= 0)
                return Fail(RequestState.BadRequest);

            if (!HavePermission(ApplicationUser.Permissions.EmployeeTest_Edit))
                return Fail(RequestState.NoPermission);

            if (!ModelState.IsValid(model))
                return false;

            var employeeTest = UnitOfWork.EmployeeTest.Find(model.EmployeeTestId);

            if (employeeTest == null)
                return Fail(RequestState.NotFound);

            if (UnitOfWork.EmployeeTest.NameIsExisted(model.NationalNumber,model.EmployeeTestId))
                return NameExisted();
            employeeTest.Modify(model.FirstName, model.FatherName, model.GrandfatherName, model.LastName
                ,DateTime.Parse( model.DateStartJob),DateTime.Parse( model.DateEndJob), model.NationalNumber, model.Nationality, model.Address, model.Phone, model.Email,model.Gender, model.BirthDate, model.Department, model.StateEmployee);
            UnitOfWork.Complete(n => n.EmployeeTest_Edit);

            return SuccessEdit();

        }

        public bool Delete(EmployeeTestModel model)
        {
            if (!HavePermission(ApplicationUser.Permissions.EmployeeTest_Delete))
                return Fail(RequestState.NoPermission);

            if (model.EmployeeTestId  <= 0)
                return Fail(RequestState.BadRequest);

            var employeeTest = UnitOfWork.EmployeeTest.Find(model.EmployeeTestId );

            if (employeeTest == null)
                return Fail(RequestState.NotFound);

            UnitOfWork.EmployeeTest.Remove(employeeTest);

            if (!UnitOfWork.TryComplete(n => n.EmployeeTest_Delete))
                return Fail(UnitOfWork.Message);

            return SuccessDelete();
        }

        public bool Refinesh(EmployeeTestModel model)
        {
            if (model.EmployeeTestId <= 0)
                return Fail(RequestState.BadRequest);

            if (!HavePermission(ApplicationUser.Permissions.EmployeeTest_Edit))
                return Fail(RequestState.NoPermission);

            if (!ModelState.IsValid(model))
                return false;

            var employeeTest = UnitOfWork.EmployeeTest.Find(model.EmployeeTestId);

            if (employeeTest == null)
                return Fail(RequestState.NotFound);

            //if (UnitOfWork.EmployeeTestSalary.NameIsExisted(model.NationalNumber, model.EmployeeTestId))
            //    return NameExisted();
         var emplo =   EmployeeTestSalary.New(employeeTest.FirstName, employeeTest.FatherName, employeeTest.GrandfatherName, employeeTest.LastName
                , employeeTest.DateStartJob, employeeTest.DateEndJob, employeeTest.NationalNumber, employeeTest.Nationality, employeeTest.Address, employeeTest.Phone, employeeTest.Email, employeeTest.Gender, employeeTest.Department, employeeTest.StateEmployee);

            UnitOfWork.EmployeeTestSalary.Add(emplo);
            UnitOfWork.Complete(n => n.EmployeeTest_Create);
            UnitOfWork.EmployeeTestSalary.Find(emplo.EmployeeTestSalaryID);
            return SuccessCreate();
        }

        public bool Accsept(EmployeeTestModel model)
        {
            if (!HavePermission(ApplicationUser.Permissions.EmployeeTest_Accsept))
                return Fail(RequestState.NoPermission);

            //if (!ModelState.IsValid(model))
            //    return false;
            //  var employeeAll = UnitOfWork.Employees.GetAll();
            var employeeTest = UnitOfWork.EmployeeTest.Find(model.EmployeeTestId);

            var employee = Employee.New()
               .WithFirstName(employeeTest.FirstName, null)
               .WithFatherName(employeeTest.FatherName, null)
               .WithGrandfatherName(employeeTest.GrandfatherName, null)
               .WithLastName(employeeTest.LastName, null)
               .WithMotherName(null)
               .WithGender(employeeTest.Gender)
               .WithBirthDate(employeeTest.BirthDate)
               .WithBirthPlace(null)
               .WithNationalNumber(employeeTest.NationalNumber)
               .WithReligion(Religion.Muslim)
               .WithNationalityId(0)
               .WithWifeNationalityId(0)/////////
               .WithPlaceId(0)
               .WithAddress(employeeTest.Address)
               .WithPhone(employeeTest.Phone)
               .WithEmail(employeeTest.Email)
               .WithSocialStatus(SocialStatus.Single)
               .WithChildernCount(0)
               .WithBloodType(BloodType.OPlus)
               .WithIsActive(true)
               .WithBooklet((new BookletModel()).ToDomain())
               .WithPassport((new PassportModel()).ToDomain())
               .WithIdentificationCard((new IdentificationCardModel()).ToDomain())
               .WithImage(null)
               .WithContactInfo((new ContactInfoModel()).ToDomain())
               .Biuld();

            UnitOfWork.Employees.Add(employee);
            UnitOfWork.Complete(n => n.Employee_Create);
            UnitOfWork.Employees.Find(employee.EmployeeId);


            var modifier = employee.JobInfo?.Modify()
               .DirectlyDate(DateTime.Parse(employeeTest.DateEndJob.FormatToString()))
               .JobNumberLIC(0)
               .SalaryTest(employeeTest.Totalsalary());
               
            UnitOfWork.Complete(n => n.Employee_Create);
            //   model.EmployeeId = employee.EmployeeId;
            return SuccessCreate();
        }



    }
}
