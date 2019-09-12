using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("subjects")]
    public class Subjects
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name of subject is required")]
        [StringLength(100, ErrorMessage = "Name of subject can't be longer than 100 characters")]
        [Column("name")]
        public string Name { get; set; }
    }
}
