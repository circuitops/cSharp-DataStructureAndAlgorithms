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
                try
                {
                    Console.WriteLine("\n-----------OPTIONS-------------");
                    Console.WriteLine("Enter 1 - to Add Appliance and Hours Connected");
                    Console.WriteLine("Enter 2 - to View Collection");
                    Console.WriteLine("Enter 3 - to View Common Intervals");
                    Console.WriteLine("Enter x - to EXIT");

                    Console.Write(">>> ");
                    var input = Console.ReadLine();
                    switch (input)
                    {
                        case "1":
                            appCollection = addAppliances(appCollection, add());
                            Console.WriteLine("\tAdded");
                            break;
                        case "2":
                            break;
                        case "3":
                            break;
                        case "x": flag = false; break;
                    }
                }
                catch
                {
                    Console.WriteLine("Parse Failed");
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
        static appliances add()
        {
            Console.WriteLine("\n\tADD(1)");
            Console.Write("\tAppliance Name and Connected Hours: ");
            var input = Console.ReadLine();

            var name = string.Empty;
            var start = string.Empty;
            var end = string.Empty;
            var connected = new appUsedHour[0];
            var flag = false;
            var flagStart = false;
            var flagEnd = false;

            try
            {
                for (int i = 0; i < input.Length; i++)
                {
                    if (flag)
                    {
                        if (input[i].Equals('{'))
                        {
                            flagStart = true;
                            i++;
                        }
                        else if (input[i].Equals(','))
                        {
                            flagStart = false;
                            flagEnd = true;
                            i++;
                        }
                        else if (input[i].Equals('}'))
                        {
                            Array.Resize(ref connected, connected.Length + 1);
                            connected[connected.Length - 1] = new appUsedHour
                            {
                                start = int.Parse(start),
                                end = int.Parse(end)
                            };

                            start = string.Empty;
                            end = string.Empty;
                            flagStart = false;
                            flagEnd = false;
                            i++;
                        }

                        if (flagStart && !input[i].Equals(' '))
                        {
                            start += input[i].ToString();
                        }
                        if (flagEnd && !input[i].Equals(' '))
                        {
                            end += input[i].ToString();
                        }
                    }
                    else
                    {
                        if (!input[i].Equals(':'))
                        {
                            name += input[i].ToString();
                        }
                        else
                        {
                            flag = true;
                        }
                    }
                }
            }
            catch
            {
                throw new Exception();
            }

            return new appliances
            {
                 applianceName = name, appIntervals = connected
            };
        }
        static void view(appliances[] collection)
        {

        }
    }

    public struct appliances
    {
        public string applianceName;
        public appUsedHour[] appIntervals;
    }
    
    public struct appUsedHour
    {
        public int start;
        public int end;
    }
}
