using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursova_Art
{
    public class Artist
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Country { get; set; }
        public int BirthYear { get; set; }
        public string Style { get; set; }
        public string Genre { get; set; }

        public Artist(string name, string surname, string country, int birthYear, string style, string genre)
        {
            Name = name;
            Surname = surname;
            Country = country;
            BirthYear = birthYear;
            Style = style;
            Genre = genre;
        }

        public override string ToString()
        {
            return $"{Surname} {Name}";
        }

        public static List<Artist> FindArtists(List<Artist> artists, string firstName, string lastName, string country, int birthYear, string style, string genre)
        {
            List<Artist> foundArtists = new List<Artist>();

            foreach (var artist in artists)
            {
                bool matches = true;

                if (!(firstName == "" || firstName == artist.Name))
                {
                    matches = false;
                }

                if (!(lastName == "" || lastName == artist.Surname))
                {
                    matches = false;
                }

                if (!(country == "" || country == artist.Country))
                {
                    matches = false;
                }

                if (!(birthYear == 0 || artist.BirthYear == birthYear))
                {
                    matches = false;
                }

                if (!(style == "" || style == artist.Style))
                {
                    matches = false;
                }

                if (!(genre == "" || genre == artist.Genre))
                {
                    matches = false;
                }

                if (matches)
                {
                    foundArtists.Add(artist);
                }
            }

            return foundArtists;
        }
    }
}
