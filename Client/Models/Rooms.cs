﻿
namespace Models
{
    public class Rooms
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
        public ComboBoxItem ComboItem()
        {
            return new ComboBoxItem(Id, this.ToString());
        }
    }
}
