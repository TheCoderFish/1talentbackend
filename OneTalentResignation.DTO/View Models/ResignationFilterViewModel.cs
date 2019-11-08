using System;
using System.ComponentModel.DataAnnotations;


namespace OneTalentResignation.DTO.View_Models
{
    public class ResignationFilterViewModel
    {
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string ResignationStatus { get; set; }

        public string TechnologyName { get; set; }

        public string DomainName { get; set; }

        public string DesignationName { get; set; }

        public string EmployeeName { get; set; }

        public int? EmployeeCode { get; set; }
        
        public string Role { get; set; }

        public string SubId { get; set; }

        [Range(1,int.MaxValue,ErrorMessage = "Input is Not Valid")]
        public int PageNumber { get; set; }

        public int Limit { get; set; }
    }
}
