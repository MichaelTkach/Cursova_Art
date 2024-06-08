using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursova_Art
{
    public class ConsignmentShop
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int Comission { get; set; }
        public List<Painting> Paintings { get; set; }

        public ConsignmentShop(string name, string address, int comission, List<Painting> paintings)
        {
            Name = name;
            Address = address;
            Comission = comission;
            Paintings = paintings;
        }

        public override string ToString()
        {
            return $"{Name} ({Address})";
        }
    }
}
