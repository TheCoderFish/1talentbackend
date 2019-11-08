using OneTalentResignation.BLL.Interface;
using OneTalentResignation.DAL.ResignationProcess.Interface;
using OneTalentResignation.DTO.DomainModel;
using OneTalentResignation.DTO.View_Models;
using System;
using System.Collections.Generic;
using OneTalentResignation.BLL.Shared;


namespace OneTalentResignation.BLL.ResignationBLL
{
    public class EmployeeResignationBLL
    {
        private readonly IEmployeeResignationDAL _employeeResignation;
        private readonly ICalculations _calculations;
        private readonly IClaims _claims;

        /// <summary>
        /// Constructor   : EmployeeResignationBLL
        /// Author        : Khushboo Patel
        /// Creation Date : 17 October 2019
        /// Purpose       : To resolve the dependency
        /// <param name="employeeResignation"> EmployeeResignation</param>
        /// <param name="calculations"> Calculations</param>
        public EmployeeResignationBLL(IEmployeeResignationDAL employeeResignation, ICalculations calculations, IClaims claims)
        {
            _employeeResignation = employeeResignation;
            _calculations = calculations;
            _claims = claims;
        }

        /// <summary>
        /// Method Name      : EmployeeResignationRevoke
        /// Author           : Keval Kiniyara
        /// Creation Date    : 18 October 2019
        /// Purpose          : To Revoke The Resignation.
        /// Revision         :
        /// </summary>
        /// <param name="revoke"> Revoke</param>
        public void EmployeeResignationRevoke(ResignationRevokeViewModel revoke)
        {
            Resignation resignation = new Resignation
            {
                ResignationId = _employeeResignation.GetEmployeeResignationId(_claims.UserId),
                RevokeRemark = revoke.RevokeRemarks,
                ResignationStatus = "Revoked",
                LastModifiedDate = DateTime.Now,
                DeletedDate = DateTime.Now
            };
            _employeeResignation.ResignationRevoke(resignation);
        }

        /// <summary>
        /// Method Name   : GetResignationList
        /// Author        : Yaseen Agwan
        /// Creation Date : 17 October 2019
        /// Purpose       : To get resignation details of particular employee.
        /// </summary>
        /// <returns></returns>       
        public List<EmployeeResignationViewModel> GetResignationList()
        {
            var resignationDetails = _employeeResignation.GetResignationList(_employeeResignation.GetEmployeeId(_claims.UserId));

            foreach (var items in resignationDetails)
            {
                items.ProposedNoticePeriod = _calculations.CalculateProposedNoticePeriod(items.ResignationProposedDate, items.RaisedOnDate);
                items.ApprovedNoticePeriod = _calculations.CalculateApprovedNoticePeriod(items.ResignationApprovedDate, items.RaisedOnDate);
                if (_calculations.IsRevoked(items))
                {
                    items.IsApprovedByHr = false;
                    items.IsApprovedByRm = false;
                }
                else
                {
                    items.IsApprovedByHr = _calculations.IsApprovedByHr(items);
                    items.IsApprovedByRm = _calculations.IsApprovedByRm(items);
                }
            }

            return resignationDetails;
        }

        /// <summary>
        /// Method Name   : Apply
        /// Author        : Khushboo Patel
        /// Creation Date : 17 October 2019
        /// Purpose       : Apply For Resignation.
        /// </summary>
        /// <param name="resignationDetail"> ResignationDetail </param>
        public void Apply(ResignationDetailViewModel resignationDetail)
        {
            List<ConcernEmployee> listOfConcernEmployees = new List<ConcernEmployee>();

            var resignation = new Resignation()
            {
                CreatedDate = DateTime.Now,
                RaisedOnDate = DateTime.Now,
                EmployeeId = _employeeResignation.GetEmployeeId(_claims.UserId),
                ResignationProposedDate = resignationDetail.RelieveDate,
                ResignationReason = resignationDetail.Reason,
                ResignationStatus = "Initiated",
            };

            foreach (var items in resignationDetail.CcPersons)
            {
                if (_employeeResignation.IsEmployeeRegistered(items))
                {
                    var concernEmployee = new ConcernEmployee
                    {
                        EmployeeId = items
                    };
                    listOfConcernEmployees.Add(concernEmployee);
                }
            }

            _employeeResignation.Apply(resignation, listOfConcernEmployees);
        }

        /// <summary>
        /// Method Name   : GetEmployeeList
        /// Author        : Yaseen Agwan
        /// Creation Date : 17 October 2019
        /// Purpose       : TO Get all List of Employees.
        /// </summary>
        /// <returns></returns>
        public List<ConcenEmployeeViewModel> GetEmployeeList()
        {
            return _employeeResignation.GetEmployeeList();
        }
        
    }
}