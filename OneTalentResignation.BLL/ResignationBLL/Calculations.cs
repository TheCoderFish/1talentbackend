using System;
using OneTalentResignation.BLL.Interface;
using OneTalentResignation.DTO.View_Models;

namespace OneTalentResignation.BLL.ResignationBLL
{
    public class Calculations : ICalculations
    {
        /// <summary>
        /// Method Name     : CalculateApprovedNoticePeriod
        /// Author          : Mrunali Patel
        /// Creation Date   : 16 October 2019
        /// Purpose         : To Calculate Approve Notice Period.
        /// Revision        :
        /// </summary>
        /// <param name="resignationApprovedDate"> ResignationApprovedDate</param>
        /// <param name="raisedOnDate"> RaisedOnDate</param>
        /// <returns>Return Number Of Days Diffrence Between ResignationApprovedDate And  raisedOnDate</returns>
        public int? CalculateApprovedNoticePeriod(DateTime? resignationApprovedDate, DateTime raisedOnDate)
        {
            if (resignationApprovedDate.HasValue)
            {
                return Math.Abs(raisedOnDate.Subtract(resignationApprovedDate.Value).Days);
            }
            return null;
        }

        /// <summary>
        /// Method Name     : CalculateApprovedRelieveningDate
        /// Author          : Yaseen Agwan
        /// Creation Date   : 5 November 2019
        /// Purpose         : Calulate Relieve Date from ProposedDate and Notice Period
        /// Revision        :
        /// </summary>
        /// <param name="employee"> Employee</param>
        /// <returns>DateTime</returns>
        public DateTime CalculateApprovedRelieveningDate(DateTime? proposedDate, int noticePeriod)
        {
            return proposedDate.Value.AddDays(noticePeriod);
        }

        /// <summary>
        /// Method Name     : CalculateProposedNoticePeriod
        /// Author          : Mrunali Patel
        /// Creation Date   : 16 October 2019
        /// Purpose         : To Calculate Proposed Notice Period.
        /// Revision        :
        /// </summary>
        /// <param name="resignationProposedDate">ResignationProposedDate</param>
        /// <param name="raisedOnDate">RaisedOnDate</param>
        /// <returns>Return Number Of Days Diffrence Between resignationProposedDate And  raisedOnDate</returns>
        public int? CalculateProposedNoticePeriod(DateTime? resignationProposedDate, DateTime raisedOnDate)
        {
            if (resignationProposedDate.HasValue)
            {
               return Math.Abs(raisedOnDate.Subtract(resignationProposedDate.Value).Days);
            }
            return null;
        }

        /// <summary>
        /// Method Name     : IsApprovedByHr
        /// Author          : Mrunali Patel
        /// Creation Date   : 17 October 2019
        /// Purpose         : Check whether Resignation is approved By HR Or Not
        /// Revision        :
        /// </summary>
        /// <param name="employee"> Employee</param>
        /// <returns><c>true</c> if the Approve By HR; otherwise <c>false</c>.</returns>
        public bool IsApprovedByHr(EmployeeResignationViewModel employee)
        {
            return employee.HRId != null && employee.ResignationApprovedDate != null && employee.ExitInterviewDate != null ? true : false;
        }

        /// <summary>
        /// Method Name     : IsApprovedByRm
        /// Author          : Mrunali Patel
        /// Creation Date   : 17 October 2019
        /// Purpose         : Check whether Resignation is approved By RM Or Not
        /// Revision        :
        /// </summary>
        /// <param name="employee"></param>
        /// <returns><c>true</c> if the Approve By RM; otherwise <c>false</c>.</returns>
        public bool IsApprovedByRm(EmployeeResignationViewModel employee)
        {
            return employee.RMId != null ? true : false;
        }

        /// <summary>
        /// Method Name     : IsRevoked
        /// Author          : Mrunali Patel
        /// Creation Date   : 17 October 2019
        /// Purpose         : Check Whether Employee Resignation The Application Revoke Or Not
        /// Revision        :
        /// </summary>
        /// <param name="employee"> Employee</param>
        /// <returns><c>true</c> if the Revoke By Employee; otherwise <c>false</c>.</returns>
        public bool IsRevoked(EmployeeResignationViewModel employee)
        {
            if (employee.RevokeReason != null)
            {
                return employee.RevokeReason.Equals("revoked", StringComparison.OrdinalIgnoreCase) ? true : false;
            }
            return false;
        }
    }
}

