using System.ComponentModel.DataAnnotations;

namespace WebAPI_Basics.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

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
