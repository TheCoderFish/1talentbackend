using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneTalentResignation.BLL.Interface;
using OneTalentResignation.BLL.ResignationBLL;
using OneTalentResignation.DTO.View_Models;
using System;


namespace OneTalentResignation.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ResignationsController : Controller
    {
        private readonly IApprovalResignationBLL _approvalResignationBLL;
        private readonly EmployeeResignationBLL _employeeResignation;

        /// <summary>
        /// Constructor   : ResignationsController
        /// Author        : Binal Patel
        /// Creation Date : 17 October 2019
        /// Purpose       : To resolve the dependency
        /// </summary>
        /// <param name="approvalResignationBLL"> ApprovalResignationBLL</param>
        /// <param name="employeeResignation"> EmployeeResignation </param>
        public ResignationsController(IApprovalResignationBLL approvalResignationBLL, EmployeeResignationBLL employeeResignation)
        {
            _approvalResignationBLL = approvalResignationBLL;
            _employeeResignation = employeeResignation;

        }

        /// <summary>
        /// Method Name   : GetEmployeeList
        /// Author        : Chanda Chaurasiya
        /// Creation Date : 17 October 2019
        /// Purpose       : Invokes the get API request to get Cc Person list.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ccpersons")]
        [Authorize("resign_request")]
        public IActionResult GetEmployeeList()
        {
            return Ok(_employeeResignation.GetEmployeeList());
        }

        /// <summary>
        /// Method Name   : GetResignationList
        /// Author        : Chanda Chaurasiya
        /// Creation Date : 17 October 2019
        /// Purpose       : To get resignation details of particular employee.
        /// </summary>
        /// <returns></returns>
        [Route("details")]
        [HttpGet]
        [Authorize("resign_history")]
        public IActionResult GetDetails()
        {
            return Ok(_employeeResignation.GetResignationList());
        }

        /// <summary>
        /// Method Name   : Apply
        /// Author        : Chanda Chaurasiya
        /// Creation Date : 17 October 2019
        /// Purpose       : Apply For Resignation
        /// </summary>
        /// <param name="resignation"> Resignation </param>
        [Route("apply")]
        [HttpPost]
        [Authorize("resign_request")]
        public IActionResult Apply([FromBody] ResignationDetailViewModel resignation)
        {
            _employeeResignation.Apply(resignation);
            return Ok();
        }

        /// <summary>
        /// Method Name   : Revoke
        /// Author        : Chanda Chaurasiya
        /// Creation Date : 17 October 2019
        /// Purpose       : To Revoke the Particular  Employee resignation
        /// </summary>
        /// <param name="revokeReason">RevokeReason</param>
        /// <returns></returns>
        [Route("revoke")]
        [HttpPut]
        [Authorize("resign_revoke")]
        public IActionResult Revoke([FromBody]ResignationRevokeViewModel revokeReason)
        {
            _employeeResignation.EmployeeResignationRevoke(revokeReason);
            return Ok();
        }

        /// <summary>
        /// Method Name     : GetDomain
        /// Author          : Vrunda Patel
        /// Creation Date   : 16 October 2019
        /// Purpose         : Invokes the get API request for Domain Name.
        /// </summary>
        /// <returns>List of Domain Name</returns>
        [Route("domain")]
        [HttpGet]
        [Authorize("resign_list")]
        public IActionResult GetDomain()
        {
            return Ok(_approvalResignationBLL.GetDomain());
        }

        /// <summary>
        /// Method Name     : GetTechnology
        /// Author          : Divya Patel
        /// Creation Date   : 15 October 2019
        /// Purpose         : Invokes the get API request for Technology Name.
        /// </summary>
        /// <returns>List of Technology Name</returns>
        [Route("technology")]
        [HttpGet]
        [Authorize("resign_list")]
        public IActionResult GetTechnology()
        {
            return Ok(_approvalResignationBLL.GetTechnology());
        }

        /// <summary>
        /// Method Name     : GetDesignation
        /// Author          : Binal Patel
        /// Creation Date   : 15 October 2019
        /// Purpose         : Invokes the get API request for Designation Name.
        /// </summary>
        /// <returns>List of Designation Name</returns>
        [Route("designation")]
        [HttpGet]
        [Authorize("resign_list")]
        public IActionResult GetDesignation()
        {
            return Ok(_approvalResignationBLL.GetDesignation());
        }

        /// <summary>
        /// Method Name     : GetStatus
        /// Author          : Binal Patel
        /// Creation Date   : 15 October 2019
        /// Purpose         : Invokes the get API request for Status Name.
        /// </summary>
        /// <returns>List of Status Name</returns>
        [Route("status")]
        [HttpGet]
        [Authorize("resign_list")]
        public IActionResult GetStatus()
        {
            return Ok(_approvalResignationBLL.GetStatus());
        }

        /// <summary>
        /// Method Name     : GetResignationEmployeeList
        /// Author          : Yogesh Parmar
        /// Creation Date   : 16 October 2019
        /// Purpose         : Invokes the get API request for ResignationEmployeeList 
        /// </summary>
        /// <param name="filter">Filter</param>
        [HttpPost]
        [Authorize("resign_list")]
        public IActionResult GetResignationEmployeeList([FromBody] ResignationFilterViewModel filter)
        {
            if (ModelState.IsValid)
            {
                return Ok(_approvalResignationBLL.GetResignationEmployeeList(filter));
            }
            return BadRequest();

        }

        /// <summary>
        /// Method Name     : GetResignationDetails
        /// Author          : Divya Patel
        /// Creation Date   : 16 October 2019
        /// Purpose         : Invokes the get API request for EmployeeDetails View for Approve and Reject
        /// </summary>
        /// <param name="resignationId">ResignationId</param>
        /// <returns>List of ResignationEmployeeList</returns>
        [HttpGet("{resignationId}")]
        [Authorize("resign_list")]
        public IActionResult GetResignationDetails([FromRoute] uint resignationId)
        {
            if (resignationId > 0)
            {
                return Ok(_approvalResignationBLL.GetResignationDetails(Convert.ToInt32(resignationId)));
            }
            return new BadRequestObjectResult("Resignation Id Should Be Greater than 0");
        }

        /// <summary>
        /// Method Name     : Approve
        /// Author          : Vrunda Patel 
        /// Creation Date   : 16 October 2019
        /// Purpose         : Invokes the Post API request for Approve and Reject
        /// </summary>
        /// <param name="resignationModel">ResignationModel</param>
        /// <returns>List of ResignationEmployeeList</returns>
        [Route("approve")]
        [HttpPost]
        [Authorize("approve_resign")]
        public IActionResult Approve([FromBody]ResignationViewModel resignationModel)
        {
            if (ModelState.IsValid)
            {
                _approvalResignationBLL.Approve(resignationModel);
                return Ok();
            }
            return BadRequest();
        }

        /// <summary>
        /// Method Name     : Put
        /// Author          : Binal Patel
        /// Creation Date   : 17 October 2019
        /// Purpose         : Invokes the Put API request for HR 
        /// </summary>
        /// <param name="resignationModel">Model Of Resignation</param>
        [Route("edit")]
        [HttpPut]
        [Authorize("approve_date")]
        public IActionResult Put([FromBody]ResignationViewModel resignationModel)
        {
            var source = _approvalResignationBLL.Put(resignationModel);
            if (source == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(source);
            }
        }
    }
}
