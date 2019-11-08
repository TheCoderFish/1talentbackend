using OneTalentResignation.DAL.Context;
using OneTalentResignation.DAL.ResignationProcess.Interface;
using OneTalentResignation.DTO.DomainModel;
using OneTalentResignation.DTO.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace OneTalentResignation.DAL.ResignationProcess.ResignationDAL
{
    public class DatabaseQuery : IApprovalResignationDAL
    {
        private readonly ResignationContext _context;
        private readonly IConcernEmployees _concernEmployees;
        private const string BYHR = " by HR";
        private const string BYRM = " by RM";


        /// <summary>
        /// Constructor   : DatabaseQuery
        /// Author        : Binal Patel
        /// Creation Date : 17 October 2019
        /// Purpose       : To resolve the dependency
        /// </summary>
        /// <param name="context">context</param>

        public DatabaseQuery(ResignationContext context, IConcernEmployees concernEmployees)
        {
            _context = context;
            _concernEmployees = concernEmployees;
        }

        /// <summary>
        ///  Method Name     : GetDomain
        /// Author          : Vrunda Patel
        /// Creation Date   : 16 oct 2019
        /// Purpose         : Featching the list of  Domain Name from Database.
        /// </summary>
        /// <returns></returns>

        public List<string> GetDomain()
        {
            return (from domain in _context.Domains
                    select domain.DomainName).ToList();
        }

        /// <summary>
        ///  Method Name     : GetTechnology
        /// Author          : Divya Patel
        /// Creation Date   : 16 oct 2019
        /// Purpose         : Featching the list of  Technology Name from Database.
        /// </summary>
        /// <returns></returns>
        public List<string> GetTechnology()
        {
            return (from technology in _context.Technologies
                    select technology.TechnologyName).ToList();
        }

        /// <summary>
        /// Method Name     : GetDesignation
        /// Author          : Binal Patel
        /// Creation Date   : 16 oct 2019
        /// Purpose         : Featching the list of  Designation Name from Database.
        /// </summary>
        /// <returns></returns>
        public List<string> GetDesignation()
        {
            return (from designation in _context.Designations
                    select designation.DesignationName).ToList();
        }

        /// <summary>
        ///  Method Name     : GetStatus
        /// Author          : Binal Patel
        /// Creation Date   : 16 oct 2019
        /// Purpose         : Featching the list of Status Name from Database.
        /// </summary>
        /// <returns></returns>
        public List<string> GetStatus()
        {
            return (from resignationstatus in _context.Resignations
                    select resignationstatus.ResignationStatus).Distinct().ToList();

        }

        /// <summary>
        /// Method Name     : GetResignationEmployeeList
        /// Author          : Yogesh Parmar
        /// Creation Date   : 17 oct 2019
        /// Purpose         : Featching the list of ResignationEmployeeList Name from Database
        /// Revision        : By Nilesh Avsthi add Paging to this Method 18 oct 2019
        /// </summary>
        /// <param name="filter">filter</param>
        /// <returns></returns>
        public List<ResignationListViewModel> GetResignationEmployeeList(ResignationFilterViewModel filter)
        {
            var source = (from employee in _context.Employees
                          join domain in _context.Domains on employee.DomainId equals domain.DomainId
                          join technology in _context.Technologies on employee.TechnologyID equals technology.TechnologyId
                          join designation in _context.Designations on employee.DesignationId equals designation.DesignationId
                          join resignations in _context.Resignations on employee.EmployeeId equals resignations.EmployeeId
                          select new ResignationListViewModel
                          {
                              FirstName = employee.FirstName,
                              LastName = employee.LastName,
                              EmployeeCode = employee.EmployeeCode,
                              DomainName = domain.DomainName,
                              TechnologyName = technology.TechnologyName,
                              DesignationName = designation.DesignationName,
                              RaisedOnDate = resignations.RaisedOnDate,
                              ManagerId = employee.ManagerId,
                              ResignationId = resignations.ResignationId,
                              ResignationManagerId = resignations.ManagerId,
                              ResignationApprovedDate = resignations.ResignationApprovedDate,
                              ResignationStatus = resignations.ResignationStatus
                          }).ToList();

            return ApplyFilter(filter, source);
        }
        public List<ResignationListViewModel> ApplyFilter(ResignationFilterViewModel filter, List<ResignationListViewModel> source)
        {
            if (filter.SubId != "")
            {
                source = source.Where(b => b.ManagerId == GetEmployeeId(filter.SubId)).ToList();
            }
            else
            {
                // source = source.Where(b => b.ResignationStatus.Equals("Accepted by RM") || b.ResignationStatus.Equals("accept by rm")).ToList();

            }

            if (filter?.FromDate != null)
                if (filter.ToDate != null)
                {
                    source = source.Where(x => x.RaisedOnDate >= filter.FromDate && x.RaisedOnDate <= filter.ToDate).ToList();
                }
                else
                {
                    source = source.Where(x => x.RaisedOnDate >= filter.FromDate && x.RaisedOnDate <= DateTime.Now).ToList();
                }

            if (filter.DomainName != null)
            {
                source = source.Where(x => string.Equals(x.DomainName, filter.DomainName, StringComparison.CurrentCultureIgnoreCase)).ToList();

            }
            if (filter.TechnologyName != null)
            {
                source = source.Where(x => string.Equals(x.TechnologyName, filter.TechnologyName, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            if (filter.DesignationName != null)
            {
                source = source.Where(x => string.Equals(x.DesignationName, filter.DesignationName, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            if (filter.ResignationStatus != null)
            {
                source = source.Where(x => string.Equals(x.ResignationStatus, filter.ResignationStatus, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            if (filter.EmployeeName != null)
            {
                source = source.Where(x => x.FirstName.StartsWith(filter.EmployeeName, StringComparison.CurrentCultureIgnoreCase)).ToList();

            }
            if (filter.EmployeeCode != 0 && filter.EmployeeCode != null)
            {
                source = source.Where(x => x.EmployeeCode == filter.EmployeeCode).ToList();
            }

            return source.Skip((filter.PageNumber - 1) * filter.Limit).Take(filter.Limit).ToList();
        }




        /// <summary>
        /// Method Name     : GetEmployeeDetails
        /// Author          : Divya Patel
        /// Creation Date   : 16 October 2019
        /// Purpose         : Invokes the get API request for EmployeeDetails View for Approve and Reject
        /// </summary>
        /// <param name="resignationId">resignationId</param>
        /// <param name="subId">subId</param>
        /// <returns></returns>
        public EmployeeDetailViewModel GetResignationDetails(int resignationId, string subId)
        {
            var resignationDetails = (from employee in _context.Employees
                                      join domain in _context.Domains on employee.DomainId equals domain.DomainId
                                      join technology in _context.Technologies on employee.TechnologyID equals technology.TechnologyId
                                      join designation in _context.Designations on employee.DesignationId equals designation.DesignationId
                                      join resignations in _context.Resignations on employee.EmployeeId equals resignations.EmployeeId
                                      where resignations.ResignationId == resignationId
                                      select new EmployeeDetailViewModel
                                      {
                                          FirstName = employee.FirstName,
                                          LastName = employee.LastName,
                                          EmployeeCode = employee.EmployeeCode,
                                          EmployeeReason = resignations.ResignationReason,
                                          EmployeeId = employee.EmployeeId,
                                          Domain = domain.DomainName,
                                          Technology = technology.TechnologyName,
                                          Designation = designation.DesignationName,
                                          RaisedOnDate = resignations.RaisedOnDate.Value,
                                          ProposedRelievingDate = resignations.ResignationProposedDate.Value,
                                          ReportingManagerId = employee.ManagerId,
                                          ReportingPersonRemark = resignations.ManagerRemark,
                                          HrRemark = resignations.HRRemark,
                                          ApprovedNoticePeriod = employee.NoticePeriod,
                                          ExitInterviewDate = resignations.ExitInterviewDate.Value,
                                          IsRehiredByHR = resignations.IsRehiredByHR,
                                          IsRehiredByRM = resignations.IsRehiredByManager,
                                          ApprovedRelievingDate = resignations.ResignationApprovedDate.Value,
                                          ResignationId = resignations.ResignationId,
                                          ManagerName = GetEmployeeName(subId),
                                          OnBoardingNoticePeriod = employee.NoticePeriod,
                                          ResignationProposedDate = resignations.ResignationProposedDate,
                                          ResignationApprovedDate = resignations.ResignationApprovedDate,
                                          Status = resignations.ResignationStatus,
                                      }).FirstOrDefault();

            resignationDetails.ConcernEmployees = _concernEmployees.GetAllTheConcernEmployees(resignationDetails.ResignationId);

            return resignationDetails;
        }


        /// <summary>
        /// Method Name     : GetEmployeeName
        /// Author          : Binal Patel 
        /// Creation Date   : 5 November 2019
        /// Purpose         : Get Employee name
        /// </summary>
        /// <param name="subId">subId</param>
        public string GetEmployeeName(string subId)
        {
            var data = _context.Employees.Where(x => x.EmployeeRegisterId.Equals(subId))
                                        .Select(x => x.FirstName).FirstOrDefault();
            return data;
        }


        /// <summary>
        /// Method Name     : AcceptByHR
        /// Author          : Binal Patel 
        /// Creation Date   : 8 November 2019
        /// Purpose         : Accept or Reject Action for HR
        /// </summary>
        /// <param name="resignationModel">resignationModel</param>
        /// <param name="resignation">resignation</param>
        public void AcceptByHR(Resignation resignation, ResignationViewModel resignationModel)
        {
            if (resignation.ManagerId > 0 && resignation.ResignationStatus != string.Empty)
            {
                if (!resignationModel.Status)
                {
                    resignation.DeletedDate = System.DateTime.Now;
                    CommanAcceptByHR(resignation, resignationModel);
                }
                else
                {
                    resignation.ResignationApprovedDate = resignationModel.ResignationApprovalDate;
                    resignation.ExitInterviewDate = resignationModel.ExitInterviewDate;
                    resignation.LastModifiedDate = System.DateTime.Now;
                    CommanAcceptByHR(resignation, resignationModel);
                }
            }
        }

        /// <summary>
        /// Method Name     : AcceptByRM
        /// Author          : Binal Patel 
        /// Creation Date   : 8 November 2019
        /// Purpose         : Accept or Reject Action for RM
        /// </summary>
        /// <param name="resignationModel">resignationModel</param>
        /// <param name="resignation">resignation</param>
        public void AcceptByRM(Resignation resignation, ResignationViewModel resignationModel)
        {
            if (resignation.ResignationStatus == "Initiated" && !resignation.ExitInterviewDate.HasValue)
            {
                if (!resignationModel.Status)
                {
                    resignation.DeletedDate = System.DateTime.Now;
                    CommanDataForRM(resignation, resignationModel);
                }
                else
                {
                    resignation.LastModifiedDate = System.DateTime.Now;
                    CommanDataForRM(resignation, resignationModel);
                }
            }
        }
        /// <summary>
        /// Method Name     : CommanDataForRM
        /// Author          : Binal Patel 
        /// Creation Date   : 7 November 2019
        /// Purpose         : Command data used for RM accept and reject action
        /// </summary>
        /// <param name="resignationModel">resignationModel</param>
        /// <param name="resignation">resignation</param>
        public void CommanDataForRM(Resignation resignation, ResignationViewModel resignationModel)
        {
            int employeeId = GetEmployeeId(resignationModel.SubId);
            resignation.ManagerId = employeeId;
            resignation.ResignationStatus = resignationModel.ResignationStatus.Trim() + BYRM;
            resignation.ManagerRemark = resignationModel.Remark;
            resignation.IsRehiredByManager = resignationModel.IsRehired;
        }

        /// <summary>
        /// Method Name     : Approve
        /// Author          : Binal Patel 
        /// Creation Date   : 8 November 2019
        /// Purpose         : Approve Method
        /// </summary>
        /// <param name="resignationModel">resignationModel</param>
        public void Approve(ResignationViewModel resignationModel)
        {
            var resignation = _context.Resignations.FirstOrDefault(x => x.ResignationId == resignationModel.ResignationId);
            if (resignation != null)
            {
                AcceptorReject(resignation, resignationModel);
            }

        }

        /// <summary>
        /// Method Name     : ApproveorReject
        /// Author          : Binal Patel 
        /// Creation Date   : 8 November 2019
        /// Purpose         : Accept or Reject Action Performed
        /// </summary>
        /// <param name="resignationModel">resignationModel</param>
        /// <param name="resignation">resignation</param>
        public void AcceptorReject(Resignation resignation, ResignationViewModel resignationModel)
        {

            if (resignationModel.Role.Equals("HR"))
            {
                AcceptByHR(resignation, resignationModel);
                _context.Update(resignation);
                _context.SaveChanges();
            }
            if (resignationModel.Role.Equals("RM"))
            {
                AcceptByRM(resignation, resignationModel);
                _context.Update(resignation);
                _context.SaveChanges();
            }

        }


        /// <summary>
        /// Method Name     : CommanDataForHR
        /// Author          : Binal Patel 
        /// Creation Date   : 7 November 2019
        /// Purpose         : Command data used for HR accept and reject action
        /// </summary>
        /// <param name="resignationModel">resignationModel</param>
        /// <param name="resignation">resignation</param>
        public void CommanAcceptByHR(Resignation resignation, ResignationViewModel resignationModel)
        {
            resignation.HRId = GetEmployeeId(resignationModel.SubId);
            resignation.ResignationStatus = resignationModel.ResignationStatus.Trim() + BYHR;
            resignation.HRRemark = resignationModel.Remark;
            resignation.IsRehiredByHR = resignationModel.IsRehired;
        }



        /// <summary>
        /// Method Name     : Get
        /// Author          : Nilesh Avasthi
        /// Creation Date   : 16 October 2019
        /// Purpose         :Get Resignation Id
        /// </summary>
        /// <param name="regId"></param>
        /// <returns></returns>
        public Resignation Get(int regId)
        {
            return _context.Resignations.FirstOrDefault(x => x.ResignationId == regId);
        }

        /// <summary>
        /// Method Name     : EmployeeId
        /// Author          : Nilesh Avasthi
        /// Creation Date   : 16 October 2019
        /// Purpose         :
        /// </summary>
        /// <param name="subId">subId</param>
        /// <returns></returns>
        public int GetEmployeeId(string subId)
        {
            var data = _context.Employees.Where(x => x.EmployeeRegisterId.Equals(subId)).Select(x => x.EmployeeId).FirstOrDefault();
            return data;
        }



        /// <summary>
        /// Method Name     : Put
        /// Author          : Binal Patel
        /// Creation Date   : 17 October 2019
        /// Purpose         : Invokes the Put API request for HR 
        /// </summary>
        /// <param name="resignationModel"></param>
        /// <returns></returns>
        public Resignation Put(ResignationViewModel resignationModel)
        {
            var updateRecord = _context.Resignations.FirstOrDefault(x => x.ResignationId == resignationModel.ResignationId);
            if (updateRecord == null)
            { }
            else
            {
                if (resignationModel.Role.Equals("HR"))
                {
                    if (!resignationModel.Status)
                    {
                        resignationModel.DeletedDate = System.DateTime.Now;
                    }
                    else
                    {
                        updateRecord.ResignationApprovedDate = resignationModel.ResignationApprovalDate;
                        updateRecord.ExitInterviewDate = resignationModel.ExitInterviewDate;
                        updateRecord.LastModifiedDate = System.DateTime.Now;
                    }
                    updateRecord.HRId = GetEmployeeId(resignationModel.SubId);
                    updateRecord.ResignationStatus = resignationModel.ResignationStatus.Trim() + BYHR;
                    updateRecord.HRRemark = resignationModel.Remark;
                    updateRecord.IsRehiredByHR = resignationModel.IsRehired;
                    _context.Update(updateRecord);
                    _context.SaveChanges();
                }
            }
            return updateRecord;
        }
    }
}
