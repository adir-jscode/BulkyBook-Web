using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]

        [DisplayName("Category Name")]
        [MaxLength(255)]
        public string Name { get; set; }
        [DisplayName("Display Order")]

        [Range(1,100,ErrorMessage ="Order must be between 1 to 100")]
        public int DisplayOrder { get; set; }
    }
}
