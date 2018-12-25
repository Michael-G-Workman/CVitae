using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVitae.Models
{
    [Table("ContactCategories", Schema = "dbo")]
    public class ContactCategory
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(100)]
        [Display(Name = "Category")]
        public string category { get; set; }

        [Required]
        [Display(Name = "Sort Order")]
        public int sortorder { get; set; }
    }
}