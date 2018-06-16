using Microsoft.AspNet.Identity;
using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
using resourceEdge.webUi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace resourceEdge.webUi.Infrastructure.SystemManagers
{
    public class AssetManager
    {
        IAsset assetRepo;
        IAssetCategory assetCategoryRepo;
        IRequestAsset reqAssetRepo;
        Domain.UnitofWork.UnitOfWork unitOfWork = new Domain.UnitofWork.UnitOfWork();
        public AssetManager()
        {

        }
        public AssetManager(IAsset assetParam, IAssetCategory assetCategoryParam, IRequestAsset reqAssetParam)
        {
            assetRepo = assetParam;
            assetCategoryRepo = assetCategoryParam;
            reqAssetRepo = reqAssetParam;
        }
        public SelectList InstantiateCategoryDropDown(int groupId)
        {
            var categoryList = new SelectList(unitOfWork.AssetCategory.Get(filter: x=>x.GroupId == groupId).Select(x => new { name = x.Name, Id = x.ID }), "Id", "Name");
            return categoryList;
        }
        public IEnumerable<Asset> GetAllAssetLazily(int? groupId, int? locationId)
        {
            IEnumerable<Asset> result = null;
            if(groupId != null && locationId != null)
            {
                 result = unitOfWork.Asset.Get(filter: x => x.GroupId == groupId && x.LocationId == locationId, includeProperties: "AssetCategory");
            }
            else
            {
                result = unitOfWork.Asset.Get(includeProperties: "AssetCategory");
            }
            return result;
        }
        public bool AddAsset(AssetViewModel model, HttpPostedFileBase File, int groupId, int locationId)
        {
            if (model.Name != null && model.SerialNumber.ToString() != null)
            {
                Asset asset = new Asset()
                {
                    Name = model.Name,
                    AssetCategoryId = model.Category,
                    CreatedOn = DateTime.Now,
                    CreatedBy = HttpContext.Current.User.Identity.GetUserId(),
                    IsInUse = false,
                    SerialNumber = model.SerialNumber,
                    GroupId = groupId,
                    LocationId = locationId
                };
                if (File != null)
                {
                    var Foldername = HttpContext.Current.Server.MapPath("~/Files/Asset/");
                    string fileName = Path.GetFileNameWithoutExtension(File.FileName);
                    string extenstion = Path.GetExtension(File.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssff") + extenstion;
                    asset.ImageUrl = "~/Files/Asset/" + fileName;
                    fileName = Path.Combine(Foldername + fileName);
                    File.SaveAs(fileName);
                }
                assetRepo.Insert(asset);
                return true;
            }
            return false;
        }
        public bool EditAsset(AssetViewModel model)
        {
            var oldAsset = assetRepo.GetById(model.ID);
            if (oldAsset != null)
            {
                oldAsset.Name = model.Name;
                oldAsset.SerialNumber = model.SerialNumber;
                oldAsset.ModifiedBy = HttpContext.Current.User.Identity.GetUserId();
                oldAsset.ModifiedOn = DateTime.Now;
                assetRepo.update(oldAsset);
                return true;  
            }
            return false;
        }

        public IEnumerable<AssetCategory> GetAllAssetCategoryByGroup(int? groupId)
        {
            IEnumerable<AssetCategory> result = null;
            if (groupId != null)
            {
                result = unitOfWork.AssetCategory.Get(filter: x => x.GroupId == groupId);
            }
            else
            {
                result = unitOfWork.AssetCategory.Get();
            }
            return result;
        }
        public bool AddAssetCategory(AssetViewModel model, int groupId)
        {
            if (model.Name != null)
            {
                AssetCategory asset = new AssetCategory()
                {
                    Name = model.Name,
                    IsActive = true,
                    CreatedOn = DateTime.Now,
                    CreatedBy = HttpContext.Current.User.Identity.GetUserId(),
                     GroupId = groupId
                };
                assetCategoryRepo.Insert(asset);
                return true;
            }
            return false;
        }
        public bool EditAssetCategory(AssetViewModel model)
        {
            var oldAsset = assetCategoryRepo.GetById(model.ID);
            if (oldAsset != null)
            {
                oldAsset.Name = model.Name;
                oldAsset.ModifiedBy = HttpContext.Current.User.Identity.GetUserId();
                oldAsset.ModifiedOn = DateTime.Now;
                assetCategoryRepo.update(oldAsset);
                return true;
            }
            return false;
        }

        public bool AddRequestForAsset(RequestAssetViewModel model)
        {
            if (model.Amount > 0 && !string.IsNullOrEmpty(model.Category.ToString()))
            {
                var userFromSession =(SessionModel) HttpContext.Current.Session["_ResourceEdgeTeneceIdentity"];
                RequestAsset reqAssest = new RequestAsset()
                {
                    Amount = model.Amount,
                    AssetCategoryId = model.Category,
                    createdBy = HttpContext.Current.User.Identity.GetUserId(),
                    CreatedOn = DateTime.Now,
                    DueTime = model.DueTime,
                    RequestedBy = model.RequestedBy,
                    LocationId = userFromSession.LocationId.Value,
                    GroupId = userFromSession.GroupId.Value,
                     AssetId = model.AssetId
                };
                var fullName = unitOfWork.employees.Get(filter: x => x.userId == reqAssest.createdBy).FirstOrDefault().FullName;
                reqAssest.CreatedByFullName = fullName;
                reqAssetRepo.Insert(reqAssest);
                return true;
            }
            return false;
        }
        public IEnumerable<RequestAsset> GetAllRequestByUser(int groupId, int locationId)
        {
            if (HttpContext.Current.User.IsInRole("HR"))
            {          
                var result = unitOfWork.RequestAsset.Get(filter: x=>x.GroupId == groupId && x.LocationId == locationId, includeProperties:"AssetCategory,Asset");
                return result.OrderBy(x=>x.DueTime);
            }
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var empResult = unitOfWork.RequestAsset.Get(filter: x => x.createdBy ==userId, includeProperties: "AssetCategory,Asset");
            return empResult.OrderBy(x => x.DueTime);
        }
        public bool AcceptRequest(int id)
        {
            var asset = unitOfWork.RequestAsset.Get(filter: x => x.Id == id).FirstOrDefault();
            if (asset != null)
            {
                asset.IsResolved = true;
                asset.ModifiedBy = HttpContext.Current.User.Identity.GetUserId();
                asset.ModifiedOn = DateTime.Now;
                unitOfWork.RequestAsset.Update(asset);
                unitOfWork.Save();
                return true;
            }
            return false;
        }
        public bool RejectRequest(int id)
        {
            var asset = unitOfWork.RequestAsset.Get(filter: x => x.Id == id).FirstOrDefault();
            if (asset != null)
            {
                asset.IsResolved = false;
                asset.ModifiedBy = HttpContext.Current.User.Identity.GetUserId();
                asset.ModifiedOn = DateTime.Now;
                unitOfWork.RequestAsset.Update(asset);
                unitOfWork.Save();
                return true;
            }
            return false;
        }

        public bool DeleteRequest(int id)
        {
            var user = HttpContext.Current.User.Identity.GetUserId();
            var userRequest = unitOfWork.RequestAsset.Get(filter: x => x.Id == id && x.createdBy == user).FirstOrDefault();
            if (userRequest != null)
            {
                unitOfWork.RequestAsset.Delete(userRequest);
                unitOfWork.Save();
                return true;
            }
            return false;
        }

        public Asset GetAssetDetail(int id)
        {
            var asset = unitOfWork.Asset.Get(filter: x =>x.ID == id, includeProperties: "AssetCategory,Group,Location").FirstOrDefault();
            return asset;
        }
        public IEnumerable<RequestAssetViewModel> AllRequestHistory(int groupId, int LocationId)
        {
            var History = unitOfWork.RequestAsset.Get(filter: x => x.GroupId == groupId && x.LocationId == LocationId, includeProperties: "AssetCategory,Asset")
                .Select(x => new RequestAssetViewModel
                {
                    Amount = x.Amount,
                    CreatedByFullName = x.CreatedByFullName,
                    DueTime = x.DueTime,
                    RequestedBy = x.RequestedBy,
                     CategoryName = x.AssetCategory.Name,
                   AssetName = x.Asset.Name,
                   IsResolved=  x.IsResolved
                });
            return History;
        }
    }
}