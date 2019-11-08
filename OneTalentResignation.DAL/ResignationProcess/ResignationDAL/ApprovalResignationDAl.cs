using System.Collections.Generic;
using OneTalentResignation.DTO.View_Models;
using OneTalentResignation.DTO.DomainModel;

namespace OneTalentResignation.DAL.ResignationProcess.ResignationDAL
{
    public class ApprovalResignationDAL
    {

        private readonly DatabaseQuery _databaseQuery;

        /// <summary>
        ///  Constructor   : DatabaseQuery
        /// Author        : Binal Patel
        /// Creation Date : 17 October 2019
        /// Purpose       : To resolve the dependency
        /// </summary>
        /// <param name="databaseQuery"></param>
        public ApprovalResignationDAL(DatabaseQuery databaseQuery)
        {
            _databaseQuery = databaseQuery;
        }

        /// <summary>
        /// Method Name     : GetDomain
        /// Author          : Vrunda Patel
        /// Creation Date   : 16 oct 2019
        /// Purpose         : Featching the list of  Domain Name from Database.
        /// </summary>
        public List<string> GetDomain()
        {
            return _databaseQuery.GetDomain();
        }

        /// <summary>
        /// Method Name     : GetTechnology
        /// Author          : Divya Patel
        /// Creation Date   : 16 oct 2019
        /// Purpose         : Featching the list of  Technology Name from Database.
        /// </summary>
        public List<string> GetTechnology()
        {
            return _databaseQuery.GetTechnology();
        }


        /// <summary>
        /// Method Name     : GetDesignation
        /// Author          : Binal Patel
        /// Creation Date   : 16 oct 2019
        /// Purpose         : Featching the list of  Designation Name from Database.
        /// </summary>
        public List<string> GetDesignation()
        {
            return _databaseQuery.GetDesignation();
        }

        /// <summary>
        /// Method Name     : GetStatus
        /// Author          : Binal Patel
        /// Creation Date   : 16 oct 2019
        /// Purpose         : Featching the list of Status Name from Database.
        /// </summary>
        public List<string> GetStatus()
        {
            return _databaseQuery.GetStatus();

        }

        /// <summary>
        /// Method Name     : GetResignationEmployeeList
        /// Author          : Yogesh Parmar
        /// Creation Date   : 17 oct 2019
        /// Purpose         : Featching the list of ResignationEmployeeList Name from Database
        /// Revision        : By Nilesh Avsthi add Paging to this Method 18 oct 2019
        /// </summary>
        /// <param name="paging">paging</param>
        /// <param name="Id">Id</param>  
        public List<ResignationListViewModel> GetResignationEmployeeList(ResignationFilterViewModel filter)
        {
            return _databaseQuery.GetResignationEmployeeList(filter);

        }

        /// <summary>
        /// Method Name     : GetEmployeeDetails
        /// Author          : Divya Patel
        /// Creation Date   : 16 October 2019
        /// Purpose         : Invokes the get API request for EmployeeDetails View for Approve and Reject
        /// </summary>
        /// <param name="employeeId">employeeId</param>
        /// <returns></returns>
        public EmployeeDetailViewModel GetResignationDetails(int resignationId,string subId)
        {
            return _databaseQuery.GetResignationDetails(resignationId, subId);

        }

        /// <summary>
        ///  Method Name    : EmployeeId
        /// Author          : Nilesh Avasthi
        /// Creation Date   : 16 October 2019
        /// Purpose         :
        /// </summary>
        /// <param name="subId">subId</param>
        /// <returns></returns>
        public int EmployeeId(string subId)
        {
            return _databaseQuery.GetEmployeeId(subId);
        }

        /// <summary>
        /// Method Name     : Approve
        /// Author          : Vrunda Patel & Nilesh avasthi
        /// Creation Date   : 17 October 2019
        /// Purpose         : Invokes the Post API request for Approve and Reject
        /// Revision        : 
        /// </summary>
        /// <param name="resignationModel">resignationModel</param>
        public void Approve(ResignationViewModel resignationModel)
        {
            _databaseQuery.Approve(resignationModel);
        }

        /// <summary>
        /// Method Name     : Put
        /// Author          : Binal Patel
        /// Creation Date   : 17 October 2019
        /// Purpose         : Invokes the Put API request for HR 
        /// </summary>
        /// <param name="resignationModel">resignationModel</param>
        /// <returns></returns>
        public Resignation Put(ResignationViewModel resignationModel)
        {
            return _databaseQuery.Put(resignationModel);
        }

    }
}

