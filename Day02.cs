using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode2024
{
    public class Day02
    {
        public static void Part01()
        {
            using (var reader = File.OpenText("inputs/day02.txt"))
            {
                var safeCount = 0;
                var line = "";
                var safeDict = new Dictionary<string, bool>();
                while ((line = reader.ReadLine()) != null)
                {
                    var lineNumbers = line.Split(' ').Select(x => int.Parse(x)).ToList();
                    var safe = IsSafe(lineNumbers);
                    if (safe) safeCount++;
                    safeDict.Add(line, safe);
                }
                Console.Write($"{safeCount} safe");
            }
        }

        private static bool IsSafe(List<int> report)
        {
            var safe = false;
            var item = 0;
            var next = 0;
            var ascending = report.Last() > report.First();
            for (int i = 0; i < report.Count(); i++)
            {
                item = report[i];
                if ((i + 1) < report.Count())
                {
                    next = report[i + 1];
                    var diff = Math.Abs(item - next);
                    if (diff != 0 && diff <= 3)
                    {
                        safe = true;
                    }
                    else
                    {
                        safe = false; break;
                    }
                    if (ascending && item > next)
                    {
                        safe = false;
                        break;
                    }
                    if (!ascending && item < next)
                    {
                        safe = false;
                        break;
                    }
                }
            }
            return safe;
        }

        private static List<List<int>> GetDampenedReports(List<int> report)
        {
            var result = new List<List<int>>();
            var safe = false;
            var item = 0;
            var next = 0;
            var ascending = report.Last() > report.First();
            var removeIndex = 0;
            for (int i = 0; i < report.Count(); i++)
            {
                item = report[i];
                if ((i + 1) < report.Count())
                {
                    next = report[i + 1];
                    var diff = Math.Abs(item - next);
                    if (diff != 0 && diff <= 3)
                    {
                        safe = true;
                    }
                    else
                    {
                        safe = false;
                    }
                    if ((ascending && item > next) ||
                        (!ascending && item < next)) safe = false;
                    if (safe == false)
                    {
                        removeIndex = i; 
                        break;
                    }
                }
            }
            result.Add(report.Where((v, i) => i != removeIndex).ToList());
            result.Add(report.Where((v, i) => i != (removeIndex+1)).ToList());
            return result;
        }

        public static void Part02()
        {
            using (var reader = File.OpenText("inputs/day02.txt"))
            {
                var safeCount = 0;
                var line = "";
                var safeDict = new Dictionary<List<int>, bool>();
                while ((line = reader.ReadLine()) != null)
                {
                    var lineNumbers = line.Split(' ').Select(x => int.Parse(x)).ToList();
                    var safe = IsSafe(lineNumbers);
                    if (safe) safeCount++;
                    safeDict.Add(lineNumbers, safe);
                }
                var retryList = safeDict.Where(x=> !x.Value).Select(x=>x.Key).ToList();
                foreach (var retry in retryList)
                {
                    var damepenedList = GetDampenedReports(retry);
                    if (damepenedList.Any(x => IsSafe(x))) safeCount++;
                }
                Console.Write($"{safeCount} safe");
            }
        }
    }
}
