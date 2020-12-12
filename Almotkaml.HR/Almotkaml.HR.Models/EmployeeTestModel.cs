using Almotkaml.Attributes;
using Almotkaml.HR.Domain;
using Almotkaml.HR.Models.Resources;
using Almotkaml.HR.Resources;
using Almotkaml.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almotkaml.HR.Models
{
    public class EmployeeTestModel
    {
    
    //public class EntrantsAndReviewersIndexModel
    //{
        public IEnumerable<EmployeeTestGridRow> EmployeeTestGridRow { get; set; } = new HashSet<EmployeeTestGridRow>();
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanAccsept { get; set; }
        public bool CanRefinesh { get; set; }
        
        [Required(ErrorMessageResourceType = typeof(SharedMessages), ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(Title), Name = nameof(Title.EmployeeNumber))]
        public int EmployeeNumber { get; set; }
        public int EmployeeTestId { get; set; }

      //  [Required(ErrorMessageResourceType = typeof(SharedMessages), ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(Title), Name = nameof(Title.FirstName))]
        public string FirstName { get;  set; }


       // [Required(ErrorMessageResourceType = typeof(SharedMessages), ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(Title), Name = nameof(Title.FatherName))]
        public string FatherName { get; set; }




        //public IEnumerable<DepartmentListItem> DepartmentList { get; set; } = new HashSet<DepartmentListItem>();


        //[Display(ResourceType = typeof(Title),
        // Name = nameof(Title.Department))]
        //public int DepartmentId { get; set; }
        [MinLength(12, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.NationalNumber))]
        [Display(ResourceType = typeof(Title), Name = nameof(Title.NationalNumber))]
        public string NationalNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedMessages), ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(Title), Name = nameof(Title.Gender))]
        public Gender Gender { get; set; }

        [Display(ResourceType = typeof(Title), Name = nameof(Title.Phone))]
        public string Phone { get; set; }


        [Display(ResourceType = typeof(Title), Name = nameof(Title.Email))]
        public string Email { get; set; }

   
        [Display(ResourceType = typeof(Title), Name = nameof(Title.DateStartJob))]
        public string DateStartJob { get; set; }

      
        [Display(ResourceType = typeof(Title), Name = nameof(Title.DateEndJob))]
        public string DateEndJob { get; set; }


        [Display(ResourceType = typeof(Title), Name = nameof(Title.Note))]
        public string Note { get; set; }


       // [Required(ErrorMessageResourceType = typeof(SharedMessages), ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(Title), Name = nameof(Title.GrandfatherName))]
        public string GrandfatherName { get;  set; }
    //    [Required(ErrorMessageResourceType = typeof(SharedMessages), ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(Title), Name = nameof(Title.LastName))]
        public string LastName { get; set; }
       // [Required(ErrorMessageResourceType = typeof(SharedMessages), ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(Title), Name = nameof(Title.Nationality))]
        public string Nationality { get;  set; }
      //  [Required(ErrorMessageResourceType = typeof(SharedMessages), ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(Title), Name = nameof(Title.Address))]
        public string Address { get; set; }
       // [Required(ErrorMessageResourceType = typeof(SharedMessages), ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(Title), Name = nameof(Title.BirthDate))]
        public DateTime BirthDate { get;  set; }
      //  [Required(ErrorMessageResourceType = typeof(SharedMessages), ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(Title), Name = nameof(Title.Department))]

       public string Department { get; set; }
     //   [Required(ErrorMessageResourceType = typeof(SharedMessages), ErrorMessageResourceName = nameof(SharedMessages.IsRequired))]
        [Display(ResourceType = typeof(Title), Name = nameof(Title.StateEmployee))]
        public bool StateEmployee { get; set; }
        public string EmployeeTestName { get; set; }

        public int DayCount { get;  set; }

        public decimal SalaryTest { get; set; }
    }






    public class EmployeeTestGridRow
    {
        public int EmployeeTestId { get;  set; }
        public string EmployeeTestName { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get;  set; }
        public string GrandfatherName { get; set; }
        public string LastName { get;  set; }
        public string DateStartJob { get;  set; }
        public string DateEndJob { get;  set; }
        public string NationalNumber { get;  set; }
        public Nationality Nationality { get;  set; }
        public string Address { get;  set; }
        public string Phone { get;  set; }
        public string Email { get;  set; }
        public Gender Gender { get;  set; }
        public DateTime BirthDate { get;  set; }
      //  public int?  DepartmentID { get; set; }
        public string Department { get;  set; }
        public bool StateEmployee { get;  set; }
         public int DayCount { get; set; }
    }

    public class EmployeeTestListItem
    {
        public string EmployeeTestName { get; set; }
        public int EmployeeTestId { get;  set; }
        public string FirstName { get;  set; }
        public string FatherName { get;  set; }
        public string GrandfatherName { get;  set; }
        public string LastName { get;  set; }
        public string DateStartJob { get;  set; }
        public string DateEndJob { get;  set; }
        public string NationalNumber { get;  set; }
        public string Nationality { get;  set; }
        public string Address { get;  set; }
        public string Phone { get;  set; }
        public string Email { get;  set; }
        public Gender Gender { get;  set; }
        public DateTime? BirthDate { get;  set; }
       // public int? DepartmentID { get; set; }
        public string Department { get;  set; }
        public bool StateEmployee { get;  set; }
        public int DayCount { get; set; }
    }
    





}
