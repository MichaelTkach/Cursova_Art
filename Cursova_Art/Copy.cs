using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursova_Art
{
    public class Copy : Painting
    {
        public Copy(string title, int year, string style, string genre, Artist artist, int price)
            : base(title, year, style, genre, artist, price)
        {
            Title = title;
            Year = year;
            Style = style;
            Genre = genre;
            ArtistName = artist;
            Price = price;
        }

        public override string ToString()
        {
            return "Copy: " + base.ToString();
        }
    }
}
