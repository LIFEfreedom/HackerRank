using System;
using System.IO;

namespace determining_dna_health
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			string[] lines = File.ReadAllLines("TestCase_13.txt");

			int n = Convert.ToInt32(lines[0]);

			int[] healthes = Array.ConvertAll(lines[2].Split(' '), healthTemp => Convert.ToInt32(healthTemp));

			var aca = new AhoCorasick();

			aca.AddGens(lines[1], healthes);

			long min = long.MaxValue;
			long max = long.MinValue;

			int s = Convert.ToInt32(lines[3]);

			for (int i = 1; i <= s; i++)
			{
				string[] firstLastd = lines[3 + i].Split(' ');

				uint first = Convert.ToUInt32(firstLastd[0]);

				uint last = Convert.ToUInt32(firstLastd[1]);

				var sum = aca.FindAllOccurrences(firstLastd[2], first, last);

				if (sum < min)
				{
					min = sum;
				}

				if (sum > max)
				{
					max = sum;
				}
			}

			if (min == long.MaxValue)
			{
				min = 0;
			}

			if (max == long.MinValue)
			{
				max = 0;
			}

			Console.WriteLine($"{min} {max}");

			Console.ReadKey();
		}
	}
}
