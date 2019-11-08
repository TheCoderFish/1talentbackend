using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneTalentResignation.DTO.DomainModel
{
    [Table("ConcernEmployee",Schema="Resignation")]
    public class ConcernEmployee
    {
        [Key]
        public int ConcernEmployeeId { get; set; }

        [Required]
        public int ResignationId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
