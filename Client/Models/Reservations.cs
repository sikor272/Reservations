using System;

namespace Models
{
    public class Reservations
    {
        public int Id { get; set; }

        public int Teacher_id { get; set; }

        public int Subject_id { get; set; }

        public int Room_id { get; set; }

        public DateTime Date { get; set; }  

        public int Begin { get; set; }

        public int End { get; set; }
    }
}
