using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Cursova_Art
{
    public class DataContainer
    {
        public List<Artist> Artists { get; set; }
        public List<Painting> Paintings { get; set; }
        public List<Collector> Collectors { get; set; }
        public List<Museum> Museums { get; set; }
        public List<Auction> Auctions { get; set; }
        public List<ConsignmentShop> ConsignmentShops { get; set; }
        public List<Copy> Copies { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Painting> PersonalCollection { get; set; }
        public List<Painting> OwnedPaintings { get; private set; }
        public List<Copy> OwnedCopies { get; private set; }

        public DataContainer(List<Artist> artists, List<Painting> paintings, List<Collector> collectors,
                             List<Museum> museums, List<Auction> auctions, List<ConsignmentShop> consignmentShops,
                             List<Copy> copies, List<Review> reviews, List<Painting> ownCollection)
        {
            Artists = artists ?? new List<Artist>();
            Paintings = paintings ?? new List<Painting>();
            Collectors = collectors ?? new List<Collector>();
            Museums = museums ?? new List<Museum>();
            Auctions = auctions ?? new List<Auction>();
            ConsignmentShops = consignmentShops ?? new List<ConsignmentShop>();
            Copies = copies ?? new List<Copy>();
            Reviews = reviews ?? new List<Review>();
            PersonalCollection = ownCollection ?? new List<Painting>();
            OwnedPaintings = new List<Painting>();
            OwnedCopies = new List<Copy>();

            UpdateOwnedCollections();
        }

        public void SaveToFile(string filePath)
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public static DataContainer LoadFromFile(string filePath)
        {
            string json = File.ReadAllText(filePath);
            var dataContainer = JsonConvert.DeserializeObject<DataContainer>(json);

            if (dataContainer != null)
            {
                dataContainer.UpdateOwnedCollections();
            }

            return dataContainer;
        }

        private void UpdateOwnedCollections()
        {
            OwnedPaintings.Clear();
            OwnedCopies.Clear();

            // Картины и копии в собственности музеев
            OwnedPaintings.AddRange(Museums.SelectMany(m => m.Paintings).Where(p => !(p is Copy)));
            OwnedCopies.AddRange(Museums.SelectMany(m => m.Paintings).OfType<Copy>());

            // Картины и копии в собственности аукционов
            OwnedPaintings.AddRange(Auctions.SelectMany(a => a.Lots).Where(p => !(p is Copy)));
            OwnedCopies.AddRange(Auctions.SelectMany(a => a.Lots).OfType<Copy>());

            // Картины и копии в собственности комиссионных магазинов
            OwnedPaintings.AddRange(ConsignmentShops.SelectMany(cs => cs.Paintings).Where(p => !(p is Copy)));
            OwnedCopies.AddRange(ConsignmentShops.SelectMany(cs => cs.Paintings).OfType<Copy>());

            // Картины и копии в собственности коллекционеров
            OwnedPaintings.AddRange(Collectors.SelectMany(c => c.Collection).Where(p => !(p is Copy)));
            OwnedCopies.AddRange(Collectors.SelectMany(c => c.Collection).OfType<Copy>());

            // Личная коллекция картин и копий
            OwnedPaintings.AddRange(PersonalCollection.Where(p => !(p is Copy)));
            OwnedCopies.AddRange(PersonalCollection.OfType<Copy>());
        }
    }
}
