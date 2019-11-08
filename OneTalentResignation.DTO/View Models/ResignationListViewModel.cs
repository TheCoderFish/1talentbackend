using System;

namespace OneTalentResignation.DTO.View_Models
{
    public class ResignationListViewModel
    {
        public int ResignationId { get; set; }

        public string ResignationStatus { get; set; }

        public string TechnologyName { get; set; }

        public string DomainName { get; set; }

        public string DesignationName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int EmployeeCode { get; set; }

        public int? ManagerId { get; set; }

        public DateTime? RaisedOnDate { get; set; }

        public DateTime? ResignationApprovedDate { get; set; }

        public int? ResignationManagerId { get; set; }

    }
}
