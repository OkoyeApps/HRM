using Microsoft.AspNet.Identity;
using resourceEdge.Domain.Entities;
using resourceEdge.webUi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace resourceEdge.webUi.Controllers
{
    //[RoutePrefix("api/Settings")]
    public class SettingsController : ApiController
    {

        [Route("api/Settings/GetempStatus")]
        [HttpGet]
        public IHttpActionResult Status()
        {
            return Ok(Apimanager.empStatusList());
        }

        [Route("api/Settings/GetJobs")]
        [HttpGet]
        public IHttpActionResult Jobs()
        {
            var result = Apimanager.JobList();
            if (result == null)
            {
                var emptyResult = new { Message = "No file Found" };
                return Ok(emptyResult);
            }
            else
            {
                return Ok(result);
            }
        }
        [Route("api/Settings/GetPosition")]
        [HttpGet]
        public IHttpActionResult Position()
        {
            return Ok(Apimanager.GetPositionList());
        }
        [Route("api/Settings/GetPositionById/{id:int}")]
        [HttpGet]
        public IHttpActionResult PositionById(int id)
        {
            var result = Apimanager.GetPositionById(id);
            return Ok(result);
        }

        [Route("api/Settings/GetEmployeeCode")]
        [HttpGet]
        public IHttpActionResult EmployeeCode(int id)
        {
            return Ok(Apimanager.GetIdntityListByGroup(id));
        }
        [Route("api/Settings/GetEmployeeCodeByGroup/{id:int}")]
        [HttpGet]
        public IHttpActionResult EmployeeCodeById(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            return Ok(Apimanager.GetIdntityListByGroup(id.Value));
        }

        [Route("api/Settings/GetBusinessUnit")]
        [HttpGet]
        public IHttpActionResult BusinessUnit()
        {
            return Ok(Apimanager.GetBusinessUnitList());
        }

        [Route("api/Settings/GetPrefixes")]
        [HttpGet]
        public IHttpActionResult Prefixes()
        {
            return Ok(Apimanager.PrefixeList());
        }
        [Route("api/Settings/GetDepartments")]
        [HttpGet]
        public IHttpActionResult Departments()
        {
            return Ok(Apimanager.DepartmentList());
        }

        [Route("api/Settings/GetDepartmentsById/{id:int}")]
        [HttpGet]
        public IHttpActionResult DepartmentId(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            else
            {
                var dept = Apimanager.DepartmentById(id);
                if (dept == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(dept);
                }
            }
        }

        [Route("api/Settings/GetEmpByBusinessUnit/{id:int}")]
        [HttpGet]
        public IHttpActionResult GetEmpByBusinessUnit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var result = Apimanager.GetEmpByBusinessUnit(id.Value);
            if (result == null)
            {
                return NotFound();
            }

            else
            {
                return Ok(result);
            }
        }

        [Route("api/Settings/GetEligibleReportManager/{id:int}")]
        [HttpGet]
        public IHttpActionResult GetEligibleReportManager(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var result = Apimanager.GetEligibleManagerBybBusinessUnit(id.Value);
            if (result == null)
            {
                return NotFound();
            }

            else
            {
                return Ok(result);
            }
        }

        [Route("api/Settings/GetRMByBusinessUnit/{id:int}")]
        [HttpGet]
        public IHttpActionResult GetRMByBusinessUnit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var result = Apimanager.GetReportManagerBusinessUnit(id.Value);
            if (result == null)
            {
                return NotFound();
            }

            else
            {
                return Ok(result);
            }
        }
        [Route("api/Settings/GetEmpByDept/{id:int}")]
        [HttpGet]
        public IHttpActionResult GetEmpByDept(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var result = Apimanager.GetEmployeeByDept(id.Value);
            if (result == null)
            {
                return NotFound();
            }

            else
            {
                return Ok(result);
            }
        }

        [Route("api/Settings/GetempLeaveAmount/{id}")]
        [HttpGet]
        public IHttpActionResult GetempLeaveAmount(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var result = Apimanager.getEmpLeaveByUserId(id);
            if (result == null)
            {
                return NotFound();
            }

            else
            {
                return Ok(result);
            }
        }

        [Route("api/Settings/GetempLeaveTypeAmount/{id:int}")]
        [HttpGet]
        public IHttpActionResult GetempLeaveAmount(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var result = Apimanager.LeaveTypeById(id.Value);
            if (result == null)
            {
                return NotFound();
            }

            else
            {
                return Ok(result);
            }
        }

        [Route("api/settings/GetRmByUserId/{userId}")]
        [HttpGet]
        public IHttpActionResult GetRmByUserId(string userId)
        {
            if (userId == null)
            {
                return BadRequest();
            }
            var result = Apimanager.GetReportManagrbyUserId(userId);
            if (result == null)
            {
                return Ok("No result found");
            }
            return Ok(result);
        }

        [Route("api/settings/GetEmployeePendingLeave/{userId}")]
        [HttpGet]
        public IHttpActionResult GetEmployeePendingLeave(string userId)
        {
            if (userId == null)
            {
                return BadRequest();
            }
            var result = Apimanager.GetEmployeePendingLeave(userId);
            if (result == null)
            {
                return Ok("No result found");
            }
            return Ok(result);
        }
        [Route("api/settings/GetEmployeeApprovedLeave/{userId}")]
        [HttpGet]
        public IHttpActionResult GetEmployeeApprovedLeave(string userId)
        {
            if (userId == null)
            {
                return BadRequest();
            }
            var result = Apimanager.GetEmployeeApprovedLeave(userId);
            if (result == null)
            {
                return Ok("No result found");
            }
            return Ok(result);
        }
        [Route("api/settings/GetEmployeeDeniedLeave/{userId}")]
        [HttpGet]
        public IHttpActionResult GetEmployeeDeniedLeave(string userId)
        {
            if (userId == null)
            {
                return BadRequest();
            }
            var result = Apimanager.GetEmployeeDeniedLeave(userId);
            if (result == null)
            {
                return Ok("No result found");
            }
            return Ok(result);
        }
        [Route("api/settings/GetEmployeeAllLeave/{userId}")]
        [HttpGet]
        public IHttpActionResult GetEmployeeAllLeave(string userId)
        {
            if (userId == null)
            {
                return BadRequest();
            }
            var result = Apimanager.GetEmployeeAllLeave(userId);
            if (result == null)
            {
                return Ok("No result found");
            }
            return Ok(result);
        }

        //This is for test but the real method is in the employee controller
        //so this can be cleaned later;
        [Route("api/settings/GetEmpTeamMeber/{userId}/{searchstring}")]
        [HttpGet]
        public IHttpActionResult GetEmpTeamMeber(string userId, string searchString)
        {
            if (userId == null)
            {
                return BadRequest();
            }
            var result = Apimanager.GetUnitMembersBySearch(userId,searchString);
            if (result == null)
            {
                return Ok("No result found");
            }
            return Ok(result);
        }
        [Route("api/settings/GetBusinessunitByLocation/{id}")]
        [HttpGet]
        public IHttpActionResult GetBusinessunitByLocation(int? id=null)
        {
            var result = Apimanager.GetBusinessunitByLocation(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [Route("api/settings/GetLocationByGroup/{id:int}")]
        [HttpGet]
        public IHttpActionResult GetLocationByGroup(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var result = Apimanager.GetLocationByGroup(id.Value);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [Route("api/settings/GetAllHrsByGroup/{id:int}")]
        [HttpGet]
        public IHttpActionResult GetAllHrsByGroup(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var result = Apimanager.GetAllHrsByGroup(id.Value);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        

        [Route("api/settings/GetPeriodForAppraisal/{id:int}")]
        [HttpGet]
        public IHttpActionResult GetPeriodForAppraisal(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var result = Apimanager.GetPeriodForAppraisal(id.Value);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [Route("api/settings/GetUserLocation/{userId}")]
        [HttpGet]
        public IHttpActionResult GetUserLocation(string userId)
        {
            if (userId == null)
            {
                return BadRequest();
            }
            var result = Apimanager.GetUserLocation(userId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [Route("api/settings/GetDeptHeadByUnit/{Id:int}")]
        [HttpGet]
        public IHttpActionResult GetDeptHeadByUnit(int? Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }
            var result = Apimanager.GetDeptHeadByUnit(Id.Value);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [Route("api/settings/GetLineManagers/{Id:int}")]
        [HttpGet]
        public IHttpActionResult GetLineManagers(int? Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }
           List<Employees> result = null;
           var groupId = Apimanager.GetEmployeeByUserId(User.Identity.GetUserId());
            switch (Id.Value.ToString())
            {
                case "1":
                    result = Apimanager.GetDeptHeadByUnit(groupId.GroupId);
                    break;
                case "2":

                   result = Apimanager.GetEmpByBusinessUnit(groupId.GroupId);
                    break;
                case "3":
                    result = Apimanager.GetAllEmployessInGroup(groupId.GroupId);
                    break;
                default:
                    break;
            }
            return Ok(result);
        }
        
        

        // POST: api/Settings
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Settings/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Settings/5
        public void Delete(int id)
        {
        }
    }
}
