using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursova_Art
{
    public class Auction
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
        public List<Painting> Lots { get; set; }

        public Auction(string name, string address, DateTime date, List<Painting> lots)
        {
            Name = name;
            Address = address;
            Date = date;
            Lots = lots;
        }

        public override string ToString()
        {
            return $"{Name} ({Date.Day}.{Date.Month}.{Date.Year})";
        }
    }
}
