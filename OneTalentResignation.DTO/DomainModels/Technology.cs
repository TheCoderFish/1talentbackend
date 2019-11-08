using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneTalentResignation.DTO.DomainModel
{

    [Table("Technology", Schema = "Ref")]
    public class Technology
    {
        [Key]
        public int TechnologyId { get; set; }

        public string TechnologyName { get; set; }

        public int DomainId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public DateTime DeletedDate { get; set; }
    }
}
