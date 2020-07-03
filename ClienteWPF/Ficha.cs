using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteWPF
{
    public class Ficha
    {
        public int id { get; set; }
        public string color { get; set; }

        public Ficha(int id, string color)
        {
            this.id = id;
            this.color = color;
        }
    }
}
