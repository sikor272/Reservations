namespace Models
{
    public class Teachers
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Title { get; set; }

        public override string ToString()
        {
            return Title + " " + Name + " " + Surname;
        }
        public ComboBoxItem ComboItem()
        {
            return new ComboBoxItem(Id, this.ToString());
        }
    }
}
