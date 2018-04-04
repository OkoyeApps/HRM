using Microsoft.AspNet.Identity;
using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
using resourceEdge.Domain.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace resourceEdge.webUi.Infrastructure
{
    public class ConfigurationManager
    {
        private IBusinessUnits BUnitRepo;
        UnitOfWork unitOfWork = new UnitOfWork();
        public ConfigurationManager()
        {

        }
        public ConfigurationManager(IBusinessUnits UParam)
        {
            BUnitRepo = UParam;
        }

        public bool DoesIdentityCodeExistForGroup(int groupId)
        {
            var identityCode = unitOfWork.identityCodes.Get(filter: x => x.GroupId == groupId).FirstOrDefault();
            return identityCode != null ? true : false;
        }

        public bool DoesUnitExstInLocation(int locationId, string LocationName)
        {
            var location = BUnitRepo.DoesUnitExitByName(LocationName); //This code checks if the Busines sunit already exist in the db and then if it doesnt
            if (location != null && location.LocationId == locationId) //If it does it check if it has the same location
            {
                return true;
            }
            return false;
        }

        public bool AddOrUpdateDepartment(DepartmentViewModel model = null, int? id = null)
        {
            string UserId = HttpContext.Current.User.Identity.GetUserId();

            if (model != null)
            {
                Departments Dept = new Departments()
                {
                    BunitId = model.BunitId.Value,
                    CreatedBy = UserId,
                    CreatedDate = DateTime.Now,
                    deptcode = model.deptcode,
                    deptname = model.deptname,
                    descriptions = model.descriptions,
                    Isactive = true,
                    startdate = model.StartDate
                };
                unitOfWork.Department.Insert(Dept);
                unitOfWork.Save();
                return true;
            }
            if (id != null)
            {
                var dept = unitOfWork.Department.GetByID(id);
                dept.deptcode = model.deptcode;
                dept.deptname = model.deptname;
                dept.startdate = model.StartDate;
                unitOfWork.Department.Update(dept);
                unitOfWork.Save();
                return true;
            }
            return false;
        }

        public bool AddOrUpdateLocation(FormCollection collection, int? id = null, LocationViewModel model = null)
        {
            try
            {
                if (collection != null)
                {
                    model = new LocationViewModel();
                    var UserId = HttpContext.Current.User.Identity.GetUserId();
                    var allKeys = collection.AllKeys;
                    var allState = allKeys.Where(x => x.ToLower().StartsWith("state")).ToList();
                    var Group = allKeys.Where(x => x.ToLower().StartsWith("group")).FirstOrDefault();
                    var allCity = allKeys.Where(x => x.ToLower().StartsWith("city")).ToList();
                    var allCountry = allKeys.Where(x => x.ToLower().StartsWith("country")).ToList();
                    var allAddress1 = allKeys.Where(x => x.ToLower().StartsWith("address1")).ToList();
                    var allAddress2 = allKeys.Where(x => x.ToLower().StartsWith("address2")).ToList();
                    allKeys.ToList().RemoveRange(0,2);

                    int GroupId = 0;
                    int.TryParse(collection[Group], out GroupId);
                    for (int i = 0; i < allCountry.Count; i++)
                    {
                        Location location = new Location()
                        {
                            Address1 = collection[allAddress1[i].ToString()],
                            City = collection[allCity[i].ToString()],
                            Country = collection[allCountry[i].ToString()],
                            CreatedBy = UserId,
                            GroupId = GroupId,
                            State = collection[allState[i].ToString()],
                            CreatedOn = DateTime.Now
                        };
                        if (allAddress2.Count > 0)
                        {
                            if (allAddress2[i] != null)
                            {
                                location.Address2 = collection[allAddress2[i].ToString()] ?? null;
                            }
                        }
                        unitOfWork.Locations.Insert(location);
                    }
                    unitOfWork.Save();
                    return true;
                }
                else if (id != null)
                {
                    var location = unitOfWork.Locations.GetByID(id);
                    location.Address1 = model.Address1;
                    location.State = model.State;
                    location.City = model.State;
                    location.Address2 = model.Address2;
                    unitOfWork.Locations.Update(location);
                    unitOfWork.Save();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }

        public bool AddorUpdatePrefix(FormCollection collection = null, int? Id = null, prefixViewModel model = null)
        {
            try
            {
                if (collection != null)
                {
                    var UserId = HttpContext.Current.User.Identity.GetUserId();
                    var allKeys = collection.AllKeys;
                    var allNames = allKeys.Where(x => x.ToLower().StartsWith("prefixname")).ToList();
                    var allDescription = allKeys.Where(x => x.ToLower().StartsWith("description")).ToList();
                    allKeys.ToList().RemoveRange(0, 2);
                    var Length = (allKeys.Length - 1) / 2;
                    for (int i = 0; i < Length; i++)
                    {
                        model = new prefixViewModel();
                        Prefix prefix = new Prefix()
                        {
                            prefixName = collection[allNames[i].ToString()],
                            description = collection[allDescription[i].ToString()],
                            createdby = UserId,
                            createddate = DateTime.Now,
                            isactive = true
                        };
                        if (allDescription.Count() > 0)
                        {
                            if (allDescription[i] != null)
                            {
                                prefix.description = collection[allDescription[i].ToString()] ?? null;
                            }
                        }
                        unitOfWork.prefix.Insert(prefix);
                    }
                    unitOfWork.Save();
                    return true;
                }
                else if (Id != null)
                {
                    var prefix = unitOfWork.prefix.GetByID(Id);
                    prefix.prefixName = model.prefixName;
                    unitOfWork.prefix.Update(prefix);
                    unitOfWork.Save();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }
        public bool AddorUpdateEmploymentStatus(FormCollection collection = null, int? Id = null, employeeStatusViewModel model = null)
        {
            try
            {
                if (collection != null)
                {
                    var UserId = HttpContext.Current.User.Identity.GetUserId();
                    var allKeys = collection.AllKeys;
                    var allStatus = allKeys.Where(x => x.ToLower().StartsWith("employementstatus")).ToList();

                    allKeys.ToList().RemoveRange(0, 2);
                    var Length = (allKeys.Length - 1);
                    for (int i = 0; i < Length; i++)
                    {
                        EmploymentStatus status = new EmploymentStatus()
                        {
                            createdby = UserId,
                            createddate = DateTime.Now,
                            employemntStatus = collection[allStatus[i].ToString()],
                            isactive = true
                        };
                        unitOfWork.employmentStatus.Insert(status);
                    }
                    unitOfWork.Save();
                    return true;
                }
                else if (Id != null)
                {
                    var status = unitOfWork.employmentStatus.GetByID(Id);
                    status.employemntStatus = model.employemntStatus;
                    unitOfWork.employmentStatus.Update(status);
                    unitOfWork.Save();
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }
        public bool AddOrUpdateJobs(FormCollection collection = null, int? Id = null, JobtitlesVIewModel model = null)
        {
            try
            {
                if (collection != null)
                {
                    model = new JobtitlesVIewModel();
                    var UserId = HttpContext.Current.User.Identity.GetUserId();
                    var allKeys = collection.AllKeys;
                    var allTitleName = allKeys.Where(x => x.ToLower().StartsWith("jobtitlename")).ToList();
                    var allExperience = allKeys.Where(x => x.ToLower().StartsWith("minexperiencerequired")).ToList();
                    var allCode = allKeys.Where(x => x.ToLower().StartsWith("jobtitlecode")).ToList();
                    var allPayFreq = allKeys.Where(x => x.ToLower().StartsWith("jobpayfrequency")).ToList();
                    var allDescription = allKeys.Where(x => x.ToLower().StartsWith("jobdescription")).ToList();
                    var allGradeCode = allKeys.Where(x => x.ToLower().StartsWith("jobpaygradecode")).ToList();
                    var Group = allKeys.Where(x => x.ToLower().StartsWith("group")).FirstOrDefault();
                    allKeys.ToList().RemoveRange(0, 2);
                    int groupId = 0;
                    double experience = 0;
                    //int.TryParse(Group, out groupId);
                    for (int i = 0; i < allKeys.Length; i++)
                    {
                        double.TryParse(collection[allExperience[i].ToString()], out experience);
                        int.TryParse(collection[Group], out groupId);
                        if (experience != 0 && groupId != 0)
                        {
                            Jobtitle job = new Jobtitle()
                            {
                                createdby = UserId,
                                createddate = DateTime.Now,
                                GroupId = groupId,
                                jobdescription = collection[allDescription[i].ToString()] != "" ? collection[allDescription[i].ToString()] : null,
                                jobpayfrequency = collection[allPayFreq[i].ToString()] != "" ? collection[allPayFreq[i].ToString()] : null,
                                jobpaygradecode = collection[allGradeCode[i].ToString()] != "" ? collection[allGradeCode[i].ToString()] : null,
                                jobtitlecode = collection[allCode[i].ToString()],
                                jobtitlename = collection[allTitleName[i].ToString()],
                                minexperiencerequired = experience,
                                isactive = true
                            };
                            unitOfWork.jobTitles.Insert(job);
                    unitOfWork.Save();
                    return true;
                        }
                    }
                }
                else if(Id != null)
                {
                    var job = unitOfWork.jobTitles.GetByID(Id);
                    job.jobpayfrequency = model.jobpayfrequency;
                    job.jobpaygradecode = model.jobpaygradecode;
                    job.jobtitlecode = model.jobtitlecode;
                    job.jobtitlename = model.jobtitlename;
                    job.minexperiencerequired = model.minexperiencerequired;
                    unitOfWork.jobTitles.Update(job);
                    unitOfWork.Save();
                    return true;
                }
            }catch(Exception ex)
            {
                throw ex;
            }
            return false;
        }
        public bool AddOrUpdatePosition(FormCollection collection, int? Id = null, positionViewModel model = null)
        {
            try
            {
                if (collection != null && Id == null)
                {
                    var UserId = HttpContext.Current.User.Identity.GetUserId();
                    var allKeys = collection.AllKeys;
                    var allName = allKeys.Where(x => x.ToLower().StartsWith("positionname")).ToList();
                    var allJobId = allKeys.Where(x => x.ToLower().StartsWith("jobtitleid")).FirstOrDefault();
                    var allDescription = allKeys.Where(x => x.ToLower().StartsWith("description")).ToList();
                    allKeys.ToList().RemoveRange(0, 2);
                    int jobId = 0;
                    var Length =(allKeys.Length - 1) / 3;
                    for (int i = 0; i < Length; i++)
                    {
                    int.TryParse(collection[allJobId], out jobId);
                        if (jobId != 0)
                        {
                            Position position = new Position()
                            {

                                createdby = UserId,
                                createddate = DateTime.Now,
                                description = collection[allDescription[i].ToString()],
                                jobtitleid = jobId,
                                positionname = collection[allName[i].ToString()],
                                isactive = true
                            };
                            unitOfWork.positions.Insert(position);
                            unitOfWork.Save();
                        }
                    }
                    return true;
                }
                else if (Id != null)
                {
                    var position = unitOfWork.positions.GetByID(Id);
                    position.positionname = model.positionname;
                    position.jobtitleid = model.jobtitleid.Value;
                    unitOfWork.positions.Update(position);
                    unitOfWork.Save();
                    return true;
                }
            }catch(Exception ex)
            {
                throw ex;
            }
            return false;
        }
        public bool AddOrUpdateLevel(FormCollection collection, int? Id = null, LevelsViewModel model = null)
        {
            try
            {
                if (collection != null)
                {
                    var UserId = HttpContext.Current.User.Identity.GetUserId();
                    var allKeys = collection.AllKeys;
                    var allName = allKeys.Where(x => x.ToLower().StartsWith("LevelName")).ToList();
                    var allYears = allKeys.Where(x => x.ToLower().StartsWith("EligibleYears")).ToList();
                    var allLevelNo = allKeys.Where(x => x.ToLower().StartsWith("levelNo")).ToList();
                    var Group = allKeys.Where(x => x.ToLower().StartsWith("group")).FirstOrDefault();

                    allKeys.ToList().RemoveRange(0, 2);

                    int GroupId = 0;
                    int.TryParse(collection[Group], out GroupId);
                    int year = 0;
                    int levelNo = 0;
                    for (int i = 0; i < allKeys.Length; i++)
                    {
                        int.TryParse(collection[allYears[i].ToString()], out year);
                        int.TryParse(collection[allLevelNo[i].ToString()], out levelNo);
                        Level level = new Level()
                        {
                            CreatedBy = UserId,
                            CreatedOn = DateTime.Now,
                            EligibleYears = year,
                            LevelName = collection[allName[i].ToString()],
                            levelNo = levelNo,
                             GroupId = GroupId
                        };
                        unitOfWork.Levels.Insert(level);
                    }
                    unitOfWork.Save();
                    return true;
                }
                else if(Id != null)
                {
                    var level = unitOfWork.Levels.GetByID(Id);
                    level.LevelName = model.LevelName;
                    level.levelNo = model.levelNo;
                    unitOfWork.Levels.Update(level);
                    unitOfWork.Save();
                    return true;
                }
            }catch(Exception ex)
            {
                throw ex;
            }
            return false;
        }
    }
}