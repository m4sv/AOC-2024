using System.Text.RegularExpressions;

namespace AdventOfCode2024
{
    public class Day03
    {
        public static void Part01()
        {
            var text = File.ReadAllText("inputs/day03.txt");
            var regex = new Regex("mul\\([0-9]+,[0-9]+\\)", RegexOptions.None);
            var values = regex.Matches(text).Select(x => RunMulOperation(x)).ToList();
            Console.WriteLine($"Instruction Sum: {values.Sum()}");
        }

        public static void Part02()
        {
            var text = File.ReadAllText("inputs/day03.txt");
            var dontIndex = -1; 
            while ((dontIndex = text.IndexOf("don't()")) >= 0)
            {
                var substr = text.Substring(dontIndex, text.Length - dontIndex);
                var doIndex = substr.IndexOf("do()");
                if (doIndex == -1) doIndex = substr.Length-1;
                var removalString = substr.Substring(0, doIndex);
                text = text.Replace(removalString, "");
            }
            var mulRegex = new Regex("mul\\([0-9]+,[0-9]+\\)");
            var values = mulRegex.Matches(text).Select(x => RunMulOperation(x)).ToList();
            Console.WriteLine($"Instruction Sum: {values.Sum()}");
        }

        public static int RunMulOperation(Match match)
        {
            var numbers = match.Value.Replace("mul(", "").Replace(")", "").Split(',')
                                     .Select(x=> int.Parse(x)).ToArray();
            return numbers[0] * numbers[1];
        }
    }
}
