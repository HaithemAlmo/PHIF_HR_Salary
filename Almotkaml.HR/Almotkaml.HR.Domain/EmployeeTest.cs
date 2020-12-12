
using System;
using System.Collections.Generic;
using System.Linq;

namespace Almotkaml.HR.Domain
{
    public class EmployeeTest
    {
        private EmployeeTest()
        {

        }
        public int DateCount() => DateEndJob.Day - DateStartJob.Day;
        public string GetFullName() => FirstName + " " + FatherName + " " + GrandfatherName + " " + LastName;
        //public EmployeeTest(Employee employee)
        //{
        //    Employee = employee;
        //}
        public EmployeeTest(string firstName, string fatherName,string grandfatherName, string lastName
            , DateTime dateStartJob, DateTime dateEndJob, string nationalNumber, string nationality
            , string address, string phone,string email, Gender gender, DateTime birthDate,string department , bool stateEmployee)
        {
            FirstName = FirstName;
            FatherName = fatherName;
            GrandfatherName = grandfatherName;
            LastName = lastName;
            DateStartJob = dateStartJob;
            DateEndJob = dateEndJob;
            NationalNumber = nationalNumber;
            Nationality = nationality;
            Address = address;
            Phone = phone;
            Email = email;
            Gender = gender;
            BirthDate = birthDate;
            Department = department;
            StateEmployee = stateEmployee;


        }
        public static EmployeeTest New(string firstName, string fatherName, string grandfatherName, string lastName
           , DateTime dateStartJob, DateTime dateEndJob, string nationalNumber, string nationality
           , string address, string phone, string email, Gender gender, DateTime birthDate,string department , bool stateEmployee)
        {
            var employeeTest = new EmployeeTest();
            employeeTest. FirstName = firstName;
            employeeTest. FatherName = fatherName;
            employeeTest. GrandfatherName = grandfatherName;
            employeeTest.LastName = lastName;
            employeeTest.DateStartJob = dateStartJob;
            employeeTest.DateEndJob = dateEndJob;
            employeeTest.NationalNumber = nationalNumber;
            employeeTest.Nationality = nationality;
            employeeTest.Address = address;
            employeeTest.Phone = phone;
            employeeTest.Email = email;
            employeeTest.Gender = gender;
            employeeTest.BirthDate = birthDate;
            employeeTest.Department = department;
            employeeTest.StateEmployee = stateEmployee;
           // employeeTest.DepartmentId = departmentId;
            return employeeTest;

        }
        public void  Modify(string firstName, string fatherName, string grandfatherName, string lastName
      , DateTime dateStartJob, DateTime dateEndJob, string nationalNumber, string nationality
      , string address, string phone, string email, Gender gender, DateTime birthDate,string department , bool stateEmployee)
        {
            FirstName = FirstName;
            FatherName = fatherName;
            GrandfatherName = grandfatherName;
            LastName = lastName;
            DateStartJob = dateStartJob;
            DateEndJob = dateEndJob;
            NationalNumber = nationalNumber;
            Nationality = nationality;
            Address = address;
            Phone = phone;
            Email = email;
            Gender = gender;
            BirthDate = birthDate;
            Department = department;
            StateEmployee = stateEmployee;
        //    DepartmentId = departmentId;
        }
        public int EmployeeTestId { get;internal set; }
        public string FirstName { get; internal set; }
        public string FatherName { get; internal set; }
        public string GrandfatherName { get; internal set; }
        public string LastName { get; internal set; }
        public DateTime DateStartJob { get; internal set; }
        public DateTime DateEndJob { get; internal set; }
        public string NationalNumber { get; internal set; }
        public string Nationality { get; internal set; }
        public string Address { get; internal set; }
        public string Phone { get; internal set; }
        public string Email { get; internal set; }
        public Gender Gender { get; internal set; }
        public DateTime BirthDate { get; internal set; }
      //  public int? DepartmentId { get; internal set; }
        public string Department { get; internal set; }
        public bool StateEmployee { get; internal set; }
       // public  JobInfo JobInfo { get;  set; }


        public decimal Totalsalary()
        {
            decimal salary = 0;
            int countday = DateEndJob.Day - DateStartJob.Day;
            salary = 1100 * countday / 30;
            return salary;
        }
    }

}
