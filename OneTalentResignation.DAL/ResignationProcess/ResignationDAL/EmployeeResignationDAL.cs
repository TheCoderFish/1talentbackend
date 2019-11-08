using System.Collections.Generic;
using OneTalentResignation.DAL.Context;
using System.Linq;
using OneTalentResignation.DTO.View_Models;
using OneTalentResignation.DAL.ResignationProcess.Interface;
using OneTalentResignation.DTO.DomainModel;
using System;
using System.Net.Http;

namespace OneTalentResignation.DAL.ResignationProcess.ResignationDAL
{
    public class EmployeeResignationDAL : IEmployeeResignationDAL
    {
        private readonly EmployeeResignationDatabaseCalls _employeeResignation;
        private readonly ResignationContext _resignationContext;


        /// <summary>
        /// Constructor   : EmployeeResignationDAL
        /// Author        : Khushboo Patel
        /// Creation Date : 17 October 2019
        /// Purpose       : To resolve the dependency
        /// </summary>
        /// <param name="employeeResignation"> EmployeeResignation</param>
        /// <param name="resignationContext"> ResignationContext</param>
        public EmployeeResignationDAL(EmployeeResignationDatabaseCalls employeeResignation, ResignationContext resignationContext)
        {
            _employeeResignation = employeeResignation;
            _resignationContext = resignationContext;
        }

        /// <summary>
        /// Method Name   : GetEmployeeList
        /// Author        : Yaseen Agwan
        /// Creation Date : 18 October 2019
        /// Purpose       : To Get the list of all Emplyees for the cc.
        /// </summary>
        /// <returns></returns>
        public List<ConcenEmployeeViewModel> GetEmployeeList()
        {
            return _employeeResignation.GetCcDetails();
        }

        /// <summary>
        /// Method Name   : GetResignationList
        /// Author        : Yaseen Agwan
        /// Creation Date : 18 October 2019
        /// Purpose       : Get The Details of Previously Applied Resignaion List of a Particular employee.
        /// </summary>
        /// <param name="employeeId">EmployeeId</param>
        /// <returns></returns>
        public List<EmployeeResignationViewModel> GetResignationList(int employeeId)
        {
            return _employeeResignation.GetDetails(employeeId);
        }

        /// <summary>
        /// Method Name        : ResignationRevoke
        /// Author             : Keval Kiniyara
        /// Creation Date      : 18 October 2019
        /// Purpose            : To revoke the resignation.
        /// Revision           :
        /// </summary>
        /// <param name="revoke"> Revoke</param>
        public void ResignationRevoke(Resignation revoke)
        {
            if (!IsApplicableForRevoking(revoke.ResignationId))
            {
                throw new HttpRequestException();
            }
            _employeeResignation.ResignationRevoke(revoke);
        }

        /// <summary>
        /// Method Name   : Apply
        /// Author        : Khushboo Patel
        /// Creation Date : 17 October 2019
        /// Purpose       : Apply For Resignation.
        /// </summary>
        /// <param name="resignationDetail"> ResignationDetail </param>
        /// <param name="concernEmployee"> ConcernEmployee </param>
        public void Apply(Resignation resignationDetail, List<ConcernEmployee> concernEmployee)
        {
            if (!IsApplicableForApplyingResignation(resignationDetail.EmployeeId))
            {
                throw new HttpRequestException();
            }
            _employeeResignation.Apply(resignationDetail, concernEmployee);
        }

        /// <summary>
        /// Method Name     : GetEmployeeId
        /// Author          : Yaseen Agwan
        /// Creation Date   : 31 October 2019
        /// Purpose         : To Get the EmployeeId having employeeCode(SubId)
        /// </summary>
        /// <param name="subjectId">value fetched from JWT Token</param>
        /// <returns>EmployeeId</returns>
        public int GetEmployeeId(string subjectId)
        {
            return _resignationContext.Employees.Where(x => x.EmployeeRegisterId == subjectId).Select(x => x.EmployeeId).FirstOrDefault();
        }


        /// <summary>
        /// Method Name     : GetEmployeeResignationId
        /// Author          : Yaseen Agwan
        /// Creation Date   : 31 October 2019
        /// Purpose         : To Get the EmployeeResignationId having employeeCode(SubId)
        /// </summary>
        /// <param name="subjectId">value fetched from JWT Token</param>
        /// <returns>EmployeeResignationId</returns>
        public int GetEmployeeResignationId(string subjectId)
        {
            return (from employee in _resignationContext.Employees
                    join employeeResignation in _resignationContext.Resignations
                    on employee.EmployeeId equals employeeResignation.EmployeeId
                    where employee.EmployeeRegisterId == subjectId
                    && employee.DeletedDate == null
                    select employeeResignation.ResignationId).LastOrDefault();
        }


        /// <summary>
        /// Method Name   : IsEmployeeRegistered
        /// Author        : Khushboo Patel
        /// Creation Date : 17 October 2019
        /// Purpose       : To check employee is registered or not 
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public bool IsEmployeeRegistered(int employeeId)
        {
            return _resignationContext.Employees.Any(a => a.EmployeeId == employeeId);
        }


        /// <summary>
        /// Method Name   : IsApplicableForApplyingResignation
        /// Author        : Khushboo Patel
        /// Creation Date : 17 October 2019
        /// Purpose       : Application is previously revoked or rejected by HR/RM
        /// </summary>
        /// <param name="employeeId"> EmployeeId </param>
        private bool IsApplicableForApplyingResignation(int employeeId)
        {
            bool IsApplicable = true;

            string[] resignationStatus = { "Initiated", "Accepted by HR", "Accepted by RM" }; 

            var lastResignationApplied = _resignationContext.Resignations.Where(x => x.EmployeeId == employeeId).LastOrDefault();

            if(lastResignationApplied == null)
            {
                IsApplicable = true;
            }
            else if(resignationStatus.Contains(lastResignationApplied.ResignationStatus.Trim()))
            {
                IsApplicable = false;
            }
            
            return IsApplicable;
        }

        /// <summary>
        /// Method Name        : IsApplicableForRevoking
        /// Author             : Keval Kiniyara
        /// Creation Date      : 18 October 2019
        /// Purpose            : To revoke the resignation.
        /// </summary>
        /// <param name="resignationId"> ResignationId</param>
        /// <returns></returns>
        private bool IsApplicableForRevoking(int resignationId)
        {
            bool IsApplicable = true;
            var hrId = _resignationContext.Resignations.Where(x => x.ResignationId == resignationId).Select(x => x.HRId).SingleOrDefault();
            if (hrId != null)
            {
                IsApplicable = false;
            }

            return IsApplicable;
        }


    }
}
