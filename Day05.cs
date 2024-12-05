namespace AdventOfCode2024
{
    public class Day05
    {
        public static void Part01()
        {
            var middlePageNumberSum = 0;
            var text = File.ReadAllLines("inputs/day05.txt");

            var updateList = new List<List<int>>();
            var rules = new PageCollection();

            foreach (var line in text)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                if (line[2] == '|') rules.AddPage(line);
                else if (line[2] == ',') updateList.Add(line.Split(',').Select(x => int.Parse(x)).ToList());
            }

            foreach (var update in updateList)
            {
                if (rules.IsUpdateListValid(update))
                {
                    middlePageNumberSum += update[update.Count / 2];
                }
            }
            Console.WriteLine($"{middlePageNumberSum} is the sum of middle page numbers");
        }

        public static void Part02()
        {
            var middlePageNumberSum = 0;
            var text = File.ReadAllLines("inputs/day05.txt");

            var updateList = new List<List<int>>();
            var rules = new PageCollection();

            foreach (var line in text)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                if (line[2] == '|') rules.AddPage(line);
                else if (line[2] == ',') updateList.Add(line.Split(',').Select(x => int.Parse(x)).ToList());
            }

            foreach (var update in updateList)
            {
                if (!rules.IsUpdateListValid(update))
                {
                    while(!rules.IsUpdateListValid(update))
                    {
                        rules.OrderUpdate(update);
                    }
                    middlePageNumberSum += update[update.Count / 2];
                }
            }
            Console.WriteLine($"{middlePageNumberSum} is the sum of fixed middle page numbers");
        }

        public class PageCollection
        {
            public PageCollection()
            {
                Pages = new List<Page>();
            }
            public void AddPage(string input)
            {
                var split = input.Split('|');
                var number = int.Parse(split[0]);
                var printsBefore = int.Parse(split[1]);
                var page = Pages.FirstOrDefault(x => x.Number == number);

                if (page != null) page.PrintsBefore.Add(printsBefore);
                else Pages.Add(new Page { Number = number, PrintsBefore = new List<int> { printsBefore } });
            }
            public bool IsUpdateListValid(List<int> list)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    var page = Pages.FirstOrDefault(x => x.Number == list[i]);
                    if (page == null) continue;
                    for (int j = 0; j < i; j++)
                    {
                        if (page.PrintsBefore.Contains(list[j])) return false;
                    }
                }
                return true;
            }
            public List<int> OrderUpdate(List<int> list)
            {
                for(int i = list.Count - 1; i>=0; i--)
                {
                    var page = Pages.FirstOrDefault(x => x.Number == list[i]);
                    if (page == null) continue;
                    for (int j = 0; j < i; j++)
                    {
                        if (page.PrintsBefore.Contains(list[j]))
                        {
                            var leftValue = list[j];
                            list[j] = list[i];
                            list[i] = leftValue;
                        }
                    }
                }
                return list;
            }
            public List<Page> Pages { get; set; }
        }

        public class Page
        {
            public Page()
            {
                PrintsBefore = new List<int>();
            }
            public int Number { get; set; }
            public List<int> PrintsBefore { get; set; }
        }
    }
}

