using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursova_Art
{
    public class Museum
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Painting> Paintings { get; set; }

        public Museum(string name, string address, List<Painting> paintings)
        {
            Name = name;
            Address = address;
            Paintings = paintings;
        }

        public override string ToString()
        {
            return Name;
        }  
    }
}
