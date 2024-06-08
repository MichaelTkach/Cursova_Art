using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursova_Art
{
    public class Collector
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<Painting> Collection { get; set; }
        public string Email { get; set; }

        public Collector(string name, string surname, List<Painting> collection, string email)
        {
            Name = name;
            Surname = surname;
            Collection = collection;
            Email = email;
        }

        public override string ToString()
        {
            return $"{Surname} {Name}";
        }
    }
}
