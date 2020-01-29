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
                            view(appCollection);
                            break;
                        case "3":
                            commonInterval(appCollection);
                            break;
                        case "x": flag = false; break;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"\t***{ex.Message.ToString()}***");
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
                            if(int.Parse(start) >= 1 && int.Parse(start) <= 24 && (int.Parse(end) >= 1 && int.Parse(end) <= 24))
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
                            else
                            {
                                throw new Exception("Parse Error");
                            }
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
                throw new Exception("Parse Error");
            }

            return new appliances
            {
                 applianceName = name, appIntervals = connected
            };
        }
        static void view(appliances[] collection)
        {
            Console.WriteLine("\n\tVIEW(2)");
            for (int i = 0; i < collection.Length; i++)
            {
                Console.Write($"\t{collection[i].applianceName}");
                Console.Write(": {");
                for (int j = 0; j < collection[i].appIntervals.Length; j++)
                {
                    Console.Write($"{collection[i].appIntervals[j].start},{collection[i].appIntervals[j].end}");
                    Console.Write("}");
                    if(j != collection[i].appIntervals.Length-1)
                    {
                        Console.Write(", ");
                    }
                }
                Console.WriteLine();
            }
        }
        static void commonInterval(appliances[] collection)
        {
            var item = new range[collection.Length];
            var common = new string[0];
            var temp = new string[0];
            for (int i = 0; i < collection.Length; i++)
            {
                item[i].value = new int[24];
                for (int j = 0; j < collection[i].appIntervals.Length; j++)
                {
                    var start = collection[i].appIntervals[j].start;
                    var end = collection[i].appIntervals[j].end;
                    
                    for (int k = 0; k < item[i].value.Length; k++)
                    {
                        if(k >= (start-1) && k <= (end-1))
                        {
                            item[i].value[k] = 1;
                        }
                    }
                }
            }

            var flag2 = false;
            for (int i = 0; i < 24; i++)
            {
                var flag = item.Length;
                for (int j = 0; j < item.Length; j++)
                {
                    if (item[j].value[i] != 1)
                    {
                        flag--;
                        if (flag2)
                        {
                            Array.Resize(ref temp, temp.Length + 1);
                            temp[temp.Length - 1] = i.ToString();
                            flag2 = false;
                        }
                        break;
                    }
                }

                if(flag == item.Length)
                {
                    Array.Resize(ref temp, temp.Length + 1);
                    temp[temp.Length - 1] = (i + 1).ToString();
                    if ((temp.Length % 2) != 0)
                    {
                        flag2 = true;
                    }
                    else
                    {
                        flag2 = false;
                    }
                }
            }

            return;
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
    public struct range
    {
        public int[] value;
    }
}
