using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_CommonIntersection
{
    class Program
    {
        static void Main(string[] args)
        {
            var appCollection = new appliances[0];
            var flag = true;
            while(flag)
            {
                Console.WriteLine("\n-----------OPTIONS-------------");
                Console.WriteLine("Enter 1 - to Add Appliance");
                Console.WriteLine("Enter 2 - to View Collection");
                Console.WriteLine("Enter 3 - to View Common Intervals");
                Console.WriteLine("Enter x - to EXIT");

                Console.Write(">>> ");
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":

                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "x": flag = false; break;
                }
            }

            Console.Read();
        }

        static appliances[] addAppliances(appliances[] collection, appliances item)
        {
            Array.Resize(ref collection, collection.Length + 1);
            collection[collection.Length - 1] = item;
            return collection;
        }
    }

    public struct appliances
    {
        public string applianceName;
        public appInterval[] appIntervals;
    }

    public struct appInterval
    {
        public appUsedHour[] appUsedHours;
    }

    public struct appUsedHour
    {
        public int start;
        public int end;
    }
}
