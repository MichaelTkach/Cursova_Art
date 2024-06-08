using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursova_Art
{
    public class Review
    {
        public Painting painting { get; set; }
        public string Text { get; set; }

        public Review(string text, Painting painting)
        {
            Text = text;
            this.painting = painting;
        }

        public override string ToString()
        {
            var lines = Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            var firstLine = lines.Length > 0 ? lines[0] : string.Empty;
            return $"{painting.Title}: {firstLine}...";
        }
    }
}
