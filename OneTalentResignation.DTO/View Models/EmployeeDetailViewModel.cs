using System;
using System.Collections.Generic;

namespace OneTalentResignation.DTO.View_Models
{
    public class EmployeeDetailViewModel
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ManagerName { get; set; }

        public int ReportingManagerId { get; set; }

        public int ResignationId { get; set; }

        public int EmployeeCode { get; set; }

        public string EmployeeReason { get; set; }

        public string HrRemark { get; set; }

        public string ReportingPersonRemark { get; set; }

        public bool IsRehiredByRM { get; set; }

        public string Domain { get; set; }

        public string Designation { get; set; }

        public string Technology { get; set; }

        public DateTime? ProposedRelievingDate { get; set; }

        public int OnBoardingNoticePeriod { get; set; }

        public int? ProposedNoticePeriod { get; set; }

        public int? ApprovedNoticePeriod { get; set; }

        public string Status { get; set; }

        public DateTime RaisedOnDate { get; set; }

        public DateTime? ApprovedRelievingDate { get; set; }

        public bool IsRehiredByHR { get; set; }

        public DateTime? ExitInterviewDate { get; set; }

        public ICollection<string> ConcernEmployees { get; set; }

        public DateTime? ResignationProposedDate { get; set; }

        public DateTime? ResignationApprovedDate { get; set; }
    }

}
