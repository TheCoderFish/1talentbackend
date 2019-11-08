using OneTalentResignation.DAL.Context;
using OneTalentResignation.DAL.ResignationProcess.Interface;
using OneTalentResignation.DTO.DomainModel;
using OneTalentResignation.DTO.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OneTalentResignation.DAL.ResignationProcess.ResignationDAL
{
    public class EmployeeResignationDatabaseCalls
    {
        private readonly ResignationContext _resignationContext;
        private readonly IConcernEmployees _concernEmployees;

        /// <summary>
        /// Constructor   : EmployeeResignationDatabaseCalls
        /// Author        : Yaseen Agwan
        /// Creation Date : 17 October 2019
        /// Purpose       : To resolve the dependency
        /// </summary>
        /// <param name="resignationContext"> ResignationContext</param>

        public EmployeeResignationDatabaseCalls(ResignationContext resignationContext, IConcernEmployees concernEmployees)
        {
            _resignationContext = resignationContext;
            _concernEmployees = concernEmployees;
        }

        /// <summary>
        /// Method Name   : GetCcDetails
        /// Author        : Yaseen Agwan
        /// Creation Date : 17 October 2019
        /// Purpose       : To get the list of employees.
        /// </summary>
        /// <returns></returns>
        public List<ConcenEmployeeViewModel> GetCcDetails()
        {
            List<ConcenEmployeeViewModel> details = (from employee in _resignationContext.Employees
                                                     select new ConcenEmployeeViewModel
                                                     {
                                                         EmployeeId = employee.EmployeeId,
                                                         EmployeeName = employee.FirstName + " " + employee.LastName
                                                     }).ToList();
            return details;
        }

        /// <summary>
        /// Method Name   : GetDetails
        /// Author        : Yaseen Agwan
        /// Creation Date : 17 October 2019
        /// Purpose       : To get the list of employees.
        /// </summary>
        /// <param name="employeeId"> EmployeeId</param>
        /// <returns>  Return Resignation </returns>
        public List<EmployeeResignationViewModel> GetDetails(int employeeId)
        {
            var resignationDetails = (from details in _resignationContext.Resignations
                                     join employee in _resignationContext.Employees
                                     on details.EmployeeId equals employee.EmployeeId
                                     where details.EmployeeId == employeeId
                                     select new EmployeeResignationViewModel
                                     {
                                         ResignationId = details.ResignationId,
                                         EmployeeId = details.EmployeeId,
                                         HRId = details.HRId,
                                         RMId = details.ManagerId,
                                         ResignationProposedDate = details.ResignationProposedDate,
                                         ResignationApprovedDate = details.ResignationApprovedDate,
                                         NoticePeroid = employee.NoticePeriod,
                                         RaisedOnDate = details.CreatedDate,
                                         ResignationReason = details.ResignationReason,
                                         Status = details.ResignationStatus,
                                         RevokeReason = details.RevokeRemark,
                                         ManageRemarks = details.ManagerRemark,
                                         HRRemarks = details.HRRemark,
                                     }).ToList();

            foreach(var items in resignationDetails)
            {
                items.ConcernEmployees = _concernEmployees.GetAllTheConcernEmployees(items.ResignationId);
            }
            return resignationDetails.ToList();
        }

        /// <summary>
        /// Method Name         : ResignationRevoke
        /// Author              : Keval Kiniyara
        /// Creation Date       : 18 October 2019
        /// Purpose             : To revoke the resignation 
        /// Revision            :
        /// </summary>
        /// <param name="revoke"> Revoke </param>
        public void ResignationRevoke(Resignation revoke)
        {
            _resignationContext.Resignations.Update(revoke);
            _resignationContext.Resignations.Attach(revoke);
            _resignationContext.Entry(revoke).Property(x => x.RevokeRemark).IsModified = true;
            _resignationContext.Entry(revoke).Property(x => x.ResignationStatus).IsModified = true;
            _resignationContext.Entry(revoke).Property(x => x.LastModifiedDate).IsModified = true;
            _resignationContext.Entry(revoke).Property(x => x.DeletedDate).IsModified = true;
            _resignationContext.SaveChanges();
        }

        /// <summary>
        /// Method Name   : Apply
        /// Author        : Khushboo Patel
        /// Creation Date : 17 October 2019
        /// Purpose       : Save Data into Resignation Table and ConcernEmployee Table.
        /// </summary>
        /// <param name="resignationDetail"> ResignationDetail </param>
        /// <param name="concernEmployee"> ConcernEmployee </param>
        public void Apply(Resignation resignationDetail, List<ConcernEmployee> concernEmployee)
        {
            var transaction = _resignationContext.Database;
            try
            {
                transaction.BeginTransaction();

                _resignationContext.Resignations.Add(resignationDetail);

                _resignationContext.SaveChanges();

                var resignationId = (from resign in _resignationContext.Resignations
                                     where resign.EmployeeId == resignationDetail.EmployeeId
                                     select resign.ResignationId).LastOrDefault();

                foreach (var items in concernEmployee)
                {
                    items.ResignationId = resignationId;
                    items.CreatedDate = DateTime.Now;
                    _resignationContext.Add(items);
                }

                _resignationContext.SaveChanges();

                transaction.CommitTransaction();
            }
            catch (Exception)
            {
                transaction.RollbackTransaction();
            }
        }
    }
}

