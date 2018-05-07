using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
using resourceEdge.webUi.Infrastructure.Core;
using resourceEdge.webUi.Infrastructure.Handlers;
using resourceEdge.webUi.Infrastructure.SystemManagers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace resourceEdge.webUi.Controllers
{
    [CustomAuthorizationFilter]
    public class AssetController : Controller
    {
        IAsset assetRepo;
        IAssetCategory assetCategoryRepo;
        IRequestAsset reqAssetRepo;
        AssetManager assetmanager;
        public AssetController(IAsset assetParam, IAssetCategory assetCparam, IRequestAsset reqAssetParam)
        {
            assetRepo = assetParam;
            assetmanager = new AssetManager(assetParam,assetCparam,reqAssetParam);
            assetCategoryRepo = assetCparam;
            reqAssetRepo = reqAssetParam;
        }
        // GET: Asset
        public ActionResult Index()
        {
            ViewBag.PageTitle = "All Assets";
            var result = assetmanager.GetAllAssetLazily();
            return View(result);
        }
        [CustomAuthorizationFilter(Roles = "System Admin, HR")]
        public ActionResult AddAsset()
        {
            ViewBag.PageTitle = "New Asset";
            AssetViewModel model = new AssetViewModel()
            {
                CategotyList = assetmanager.InstantiateCategoryDropDown()
            };
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddAsset(AssetViewModel model, HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                if (File != null)
                {
                    var extension = Path.GetExtension(File.FileName);
                    if (extension.ToLower().Contains(".jpg") || extension.ToLower().Contains(".png"))
                    {
                        this.AddNotification("Please only Images with .jpg or .png are allowed", NotificationType.ERROR);
                        return RedirectToAction("AddAsset");
                    }
                }
                    var result = assetmanager.AddAsset(model, File);
                    if (result)
                    {
                        this.AddNotification("Asset added!", NotificationType.SUCCESS);
                        return RedirectToAction("Index");
                    }       
            }
            return View(model);
        }
        [CustomAuthorizationFilter(Roles ="System Admin,HR")]
        public ActionResult EditAsset(int id)
        {
            var oldAsset = assetRepo.GetById(id);
            if (oldAsset != null )
            {
                ViewBag.PageTitle = "Edit Asset";
                return View(oldAsset);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditAsset(AssetViewModel model)
        {
            if (model.ID.ToString() != null)
            {
                var result = assetmanager.EditAsset(model);
                if (result)
                {
                    this.AddNotification("Asset Updated!", NotificationType.SUCCESS);
                    return RedirectToAction("Index");
                }
            }
            this.AddNotification("Sorry the asset could not be modified because the asset was not found. please make sure your request is not been Edited or contact your System Adminstrator", NotificationType.SUCCESS);
            return RedirectToAction("Index");

        }
        [CustomAuthorizationFilter(Roles ="System Admin, HR")]
        public ActionResult AllAssetCategory()
        {
            ViewBag.PageTitle = "All Category";
            return View(assetCategoryRepo.Get());
        }
        [CustomAuthorizationFilter(Roles = "System Admin,HR")]
        public ActionResult AddAssetCategory()
        {
            ViewBag.PageTitle = "New Asset Category";
            AssetViewModel model = new AssetViewModel();
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddAssetCategory(AssetViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = assetmanager.AddAssetCategory(model);
                if (result)
                {
                    this.AddNotification("Category added!", NotificationType.SUCCESS);
                    return RedirectToAction("AllAssetCategory");
                }
            }
            return View(model);
        }
        [CustomAuthorizationFilter(Roles = "System Admin,HR")]
        public ActionResult EditAssetCategory(int id)
        {
            var oldAsset = assetCategoryRepo.GetById(id);
            if (oldAsset != null)
            {
                ViewBag.PageTitle = "Edit Category";
                return View(oldAsset);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditAssetCategory(AssetViewModel model)
        {
            if (model.ID.ToString() != null)
            {
                var result = assetmanager.EditAssetCategory(model);
                if (result)
                {
                    this.AddNotification("Asset Updated!", NotificationType.SUCCESS);
                    return RedirectToAction("AllAssetCategory");
                }
            }
            this.AddNotification("Sorry the Category could not be modified because the Category was not found. please make sure your request is not been Edited or contact your System Adminstrator", NotificationType.SUCCESS);
            return RedirectToAction("AllAssetCategory");
        }
        [CustomAuthorizationFilter(Roles = "HR")]
        public ActionResult AllRequest()
        {
            ViewBag.PageTitle = "All Request";
            var result = assetmanager.GetAllRequestByUser();
            return View(result);
        }
        public ActionResult RequestAsset()
        {
            RequestAssetViewModel request = new RequestAssetViewModel()
            {
                CategotyList = assetmanager.InstantiateCategoryDropDown()
            };
            ViewBag.PageTitle = "Add Request";
            return View(request);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult RequestAsset(RequestAssetViewModel model)
        {
            var result = assetmanager.AddRequestForAsset(model);
            if (result)
            {
                this.AddNotification("Request added!", NotificationType.SUCCESS);
                return RedirectToAction("AllRequest");
            }
            this.AddNotification("Oops! your request could not be added, kindly make sure you provide all required details", NotificationType.ERROR);
            return RedirectToAction("RequestAsset");
        }
        [CustomAuthorizationFilter(Roles ="HR"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult AcceptRequest(int id)
        {
            var result = assetmanager.AcceptRequest(id);
            if (result)
            {
                this.AddNotification("Request accepted!", NotificationType.SUCCESS);
                return RedirectToAction("AllRequest");
            }
            this.AddNotification("Oops! something went wrong, please make sure you are not manually editing your request", NotificationType.ERROR);
            return RedirectToAction("AllRequest");
        }

        [CustomAuthorizationFilter(Roles = "HR"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult DenyRequest(int id)
        {
            var result = assetmanager.RejectRequest(id);
            if (result)
            {
                this.AddNotification("Request accepted!", NotificationType.SUCCESS);
                return RedirectToAction("AllRequest");
            }
            this.AddNotification("Oops! something went wrong, please make sure you are not manually editing your request", NotificationType.ERROR);
            return RedirectToAction("AllRequest");
        }
        //public ActionResult EditRequest()
        //{

        //}
        //public ActionResult AssignAsset()
        //{

        //}

    }
}