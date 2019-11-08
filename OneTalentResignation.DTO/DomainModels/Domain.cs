using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace OneTalentResignation.DTO.DomainModel
{

    [Table("Domain",Schema="Ref")]
    public class Domain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DomainId { get; set; }

        [MaxLength(30)]
        public string DomainName { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public DateTime DeletedDate { get; set; }
    }
}
