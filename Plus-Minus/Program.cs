using System;
using System.IO;

namespace Plus_Minus
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string[] files = File.ReadAllLines("TestCase.txt");
            int[] arr = Array.ConvertAll(files[1].Split(' '), arrTemp => Convert.ToInt32(arrTemp));
            plusMinus(arr);
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }

        private static void plusMinus(int[] arr)
        {
            float positive;
            int positiveCount = 0;

            float negative;
            int negativeCount = 0;

            float zero;
            int zeroCount = 0;

            foreach (int el in arr)
            {
                if (el == 0)
                    zeroCount++;
                else if (el < 0)
                    negativeCount++;
                else
                    positiveCount++;
            }

            positive = ((float)positiveCount) / arr.Length;
            negative = ((float)negativeCount) / arr.Length;
            zero = 1 - positive - negative;

            Console.WriteLine(positive);
            Console.WriteLine(negative);
            Console.WriteLine(zero);
        }

    }
}
