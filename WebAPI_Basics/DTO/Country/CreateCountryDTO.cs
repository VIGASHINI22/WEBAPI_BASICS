using System.ComponentModel.DataAnnotations;

namespace WebAPI_Basics.DTO.Country
{
    public class CreateCountryDTO
    {
      

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(5)]
        public string ShortName { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        public string CountryCode { get; set; } = string.Empty;

    }
}

//note:
//DTO = Data Transfer Object will provide wrapper fro entity or database model to avoid directly expose in API.
