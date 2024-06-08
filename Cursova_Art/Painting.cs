using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cursova_Art
{
    public class Painting
    {
        public string Title { get; set; }
        public Artist ArtistName { get; set; }
        public int Year { get; set; }
        public string Style { get; set; }
        public string Genre { get; set; }
        public int Price { get; set; }
        public string PersonalCollectionFlag { get; set; }

        public Painting(string title, int year, string style, string genre, Artist artist, int price)
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
            if (PersonalCollectionFlag != null && (PersonalCollectionFlag == "Buy" || PersonalCollectionFlag == "Sell"))
            {
                return $"({Price}) {Title} ({ArtistName.Surname} {Year})";
            }
            return $"{Title} ({ArtistName.Surname} {Year})";
        }

        public static List<Painting> FindPaintings(List<Painting> paintings, string title, int year, string style, string genre, Artist artist, int price)
        {
            Artist unknownArtist = new Artist("Name", "Unknown", "", 0, "", "");

            List<Painting> foundPaintings = new List<Painting>();

            foreach (var p in paintings)
            {
                bool matches = true;

                if (!(title == "" || title == p.Title))
                {
                    matches = false;
                }

                if (!(year == 0 || p.Year == year))
                {
                    matches = false;
                }

                if (!(style == "" || p.Style == style))
                {
                    matches = false;
                }

                if (!(genre == "" || p.Genre == genre))
                {
                    matches = false;
                }

                if (!(price == 0 || p.Price == price))
                {
                    matches = false;
                }

                if (!(CompareArtists(unknownArtist, artist) || CompareArtists(p.ArtistName, artist)))
                {
                    matches = false;
                }

                if (matches)
                {
                    foundPaintings.Add(p);
                }
            }

            return foundPaintings;
        }

        private static bool CompareArtists(Artist a1, Artist a2)
        {
            return a1.Name == a2.Name &&
                   a1.Surname == a2.Surname &&
                   a1.Country == a2.Country &&
                   a1.BirthYear == a2.BirthYear &&
                   a1.Style == a2.Style &&
                   a1.Genre == a2.Genre;
        }
    }
}
