using System.Collections;
using System.Text.RegularExpressions;

namespace AdventOfCode2024
{
    public class Day04
    {
        public static void Part01()
        {
            var text = File.ReadAllLines("inputs/day04.txt");
            var reverseText = text.Select(x => new string(x.Reverse().ToArray())).ToArray();

            var lines = new List<string>();
            lines.AddRange(Rotate90Degrees(text));
            lines.AddRange(Rotate45Degrees(text));
            lines.AddRange(Rotate45Degrees(reverseText));

            var matchlist = new List<Match>();
            foreach (var line in lines)
            {
                var xmasMatches = Regex.Matches(line, "XMAS").ToList();
                var reverseXmasMatches = Regex.Matches(line, "SAMX").ToList();
                matchlist.AddRange(xmasMatches);
                matchlist.AddRange(reverseXmasMatches);
            }

            Console.WriteLine($"{matchlist.Count} XMAS instances detected");
        }

        public static string[] Rotate90Degrees(string[] text)
        {
            var xlen = text[0].Length;
            var ylen = text.Length;
            var inputArray = text.Select(x => x.ToCharArray()).ToArray();
            var outputArray = text.Select(x => x.ToCharArray()).ToArray();
            for (int y = 0; y < ylen; y++)
            {
                for (int x = 0; x < xlen; x++)
                {
                    outputArray[y][x] = inputArray[x][y];
                }
            }
            var result = outputArray.Select(x => new string(x)).ToArray();

            return result;
        }

        static string[] Rotate45Degrees(string[] array)
        {
            var size = array.Length;
            var result = new List<string>();
            int index = 0;
            while (index < 2 * size - 1)
            {
                var str = "";
                var line = new ArrayList();
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (i + j == index) line.Add(array[i][j]);
                    }
                }
                for (int i = line.Count - 1; i >= 0; i--)
                {
                    str += line[i];
                }
                result.Add(str);
                index += 1;
            }
            return result.ToArray();
        }

        public static void Part02()
        {
            var totalCount = 0;
            var text = File.ReadAllLines("inputs/day04.txt");
            var array = text.Select(x => x.ToArray()).ToArray();

            for (var y = 1; y < array.Length-1; y++)
            {
                for (var x = 1; x < array.Length-1; x++)
                {
                    var item = array[y][x];
                    var UL = array[y - 1][x - 1];
                    var UR = array[y - 1][x + 1];
                    var DL = array[y + 1][x - 1];
                    var DR = array[y + 1][x + 1];
                    if (item == 'A')
                    {
                        if ((UL == 'S' && UR == 'S' && DL == 'M' && DR == 'M') ||
                            (UL == 'M' && UR == 'M' && DL == 'S' && DR == 'S') ||
                            (UL == 'S' && UR == 'M' && DL == 'S' && DR == 'M') ||
                            (UL == 'M' && UR == 'S' && DL == 'M' && DR == 'S'))
                        {
                            totalCount++;
                        }
                    }
                }
            }

            Console.WriteLine($"{totalCount}");
        }

    }
}

