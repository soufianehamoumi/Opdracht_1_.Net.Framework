using System.ComponentModel.DataAnnotations;

namespace Studentenbeheer.Models
{
    public class Module
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Naam")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Omschrijving")]
        public string Omschrijving { get; set; }

        public DateTime Deleted { get; set; }
    }
}
