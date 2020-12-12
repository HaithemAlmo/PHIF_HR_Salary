using Almotkaml.HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Almotkaml.HR.Mvc.Controllers
{
    //public class EntrantsAndReviewersController : Controller
    //{
    //    // GET: EntrantsAndReviewers
    //    public ActionResult Index()
    //    {
    //        return View();
    //    }
    //}

    public class EmployeeTestSalaryController : BaseController
    {
        // GET: CostCenter
        public ActionResult Index()
        {
            var model = HumanResource.EmployeeTestSalary.Prepare();

            if (model == null)
                return HumanResourceState();

            SaveModel(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(EmployeeTestSalaryModel model, FormCollection form)
        {
            LoadModel(model, form["savedModel"]);

            HumanResource.EmployeeTestSalary.Refresh(model);

            if (!Request.IsAjaxRequest())
                return AjaxNotWorking();

            return AjaxIndex(model, form);
        }

        private PartialViewResult AjaxIndex(EmployeeTestSalaryModel model, FormCollection form)
        {
            var editEmployeeTestSalaryId = IntValue(form["editEmployeeTestSalaryId"]);
            var deleteEmployeeTestId = IntValue(form["deleteEmployeeTestSalaryId"]);
         var accseptEmployeeTestId = IntValue(form["accseptEmployeeTestSalaryId"]);
          var refineshEmployeeTestId = IntValue(form["refineshEmployeeTestSalaryId"]);
            // Select
            if (editEmployeeTestSalaryId > 0)
                return Select(model, editEmployeeTestSalaryId);

            // Delete
            if (deleteEmployeeTestId > 0)
                return Delete(model, deleteEmployeeTestId);
            //if (accseptEmployeeTestId > 0)
            //    return Accsept(model, accseptEmployeeTestId);
            //if (refineshEmployeeTestId > 0)
            //    return Refinesh(model, refineshEmployeeTestId);
            // Insert
            if (!ModelState.IsValid)
                return PartialView("_Form", model);

            if (model.EmployeeTestId == 0)
            {
                if (!HumanResource.EmployeeTestSalary.Create(model))
                    return AjaxHumanResourceState("_Form", model);
            }

            if (model.EmployeeTestId > 0)
            {
                if (!HumanResource.EmployeeTestSalary .Edit(model))
                    return AjaxHumanResourceState("_Form", model);
            }

            CallRedirect();
            return PartialView("_Form", model);
        }
        private PartialViewResult Select(EmployeeTestSalaryModel model, int editEmployeeTestId)
        {
           ModelState.Clear();
            model.EmployeeTestId = editEmployeeTestId;

            if (!HumanResource.EmployeeTestSalary.Select(model))
                return AjaxHumanResourceState("_Form", model);

            return PartialView("_Form", model);
        }
        //private PartialViewResult Accsept(EmployeeTestModel model, int accseptEmployeeTestId)
        //{
        //    ModelState.Clear();
        //    model.EmployeeTestId = accseptEmployeeTestId;

        //    if (!HumanResource.EmployeeTest.Accsept(model))
        //        return AjaxHumanResourceState("_Form", model);

        //    return PartialView("_Form", model);
        //}
        //private PartialViewResult Refinesh(EmployeeTestSalaryModel model, int refineshEmployeeTestId)
        //{
        //    ModelState.Clear();
        //    model.EmployeeTestId = refineshEmployeeTestId;

        //    if (!HumanResource.EmployeeTestSalary.Refinesh(model))
        //        return AjaxHumanResourceState("_Form", model);

        //    return PartialView("_Form", model);
        //}
        private PartialViewResult Delete(EmployeeTestSalaryModel model, int deleteEmployeeTestId)
        {
            ModelState.Clear();
            model.EmployeeTestId = deleteEmployeeTestId;

            if (!HumanResource.EmployeeTestSalary.Delete(model))
                return AjaxHumanResourceState("_Form", model);
            CallRedirect();

            return PartialView("_Form", model);
        }

        private void LoadModel(EmployeeTestSalaryModel model, string savedModel)
        {
            var loadedModel = LoadSavedModel<EmployeeTestSalaryModel>(savedModel);

            if (loadedModel == null)
                return;

            model.CanCreate = loadedModel.CanCreate;
            model.CanEdit = loadedModel.CanEdit;
            model.CanDelete = loadedModel.CanDelete;
            model.CanAccsept = loadedModel.CanAccsept;
            model.CanRefinesh = loadedModel.CanRefinesh;
            model.EmployeeTestSalayGridRow = loadedModel.EmployeeTestSalayGridRow;
        }
    }
}