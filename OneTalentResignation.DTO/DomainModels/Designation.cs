using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneTalentResignation.DTO.DomainModel
{

    [Table("Designation", Schema = "Ref")]
    public class Designation
    {
        [Key]
        public int DesignationId { get; set; }

        public string DesignationName { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public DateTime DeletedDate { get; set; }
    }
}
