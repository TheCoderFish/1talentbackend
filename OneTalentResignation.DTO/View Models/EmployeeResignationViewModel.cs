using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OneTalentResignation.DTO.View_Models
{
    public class EmployeeResignationViewModel
    {
        [JsonProperty("employeeId")]
        public int EmployeeId { get; set; }

        [JsonProperty("employeeResignationId")]
        public int ResignationId { get; set; }

        public int? HRId { get; set; }

        public int? RMId { get; set; }

        [JsonProperty("IsApprovedByHr")]
        public bool? IsApprovedByHr { get; set; }

        [JsonProperty("IsApprovedByRm")]
        public bool? IsApprovedByRm { get; set; }

        [JsonProperty("resignationRequestDate")]
        public DateTime? ResignationProposedDate { get; set; }

        [JsonProperty("resignationApprovalDate")]
        public DateTime? ResignationApprovedDate { get; set; }

        [JsonProperty("onBoardingNoticePeriod")]
        public byte NoticePeroid { get; set; }

        [JsonProperty("proposedNoticePeriod")]
        public int? ProposedNoticePeriod { get; set; }

        [JsonProperty("approvedNoticePeriod")]
        public int? ApprovedNoticePeriod { get; set; }

        [JsonProperty("requestDate")]
        public DateTime RaisedOnDate { get; set; }
        

        [JsonProperty("resignationReason")]
        public string ResignationReason { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("revokeReason")]
        public string RevokeReason { get; set; }

        [JsonProperty("rmRemarks")]
        public string ManageRemarks { get; set; }

        [JsonProperty("hrRemarks")]
        public string HRRemarks { get; set; }

        [JsonProperty("exitInterviewDate")]
        public DateTime? ExitInterviewDate { get; set; }

        [JsonProperty("ccPersons")]
        public ICollection<string> ConcernEmployees { get; set; }
    }
}
