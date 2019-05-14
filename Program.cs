using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIProject
{
    class Program
    {
        static List<Location> locations;
        static string start = "Fagaras";
        static string finish = "Giurgiu";
        static void Main(string[] args)
        {

            ReadLocations();
            WriteLocations();
            UserLocations();
            List<Location> path = FindPath();


            
            Console.ReadKey();
        }

        /// <summary>
        /// Gets the path from the start to the end
        /// </summary>
        /// <returns>Location list that shows all places that is needed to take</returns>
        static List<Location> FindPath() {
            List<string> places = new List<string>();
            List<Location> path = new List<Location>();
            Location curLocation = FindLocation(start);

            places.Add(start);
            path.Add(curLocation);

            while (places.Last() != finish)
            {
               

                //Removes all the paths that are already found
                foreach (string item in places)
                {
                    curLocation.RemovePath(item);
                }

                //If there is any paths to take
                if (curLocation.PathCount() > 0)
                {
                    curLocation = FindLocation(curLocation.paths.First().toWhere);
                    path.Add(curLocation);
                }
                //If there is no paths to take
                else
                {
                    path.RemoveAt(path.Count - 1);
                    curLocation = path.Last();
                }

                //Add to places that you were
                if (!places.Contains(curLocation.currentLocation))
                {
                    places.Add(curLocation.currentLocation);
                }


                PrintPath(path);
            }
            return path;
        }

        static void PrintPath(List<Location> locations) {
            string output = null;
            foreach (Location item in locations)
            {
                if (output == null)
                    output = item.currentLocation;
                else
                output += " --> " + item.currentLocation;
            }
            Console.WriteLine(output);
        }

        /// <summary>
        /// Finds location object by string
        /// </summary>
        /// <param name="location">Name of the location</param>
        /// <returns>Location object</returns>
        static Location FindLocation(string location)
        {
            Location currentLoc = new Location();
            foreach (Location item in locations)
            {
                if (location == item.currentLocation)
                {
                    currentLoc = item;
                }
            }
            return currentLoc;
        }

        /// <summary>
        /// Asks the user to type in the start and finish locations
        /// </summary>
        static void UserLocations()
        {
            Console.WriteLine("Start from:");
            start = Console.ReadLine();
            //Makes to an uppercase name
            start = char.ToUpper(start[0]) + start.Substring(1);
            while (true)
            {
                foreach (Location item in locations)
                {
                    if (start == item.currentLocation)
                    {
                        goto StartF;
                    }
                }
                Console.WriteLine("There is no such location. Write it again");
                start = Console.ReadLine();
                start = char.ToUpper(start[0]) + start.Substring(1);
            }
            StartF:
            Console.WriteLine("Finish to:");
            finish = Console.ReadLine();
            finish = char.ToUpper(finish[0]) + finish.Substring(1);
            while (true)
            {
                foreach (Location item in locations)
                {
                    if (finish == item.currentLocation)
                    {
                        return;
                    }
                }
                Console.WriteLine("There is no such location. Write it again");
                finish = Console.ReadLine();
                finish = char.ToUpper(finish[0]) + finish.Substring(1);
            }
        }

        static void WriteLocations()
        {
            Console.WriteLine("Locations:\n");
            foreach (var item in locations)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("");
        }

        /// <summary>
        /// Reads the file Locations.txt for all of the paths
        /// </summary>
        static void ReadLocations()
        {
            locations = new List<Location>();
            using (StreamReader read = new StreamReader("../../Locations.txt"))
            {
                string line;
                while ((line = read.ReadLine()) != null)
                {
                    string[] values = line.Split(',');
                    string location = values[0];
                    List<Path> paths = new List<Path>();
                    try
                    {
                        for (int i = 1; i < values.Length; i = i + 2)
                        {
                            Path path = new Path(values[i], int.Parse(values[i + 1]));
                            paths.Add(path);
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Something is not right with the loaction " + location);
                        throw;
                    }

                    locations.Add(new Location(location, paths));
                }
            }
        }
    }
}
