using OneTalentResignation.BLL.Interface;
using OneTalentResignation.BLL.Shared;
using OneTalentResignation.DAL.ResignationProcess.Interface;
using OneTalentResignation.DTO.DomainModel;
using OneTalentResignation.DTO.View_Models;
using System.Collections.Generic;

namespace OneTalentResignation.BLL.ResignationBLL
{

    public class ApprovalResignationBLL : IApprovalResignationBLL
    {
        private readonly IApprovalResignationDAL _approvalResignationDAL;
        private readonly ICalculations _calculations;
        private readonly IClaims _claims;

        /// <summary>
        /// Constructor   : ApprovalResignationBLL
        /// Author        : Binal Patel
        /// Creation Date : 17 October 2019
        /// Purpose       : To resolve the dependency
        /// </summary>
        /// <param name="approvalResignationDAL">ApprovalResignationDAL</param>
        /// <param name="calculations">Calculations</param>
        /// <param name="claims">Claims</param>
        public ApprovalResignationBLL(IApprovalResignationDAL approvalResignationDAL,ICalculations calculations,IClaims claims)
        {
            _approvalResignationDAL = approvalResignationDAL;
            _calculations = calculations;
            _claims = claims;

        }

        /// <summary>
        /// Method Name     : GetDomain
        /// Author          : Vrunda Patel
        /// Creation Date   : 16 oct 2019
        /// Purpose         : Acess the Method from DAL Layer to BLL.
        /// </summary>
        public List<string> GetDomain()
        {
            return _approvalResignationDAL.GetDomain();
        }

        /// <summary>
        /// Method Name     : GetTechnology
        /// Author          : Divya Patel
        /// Creation Date   : 16 oct 2019
        /// Purpose         : Acess the Method from DAL Layer to BLL.
        /// </summary>
        public List<string> GetTechnology()
        {
            return _approvalResignationDAL.GetTechnology();
        }


        /// <summary>
        /// Method Name     : GetDesignation
        /// Author          : Binal Patel
        /// Creation Date   : 15 oct 2019
        /// Purpose         : Acess the Method from DAL Layer to BLL.
        /// </summary>
        public List<string> GetDesignation()
        {
            return _approvalResignationDAL.GetDesignation();
        }


        /// <summary>
        /// Method Name     : GetStatus
        /// Author          : Binal Patel
        /// Creation Date   : 15 oct 2019
        /// Purpose         : Acess the Method from DAL Layer to BLL.
        /// </summary>
        public List<string> GetStatus()
        {
            return _approvalResignationDAL.GetStatus();
        }
        /// <summary>
        /// Method Name     : GetResignationEmployeeList
        /// Author          : Yogesh Parmar
        /// Creation Date   : 17 oct 2019
        /// Purpose         : Acess the Method from DAL Layer to BLL.
        /// Revision        : By Nilesh Avsthi add Paging to this Method 18 oct 2019
        /// </summary>
        /// <param name="filter">filter</param> 
        public List<ResignationListViewModel> GetResignationEmployeeList(ResignationFilterViewModel filter)
        {
            filter.SubId = _claims.UserId;
            filter.Role = _claims.Roles[1];
            if(filter.Role.Equals("HR"))
            {
                filter.SubId = "";
            }
            return _approvalResignationDAL.GetResignationEmployeeList(filter);
        }


        /// <summary>
        /// Method Name     : GetResignationDetails
        /// Author          : Divya Patel
        /// Creation Date   : 18 oct 2019
        /// Purpose         : Acess the Method from DAL Layer to BLL.
        /// </summary>
        /// <param name="resignationId">ResignationId</param>
        /// <returns>List of ResignationEmployeeDetails</returns>
        public EmployeeDetailViewModel GetResignationDetails(int resignationId)
        {
            var resignationDetails = _approvalResignationDAL.GetResignationDetails(resignationId, _claims.UserId);

            if (resignationDetails != null)
            {
                resignationDetails.ProposedNoticePeriod = _calculations.CalculateProposedNoticePeriod(resignationDetails.ProposedRelievingDate, resignationDetails.RaisedOnDate);
                resignationDetails.ApprovedNoticePeriod = _calculations.CalculateApprovedNoticePeriod(resignationDetails.ApprovedRelievingDate, resignationDetails.RaisedOnDate);
                resignationDetails.ApprovedRelievingDate =  _calculations.CalculateApprovedRelieveningDate(resignationDetails.ProposedRelievingDate, resignationDetails.OnBoardingNoticePeriod);
            }
            return resignationDetails;
        }

        /// <summary>
        /// Method Name     : Approve
        /// Author          : Vrunda Patel & Nilesh avasthi
        /// Creation Date   : 16 October 2019
        /// Purpose         : Invokes the Post API request for Approve and Reject
        /// </summary>
        /// <param name="resignationModel">ResignationModel</param>
        public void Approve(ResignationViewModel resignationModel)
        {
            List<string> roles = new List<string> { "EMPLOYEE", "HR", "RM" };
            List<string> rolesClaims = _claims.Roles;

            foreach(var items in roles)
            {
                if(rolesClaims.Contains(items))
                    resignationModel.Role = items;
            }
            resignationModel.SubId = _claims.UserId;
            _approvalResignationDAL.Approve(resignationModel);
        }

        /// <summary>
        ///  Method Name    : Put
        /// Author          : Binal Patel
        /// Creation Date   : 17 October 2019
        /// Purpose         : Invokes the Put API request for HR 
        /// </summary>
        /// <param name="resignationModel">ResignationModel</param>
        /// <returns></returns>
        public Resignation Put(ResignationViewModel resignationModel)
        {
            resignationModel.SubId = _claims.UserId;
            resignationModel.Role = _claims.Roles[1];
            return _approvalResignationDAL.Put(resignationModel);
        }
       
    }
}
