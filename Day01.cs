using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    public class Day01
    {
        public static void Part01()
        {
            using (var reader = File.OpenText("inputs/day01.txt"))
            {
                var totalDistance = 0;
                var leftList = new List<int>();
                var rightList = new List<int>();
                var line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    var split = line.Split("   ");
                    leftList.Add(int.Parse(split[0]));
                    rightList.Add(int.Parse(split[1]));
                }
                leftList.Sort();
                rightList.Sort();

                for (int i = 0; i < leftList.Count; i++)
                {
                    totalDistance += Math.Abs(leftList[i] - rightList[i]);
                }

                Console.WriteLine($"{totalDistance} is the Total Distance.");
            }
        }

        public static void Part02()
        {
            using (var reader = File.OpenText("inputs/day01.txt"))
            {
                var totalSimilarity = 0;
                var leftList = new List<int>();
                var rightList = new List<int>();
                var line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    var split = line.Split("   ");
                    leftList.Add(int.Parse(split[0]));
                    rightList.Add(int.Parse(split[1]));
                }
                leftList.Sort();
                rightList.Sort();

                foreach(var item in leftList)
                {
                    var count = rightList.Count(x => x.Equals(item));
                    totalSimilarity += count*item;
                }
                Console.WriteLine($"{totalSimilarity} is the similarity score.");
            }
        }
    }
}
