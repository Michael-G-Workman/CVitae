using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVitae.Models
{
    [Table("EmailContacts", Schema = "dbo")]
    public class EmailContact
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(150)]
        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(100)]
        [Display(Name = "Contact Email")]
        public string ContactEmail { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(20)]
        [Display(Name = "Phone Number")]
        public string ContactPhone { get; set; }

        [Required]
        [Display(Name = "Category ID")]
        public Guid ContactCategories_ID { get; set; }

        [Required]
        [Display(Name = "Email Message")]
        public string WebMessage { get; set; }
    }
}