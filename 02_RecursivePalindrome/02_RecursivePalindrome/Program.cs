using System;
//  Recursive Palindrome
namespace _02_RecursivePalindrome
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter a Palindrome: ");
            var input = Console.ReadLine();
            Console.WriteLine($"{input} is{(recursive(0, input, true) == true ? "" : " not")} a Palindrome");
            Console.Read();
        }

        static bool recursive(int i, string input, bool flag)
        {
            i++;
            if (i < input.Length && flag)
            {
                if (!input[i - 1].Equals(input[input.Length - i]))
                {
                    flag = false;
                }
                return recursive(i, input, flag);
            }
            return flag;
        }
    }
}
