using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace OneTalentResignation.DTO.View_Models
{
    public class ResignationRevokeViewModel
    {
        public int ResignationId { get; set; }

        [MaxLength(250)]
        [MinLength(25)]
        [Required]
        [JsonProperty("revokeReason")]
        public string RevokeRemarks { get; set; }
    }
}
