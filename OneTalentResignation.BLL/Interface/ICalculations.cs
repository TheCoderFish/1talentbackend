using OneTalentResignation.DTO.View_Models;
using System;

namespace OneTalentResignation.BLL.Interface
{
    /// <summary>
    /// Interface       : ICalculations
    /// Author          : Yaseen Agwan
    /// Creation Date   : 17 October 2019
    /// Purpose         : To implement Dependency Injection
    /// </summary>
    public interface ICalculations
    {
        /// <summary>
        /// Method Name     : CalculateApprovedNoticePeriod
        /// Author          : Mrunali Patel
        /// Creation Date   : 16 October 2019
        /// Purpose         : To Calculate Approved Notice Period.
        /// <param name="resignationApprovedDate"> ResignationApprovedDate</param>
        /// <param name="raisedOnDate"> RaisedOnDate </param>
        int? CalculateApprovedNoticePeriod(DateTime? resignationApprovedDate, DateTime raisedOnDate);

        /// <summary>
        /// Method Name     : CalculateProposedNoticePeriod
        /// Author          : Mrunali Patel
        /// Creation Date   : 16 October 2019
        /// Purpose         : To Calculate Proposed Notice Period.
        /// <param name="resignationProposedDate"> ResignationProposedDate</param>
        /// <param name="raisedOnDate"> RaisedOnDate </param>
        int? CalculateProposedNoticePeriod(DateTime? resignationProposedDate, DateTime raisedOnDate);

        /// <summary>
        /// Method Name     : IsApprovedByHr
        /// Author          : Mrunali Patel
        /// Creation Date   : 17 October 2019
        /// Purpose         : Check whether Resignation is approved By HR Or Not
        /// Revision        :
        /// </summary>
        /// <param name="employee"> Employee</param>
        /// <returns></returns>
        bool IsApprovedByHr(EmployeeResignationViewModel employee);


        /// <summary>
        /// Method Name     : IsApprovedByRm
        /// Author          : Mrunali Patel
        /// Creation Date   : 17 October 2019
        /// Purpose         : Check whether Resignation is approved By RM Or Not
        /// Revision        :
        /// </summary>
        /// <param name="employee"> Employee </param>
        /// <returns></returns>
        bool IsApprovedByRm(EmployeeResignationViewModel employee);

        /// <summary>
        /// Method Name     : IsRevoked
        /// Author          : Mrunali Patel
        /// Creation Date   : 17 October 2019
        /// Purpose         : Check Whether Employee Resignation The Application Revoke Or Not
        /// Revision        :
        /// </summary>
        /// <param name="employee"> Employee</param>
        /// <returns></returns>
        bool IsRevoked(EmployeeResignationViewModel employee);

        /// <summary>
        /// Method Name     : CalculateApprovedRelieveningDate
        /// Author          : Yaseen Agwan
        /// Creation Date   : 5 November 2019
        /// Purpose         : Calulate Relieve Date from ProposedDate and Notice Period
        /// Revision        :
        /// </summary>
        /// <param name="employee"> Employee</param>
        /// <returns>DateTime</returns>
        DateTime CalculateApprovedRelieveningDate(DateTime? proposedDate, int noticePeriod);
    }
}
