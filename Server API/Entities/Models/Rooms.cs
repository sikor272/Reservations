using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("rooms")]
    public class Rooms
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name of room is required")]
        [StringLength(50, ErrorMessage = "Name of room can't be longer than 50 characters")]
        [Column("name")]
        public string Name { get; set; }

    }
}
