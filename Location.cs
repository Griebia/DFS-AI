using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIProject
{
    class Location
    {
        public string currentLocation { get; set; }
        public List<Path> paths { get; set; }

        public Location() {
        }

        public Location(string currentLocation,List<Path> paths) {
            this.currentLocation = currentLocation;
            this.paths = paths;
        }

        public bool RemovePath(string name) {

            foreach (Path item in paths)
            {
                if (item.toWhere == name)
                {
                    paths.Remove(item);
                    return true;
                }
            }
            return false;
        }

        public int PathCount()
        {
            return paths.Count;
        }

        public override string ToString()
        {
            string loc = currentLocation;
            //foreach (Path item in paths)
            //{
            //    loc += " " + item.toWhere + " " + item.price.ToString();
            //}
            return loc;
        }
    }

    class Path {
        public string toWhere { get; set; }
        public int price { get; set; }

        public Path(string toWhere, int price) {
            this.toWhere = toWhere;
            this.price = price;
        }
    }
}
