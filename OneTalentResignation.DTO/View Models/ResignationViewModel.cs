using System;

namespace OneTalentResignation.DTO.View_Models
{
    public class ResignationViewModel
    {
        public int ResignationId { get; set; }

        public bool Status { get; set; }

        public int ManagerId { get; set; }

        public string ResignationStatus { get; set; }

        public string Remark { get; set; }

        public bool IsRehired { get; set; }

        public DateTime? ResignationApprovalDate { get; set; }

        public DateTime? ExitInterviewDate { get; set; }

        public int? HRId { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public DateTime? DeletedDate { get; set; }

        public string Role { get; set; }

        public string SubId { get; set; }
    }
}
