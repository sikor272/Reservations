using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("teachers")]
    public class Teachers
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters")]
        [Column("name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        [StringLength(50, ErrorMessage = "Surname can't be longer than 50 characters")]
        [Column("surname")]
        public string Surname { get; set; }

        //[Required(ErrorMessage = "Title is required")]
        [StringLength(30, ErrorMessage = "Title can't be longer than 30 characters")]
        [Column("title")]
        public string Title { get; set; }
    }
}
