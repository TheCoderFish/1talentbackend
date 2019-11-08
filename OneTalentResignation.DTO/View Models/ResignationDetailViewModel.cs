using Newtonsoft.Json;
using OneTalentResignation.DTO.ValidationAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace OneTalentResignation.DTO.View_Models
{
    public class ResignationDetailViewModel
    {
        [JsonProperty("employeeId")]
        public int EmployeeId { get; set; }

        [JsonProperty("relieveDate")]
        [Required]
        [ValidateDate]
        public DateTime RelieveDate { get; set; }
        
        [JsonProperty("reason")]
        [MaxLength(250)]
        [MinLength(25)]
        public string Reason { get; set; }

        [JsonProperty("ccPersons")]
        public int[] CcPersons { get; set; }
    }
}
