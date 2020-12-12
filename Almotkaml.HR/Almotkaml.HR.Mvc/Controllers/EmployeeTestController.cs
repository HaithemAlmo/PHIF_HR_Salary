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

    public class EmployeeTestController : BaseController
    {
        // GET: CostCenter
        public ActionResult Index()
        {
            var model = HumanResource.EmployeeTest.Prepare();

            if (model == null)
                return HumanResourceState();

            SaveModel(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(EmployeeTestModel model, FormCollection form)
        {
            LoadModel(model, form["savedModel"]);

            HumanResource.EmployeeTest.Refresh(model);

            if (!Request.IsAjaxRequest())
                return AjaxNotWorking();

            return AjaxIndex(model, form);
        }

        private PartialViewResult AjaxIndex(EmployeeTestModel model, FormCollection form)
        {
            var editEmployeeTestId = IntValue(form["editEmployeeTestId"]);
            var deleteEmployeeTestId = IntValue(form["deleteEmployeeTestId"]);
         var accseptEmployeeTestId = IntValue(form["accseptEmployeeTestId"]);
          var refineshEmployeeTestId = IntValue(form["refineshEmployeeTestId"]);
            // Select
            if (editEmployeeTestId > 0)
                return Select(model, editEmployeeTestId);

            // Delete
            if (deleteEmployeeTestId > 0)
                return Delete(model, deleteEmployeeTestId);
            if (accseptEmployeeTestId > 0)
                return Accsept(model, accseptEmployeeTestId);
            if (refineshEmployeeTestId > 0)
                return Refinesh(model, refineshEmployeeTestId);
            // Insert
            if (!ModelState.IsValid)
                return PartialView("_Form", model);

            if (model.EmployeeTestId == 0)
            {
                if (!HumanResource.EmployeeTest.Create(model))
                    return AjaxHumanResourceState("_Form", model);
            }

            if (model.EmployeeTestId > 0)
            {
                if (!HumanResource.EmployeeTest .Edit(model))
                    return AjaxHumanResourceState("_Form", model);
            }

            CallRedirect();
            return PartialView("_Form", model);
        }
        private PartialViewResult Select(EmployeeTestModel model, int editEmployeeTestId)
        {
           ModelState.Clear();
            model.EmployeeTestId = editEmployeeTestId;

            if (!HumanResource.EmployeeTest.Select(model))
                return AjaxHumanResourceState("_Form", model);

            return PartialView("_Form", model);
        }
        private PartialViewResult Accsept(EmployeeTestModel model, int accseptEmployeeTestId)
        {
            ModelState.Clear();
            model.EmployeeTestId = accseptEmployeeTestId;

            if (!HumanResource.EmployeeTest.Accsept(model))
                return AjaxHumanResourceState("_Form", model);

            return PartialView("_Form", model);
        }
        private PartialViewResult Refinesh(EmployeeTestModel model, int refineshEmployeeTestId)
        {
            ModelState.Clear();
            model.EmployeeTestId = refineshEmployeeTestId;

            if (!HumanResource.EmployeeTest.Refinesh(model))
                return AjaxHumanResourceState("_Form", model);

            return PartialView("_Form", model);
        }
        private PartialViewResult Delete(EmployeeTestModel model, int deleteEmployeeTestId)
        {
            ModelState.Clear();
            model.EmployeeTestId = deleteEmployeeTestId;

            if (!HumanResource.EmployeeTest.Delete(model))
                return AjaxHumanResourceState("_Form", model);
            CallRedirect();

            return PartialView("_Form", model);
        }

        private void LoadModel(EmployeeTestModel model, string savedModel)
        {
            var loadedModel = LoadSavedModel<EmployeeTestModel>(savedModel);

            if (loadedModel == null)
                return;

            model.CanCreate = loadedModel.CanCreate;
            model.CanEdit = loadedModel.CanEdit;
            model.CanDelete = loadedModel.CanDelete;
            model.CanAccsept = loadedModel.CanAccsept;
            model.CanRefinesh = loadedModel.CanRefinesh;
            model.EmployeeTestGridRow = loadedModel.EmployeeTestGridRow;
        }
    }
}