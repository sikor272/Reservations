using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class ComboBoxItem
    {
        public string Text { get; protected set; }
        public int Value { get; protected set; }
        public ComboBoxItem(int value, string name)
        {
            this.Text = name;
            this.Value = value;
        }
        public override string ToString()
        {
            return Text;
        }
    }
}
