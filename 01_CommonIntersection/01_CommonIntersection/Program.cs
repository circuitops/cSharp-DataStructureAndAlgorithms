using System;

/* Common intersections in a list of intervals
 * I'm looking for an algorithm to find the common intersections of a list of sets (of intervals). 
 * To illustrate an example, let's imagine that we have home appliances and we want to know when 
 * all of them are turned on.
 * 
 * TV: {1,10}, {16, 19}
 * Radio: {2, 5}, {12, 18}
 * Oven: {4, 5}, {15, 16}
 * 
 * Each device is connected in several intervals. If you analyze the hours (in the form {start, end}), 
 * you will soon realize that the intervals in which all of them are turned on are:
 * {4, 5}, {16, 16}
 * 
 * Ref: https://www.reddit.com/r/algorithms/comments/euqcqz/common_intersections_in_a_list_of_intervals/
 */

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
                            printCommonInterval(appCollection);
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
                if (connected.Length == 0)
                {
                    throw new Exception("Parse Error");
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
                Console.Write(": ");
                for (int j = 0; j < collection[i].appIntervals.Length; j++)
                {
                    Console.Write("{"+$"{collection[i].appIntervals[j].start},{collection[i].appIntervals[j].end}");
                    Console.Write("}");
                    if(j != collection[i].appIntervals.Length-1)
                    {
                        Console.Write(", ");
                    }
                }
                Console.WriteLine();
            }
        }
        static void printCommonInterval(appliances[] collection)
        {
            var common = getCommon(preTable(collection));

            Console.WriteLine("\n\tView Common(3)");
            Console.Write($"\tCommon Interval:  ");
            for (int i = 0; i < common.Length; i++)
            {
                if ((i % 2) != 0)
                {
                    Console.Write($"{common[i]}" + "} ");
                }
                else
                {
                    Console.Write("{" + $"{common[i]},");
                }
            }
        }
        static range[] preTable(appliances[] collection)
        {
            var item = new range[collection.Length];
            for (int i = 0; i < collection.Length; i++)
            {
                item[i].value = new int[24];
                for (int j = 0; j < collection[i].appIntervals.Length; j++)
                {
                    for (int k = 0; k < item[i].value.Length; k++)
                    {
                        if (k >= (collection[i].appIntervals[j].start - 1) && 
                            k <= (collection[i].appIntervals[j].end - 1))
                        {
                            item[i].value[k] = 1;
                        }
                    }
                }
            }
            return item;
        }
        static string[] getCommon(range[] item)
        {
            var temp = new string[0];
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
                            temp = addCommon(temp, i.ToString());
                            flag2 = false;
                        }
                        break;
                    }
                }

                if (flag == item.Length)
                {
                    temp = addCommon(temp, (i + 1).ToString());
                    flag2 = (temp.Length % 2) != 0 ? true : false;
                }
            }
            return temp;
        }
        static string[] addCommon(string[] array, string item)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = item;
            return array;
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
