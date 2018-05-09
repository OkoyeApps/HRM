using Microsoft.AspNet.Identity;
using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
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
        public IEnumerable<Asset> GetAllAssetLazily()
        {
            return assetRepo.GetallAssetLazily("AssetCategory");
        }
        public bool AddAsset(AssetViewModel model, HttpPostedFileBase File, int groupId)
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
                    groupId = groupId
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
                oldAsset.SerialNumber =  model.SerialNumber;
                oldAsset.ModifiedBy = HttpContext.Current.User.Identity.GetUserId();
                oldAsset.ModifiedOn = DateTime.Now;
                assetRepo.update(oldAsset);
                return true;  
            }
            return false;
        }

        public IEnumerable<AssetCategory> GetAllAssetCategoryByGroup(int groupId)
        {
            return unitOfWork.AssetCategory.Get(filter: x=>x.GroupId == groupId);
        }
        public bool AddAssetCategory(AssetViewModel model, int groupId)
        {
            if (model.Name != null && model.SerialNumber.ToString() != null)
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
                RequestAsset reqAssest = new RequestAsset()
                {
                    Amount = model.Amount,
                    AssetCategoryId = model.Category,
                    createdBy = HttpContext.Current.User.Identity.GetUserId(),
                    CreatedOn = DateTime.Now,
                    DueTime = model.DueTime,
                    RequestedBy = model.RequestedBy
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
                var result = unitOfWork.RequestAsset.Get(filter: x=>x.GroupId == groupId && x.LocationId == locationId, includeProperties:"AssetCategory");
                return result;
            }
            var empResult = unitOfWork.RequestAsset.Get(filter: x => x.RequestedBy == HttpContext.Current.User.Identity.GetUserId());
            return empResult;
        }
        public bool AcceptRequest(int id)
        {
            var asset = unitOfWork.RequestAsset.Get(filter: x => x.AssetCategoryId == id).FirstOrDefault();
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
            var asset = unitOfWork.RequestAsset.Get(filter: x => x.AssetCategoryId == id).FirstOrDefault();
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

    }
}