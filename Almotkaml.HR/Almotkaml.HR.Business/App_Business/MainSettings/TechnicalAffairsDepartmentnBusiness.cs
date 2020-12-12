using Almotkaml.Extensions;
using Almotkaml.HR.Abstraction;
using Almotkaml.HR.Business.Extensions;
using Almotkaml.HR.Domain;
using Almotkaml.HR.Models;
using Almotkaml.HR.Repository;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Almotkaml.HR.Business.App_Business.MainSettings
{

    public class TechnicalAffairsDepartmentnBusiness : Business, ITechnicalAffairsDepartmentnBusiness
    {
        public TechnicalAffairsDepartmentnBusiness(HumanResource humanResource)
            : base(humanResource)
        {
        }

        private bool HavePermission(bool permission = true)
            => ApplicationUser.Permissions.TechnicalAffairsDepartment && permission;


        public TechnicalAffairsDepartmentModel Prepare()
        {
            if (!HavePermission(ApplicationUser.Permissions.TechnicalAffairsDepartment_Create))
                return Null<TechnicalAffairsDepartmentModel>(RequestState.NoPermission);

            return new TechnicalAffairsDepartmentModel()
            {
                CanCreate = ApplicationUser.Permissions.TechnicalAffairsDepartment_Create,
                CanEdit = ApplicationUser.Permissions.TechnicalAffairsDepartment_Edit,
                EntrantsAndReviewersGrid = UnitOfWork.EntrantsAndReviewerss.GetAll().ToGrid(),
                TechnicalAffairsDepartmentGrid = UnitOfWork.TechnicalAffairsDepartments.GetTechnicalAffairsDepartmentByEmployeeId(0).ToGrid(),

            };
        }

        public bool Select(TechnicalAffairsDepartmentModel model)
        {
            if (!HavePermission(ApplicationUser.Permissions.TechnicalAffairsDepartment_Edit))
                return Fail(RequestState.NoPermission);
            if (model.EntrantsAndReviewersId <= 0)
                return Fail(RequestState.BadRequest);

            var technicalAffairsDepartment = UnitOfWork.TechnicalAffairsDepartments.Find((long)model.TechnicalAffairsDepartmentId);

            if (technicalAffairsDepartment == null)
                return Fail(RequestState.NotFound);

            model.EntrantsAndReviewersId = technicalAffairsDepartment.EntrantsAndReviewersId;
            model.EmployeeName = technicalAffairsDepartment.EntrantsAndReviewers?.EmployeeName;
            model.MonthWork = technicalAffairsDepartment.MonthWork;
            model.YearWork = technicalAffairsDepartment.YearWork;
            model.DataEntry = technicalAffairsDepartment.DataEntryCount;
            model.FirstReview = technicalAffairsDepartment.FirstReviewCount;
            model.AccommodationReview = technicalAffairsDepartment.AccommodationReviewCount;
            model.ClincReview = technicalAffairsDepartment.ClincReviewCount;

            return true;

        }

        public void Select0(TechnicalAffairsDepartmentModel model)
        {

            var grid = UnitOfWork.TechnicalAffairsDepartments.Findispaid(model.YearWork, model.MonthWork).ToList();

            model.TechnicalAffairsDepartmentGrid = grid.ToGrid();

        }

        public bool SelectEntries(TechnicalAffairsDepartmentModel model, int id)
        {
            if (!HavePermission(ApplicationUser.Permissions.EntrantsAndReviewers_Edit))
                return Fail(RequestState.NoPermission);

            var technicalAffairsDepartment = UnitOfWork.TechnicalAffairsDepartments.Find1(id, model.MonthWork, model.YearWork);

            if (technicalAffairsDepartment == null)
                return Fail(RequestState.NotFound);

            technicalAffairsDepartment.Paid(true);

            UnitOfWork.Complete(n => n.TechnicalAffairsDepartment_Edit);

            return SuccessEdit();

        }

        //استجلاب سعر الورقة لادخال البيانات
        public decimal DataEntryCollect(ISettings settings)
        {
            decimal dataEntryCollect = 0;
            dataEntryCollect = settings.DataEntryPrice;
            var value2 = string.Format("{0:0.000}", /*Math.Truncate(*/dataEntryCollect * 1000/*)*/ / 1000);

            return decimal.Parse(value2);
        }
        //استجلاب سعر الملف للمراجعة الاولية 
        public decimal firstReviewCollect(ISettings settings)
        {
            decimal FirstReview = 0;
            FirstReview = settings.FirstReviewPrice;
            var value2 = string.Format("{0:0.000}", /*Math.Truncate(*/FirstReview * 1000/*)*/ / 1000);

            return decimal.Parse(value2);
        }

        //استجلاب سعر الملف للمراجعةالايواء 
        public decimal AccommodationReviewCollect(ISettings settings)
        {
            decimal AccommodationReview = 0;
            AccommodationReview = settings.AccommodationReviewPrice;
            var value2 = string.Format("{0:0.000}", /*Math.Truncate(*/AccommodationReview * 1000/*)*/ / 1000);

            return decimal.Parse(value2);
        }
        //استجلاب سعر الملف للعيادات  
        public decimal ClincReviewCollect(ISettings settings)
        {
            decimal ClincReview = 0;
            ClincReview = settings.ClincReviewPrice;
            var value2 = string.Format("{0:0.000}", /*Math.Truncate(*/ClincReview * 1000/*)*/ / 1000);

            return decimal.Parse(value2);
        }

        public bool Create(TechnicalAffairsDepartmentModel model)
        {
            if (!HavePermission(ApplicationUser.Permissions.TechnicalAffairsDepartment_Create))
                return Fail(RequestState.NoPermission);

            if (!ModelState.IsValid(model))
                return false;

            if (UnitOfWork.TechnicalAffairsDepartments.NameIsExisted(model.TechnicalAffairsDepartmentId))
                return NameExisted();

            if (UnitOfWork.TechnicalAffairsDepartments.CheckEntryByMonth(model.EntrantsAndReviewersId, model.MonthWork, model.YearWork))
                return false;

            var technicalAffairsDepartment = TechnicalAffairsDepartment.New()
                .WithEntrantsAndReviewersId(model.EntrantsAndReviewersId)
                .WithMonthWork(model.MonthWork)
                .WithYearWork(model.YearWork)
                .WithDataEntryCount(model.DataEntry)
                .WithDataEntryBalance(model.DataEntry * DataEntryCollect(Settings))
                .WithFirstReviewCount(model.FirstReview)
                .WithFirstReviewBalance(model.FirstReview * firstReviewCollect(Settings))
                .WithAccommodationReviewCount(model.AccommodationReview)
                .WithAccommodationReviewBalance(model.AccommodationReview * AccommodationReviewCollect(Settings))
                .WithClincReviewCount(model.ClincReview)
                .WithClincReviewBalance(model.ClincReview * ClincReviewCollect(Settings))
                .WithTotalBalance((model.DataEntry * DataEntryCollect(Settings)) + (model.FirstReview * firstReviewCollect(Settings)) + (model.AccommodationReview * AccommodationReviewCollect(Settings)) + (model.ClincReview * ClincReviewCollect(Settings)))
                .WithNote(model.Note)
                .WithIsPaid(false)
                .Biuld();

            UnitOfWork.TechnicalAffairsDepartments.Add(technicalAffairsDepartment);

            UnitOfWork.Complete(n => n.TechnicalAffairsDepartment_Create);
            model.TechnicalAffairsDepartmentGrid = UnitOfWork.TechnicalAffairsDepartments.GetTechnicalAffairsDepartmentByEmployeeId(model.EntrantsAndReviewersId).ToGrid();

            Clear(model);
            return SuccessCreate();

        }

        public bool Edit(TechnicalAffairsDepartmentModel model)
        {
            if (model.TechnicalAffairsDepartmentId <= 0)
                return Fail(RequestState.BadRequest);

            if (!HavePermission(ApplicationUser.Permissions.TechnicalAffairsDepartment_Edit))
                return Fail(RequestState.NoPermission);

            if (!ModelState.IsValid(model))
                return false;

            var technicalAffairsDepartment = UnitOfWork.TechnicalAffairsDepartments.Find(model.TechnicalAffairsDepartmentId);

            if (technicalAffairsDepartment == null)
                return Fail(RequestState.NotFound);

            if (UnitOfWork.TechnicalAffairsDepartments.NameIsExisted(model.TechnicalAffairsDepartmentId))
                return NameExisted();
            technicalAffairsDepartment.Modify()
           .EntrantsAndReviewersId(model.EntrantsAndReviewersId)
           .MonthWork(model.MonthWork)
           .YearWork(model.YearWork)
           .DataEntryCount(model.DataEntry)
           .DataEntryBalance(model.DataEntry * DataEntryCollect(Settings))
           .FirstReviewCount(model.FirstReview)
           .FirstReviewBalance(model.FirstReview * firstReviewCollect(Settings))
           .AccommodationReviewCount(model.AccommodationReview)
           .AccommodationReviewBalance(model.AccommodationReview * AccommodationReviewCollect(Settings))
           .ClincReviewCount(model.ClincReview)
           .ClincReviewBalance(model.ClincReview * ClincReviewCollect(Settings))
           .TotalBalance((model.DataEntry * DataEntryCollect(Settings)) + (model.FirstReview * firstReviewCollect(Settings)) + (model.AccommodationReview * AccommodationReviewCollect(Settings)) + (model.ClincReview * ClincReviewCollect(Settings)))
           .Note(model.Note)
           .Confirm();

            UnitOfWork.Complete(n => n.TechnicalAffairsDepartment_Edit);
            model.TechnicalAffairsDepartmentGrid = UnitOfWork.TechnicalAffairsDepartments.GetTechnicalAffairsDepartmentByEmployeeId(model.EntrantsAndReviewersId).ToGrid();
            Clear(model);
            return SuccessEdit();

        }

        public void Refresh0(TechnicalAffairsDepartmentModel model)
        {

            var employee = UnitOfWork.TechnicalAffairsDepartments.GetAll().Where(e => e.YearWork == model.YearWork && e.MonthWork == model.MonthWork);

            if (employee == null)
                return;

            if (model.IsPaids == TechnicalAffairsDepartmentModel.IsPaidd.IsPaidtrue)
                model.TechnicalAffairsDepartmentGrid = UnitOfWork.TechnicalAffairsDepartments.GetEntrantsAndReviewersBy(model.YearWork, model.MonthWork, true).ToGrid();

            else model.TechnicalAffairsDepartmentGrid = UnitOfWork.TechnicalAffairsDepartments.GetEntrantsAndReviewersBy(model.YearWork, model.MonthWork, false).ToGrid();

        }

        public void Refresh(TechnicalAffairsDepartmentModel model)
        {

            var employee = UnitOfWork.EntrantsAndReviewerss.GetEntrantsAndReviewersByEmployeeId(model.EntrantsAndReviewersId);

            if (employee == null)
                return;

            model.EmployeeName = employee.EmployeeName;
            model.EntrantsAndReviewersType = employee.EntrantsAndReviewersType;
            model.TechnicalAffairsDepartmentGrid = UnitOfWork.TechnicalAffairsDepartments.GetTechnicalAffairsDepartmentByEmployeeId(model.EntrantsAndReviewersId).ToGrid();
        }

        public bool Delete(TechnicalAffairsDepartmentModel model)
        {
            throw new NotImplementedException();
        }

        private void Clear(TechnicalAffairsDepartmentModel model)
        {
            model.DataEntry = 0;
            model.FirstReview = 0;
            model.AccommodationReview = 0;
            model.ClincReview = 0;
            model.TotalBalance = 0;
            model.MonthWork = DateTime.Now.Month;
            model.YearWork = DateTime.Now.Year;

        }

    }
}