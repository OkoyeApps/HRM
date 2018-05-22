using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
using resourceEdge.webUi.Infrastructure.Core;
using resourceEdge.webUi.Infrastructure.Handlers;
using resourceEdge.webUi.Infrastructure.SystemManagers;
using resourceEdge.webUi.Models;
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
        FileManager FManager;
        public AssetController(IAsset assetParam, IAssetCategory assetCparam, IRequestAsset reqAssetParam, FileManager fMParam)
        {
            assetRepo = assetParam;
            assetmanager = new AssetManager(assetParam,assetCparam,reqAssetParam);
            assetCategoryRepo = assetCparam;
            reqAssetRepo = reqAssetParam;
            FManager = fMParam;
        }
        // GET: Asset
        public ActionResult Index()
        {
            ViewBag.PageTitle = "All Assets";
            var UserFromSession = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            var result = assetmanager.GetAllAssetLazily(UserFromSession.GroupId, UserFromSession.LocationId);
            return View(result);
        }
        [CustomAuthorizationFilter(Roles = "System Admin, HR")]
        public ActionResult AddAsset()
        {
            ViewBag.PageTitle = "New Asset";
            var UserFromSession = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            AssetViewModel model = new AssetViewModel()
            {
                CategotyList = assetmanager.InstantiateCategoryDropDown(UserFromSession.GroupId)
            };
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddAsset(AssetViewModel model, HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                var UserFromSession = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
                if (File != null)
                {
                    var extension = Path.GetExtension(File.FileName);
                    var equal = !extension.ToLower().Equals(".jpg");
                    if (!extension.ToLower().Equals(".jpg") && !extension.ToLower().Equals(".png"))
                    {
                        this.AddNotification("Please only Images with .jpg or .png are allowed", NotificationType.ERROR);
                        return RedirectToAction("AddAsset");
                    }
                    bool FileSize = FManager.ValidateFileSize(File);
                    if (!FileSize)
                    {
                        this.AddNotification("Please The image must be less than 500(kb) in size", NotificationType.ERROR);
                        return RedirectToAction("AddAsset");
                    }
                }
                    var result = assetmanager.AddAsset(model, File,UserFromSession.GroupId, UserFromSession.LocationId);
                    if (result)
                    {
                        this.AddNotification("Asset added!", NotificationType.SUCCESS);
                        return RedirectToAction("Index");
                    }       
            }

            return RedirectToAction("AddAsset");
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
            var UserFromSession = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            return View(assetmanager.GetAllAssetCategoryByGroup(UserFromSession.GroupId));
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
                var UserFromSession = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
                var result = assetmanager.AddAssetCategory(model, UserFromSession.GroupId);
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
        //[CustomAuthorizationFilter(Roles = "HR")]
        public ActionResult AllRequest()
        {
            ViewBag.PageTitle = "All Request";
            var UserFromSession = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            var result = assetmanager.GetAllRequestByUser(UserFromSession.GroupId, UserFromSession.LocationId);
            return View(result);
        }
        public ActionResult RequestAsset()
        {
            var UserFromSession = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            RequestAssetViewModel request = new RequestAssetViewModel()
            {
                CategotyList = assetmanager.InstantiateCategoryDropDown(UserFromSession.GroupId)
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
                this.AddNotification("Request Denied!", NotificationType.SUCCESS);
                return RedirectToAction("AllRequest");
            }
            this.AddNotification("Oops! something went wrong, please make sure you are not manually editing your request", NotificationType.ERROR);
            return RedirectToAction("AllRequest");
        }

        public PartialViewResult AssetDetail(int id)
        {
            var result = assetmanager.GetAssetDetail(id);
            return PartialView(result);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult DeleteRequest(int id)
        {
            var result = assetmanager.DeleteRequest(id);
            if (result)
            {
                this.AddNotification("Request Deleted", NotificationType.SUCCESS);
                return RedirectToAction("AllRequest");
            }
            this.AddNotification("Oops!something went wrong, please make sure you are not manually editing your request and if the problem persist please contact your system administrator", NotificationType.ERROR);
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