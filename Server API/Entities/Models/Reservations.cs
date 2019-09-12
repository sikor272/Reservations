using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("reservations")]
    public class Reservations
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("teacher_id")]
        public int Teacher_id { get; set; }

        [Column("subject_id")]
        public int Subject_id { get; set; }

        [Column("room_id")]
        public int Room_id { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }  

        [Column("begin")]
        public int Begin { get; set; }

        [Column("end")]
        public int End { get; set; }
    }
}
