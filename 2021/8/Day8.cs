public static class Day8{
    public static void Run(){
        A();
        B();
    }

    private static IEnumerable<Entry> GetInput(){
        //var stringList = FileHelper.ReadInput("2021/8/exampleInput.txt");
        var stringList = FileHelper.ReadInput("2021/8/input.txt");

        foreach(var line in stringList){
            var parts = line.Split(" | ");
            yield return new Entry(parts[0].Split(' '), parts[1].Split(' '));
        }     
        
    }

    private static void A(){
        var entries = GetInput();
        var allOutputs = entries.SelectMany(e => e.Output);
        var lengths = new[] {2,3,4,7};
        var count = allOutputs.Count(p => lengths.Contains(p.Length));

        Console.WriteLine("8a: "+count);
    }

    

    private static void B(){
        var entries = GetInput();
        var sum = entries.ToList().Select(e => e.GetOutput()).Sum();
        Console.WriteLine("8b: "+sum);
    }

    private class Entry{
        public Entry(string[] patterns, string[] output)
        {
            Patterns = patterns;
            Output = output;
        }
        public string[] Patterns { get; set; }
        public string[] Output { get; set; }
        public List<char[]> OrderedPatterns {
            get{
                return Patterns.Select(p => p.ToCharArray().OrderBy(c => c).ToArray()).ToList();
            }
        } 

        public int GetOutput(){

            var one = OrderedPatterns.First(p => p.Count() == 2);
            var four = OrderedPatterns.First(p => p.Count() == 4);
            var seven = OrderedPatterns.First(p => p.Count() == 3);
            var eight = OrderedPatterns.First(p => p.Count() == 7);
            
            var two = OrderedPatterns.First(p => p.Count() == 5 && p.Intersect(four).Count() == 2);
            var five = OrderedPatterns.First(p => p.Count() == 5 && p.Intersect(four).Count() == 3 && p.Intersect(two).Count() == 3);
            var three = OrderedPatterns.First(p => p.Count() == 5 && p.Intersect(four).Count() == 3 && p.Intersect(two).Count() == 4);
            
            var six = OrderedPatterns.First(p => p.Count() == 6 && p.Intersect(three).Count() == 4 && p.Intersect(one).Count() == 1);
            var zero = OrderedPatterns.First(p => p.Count() == 6 && p.Intersect(three).Count() == 4 && p.Intersect(one).Count() == 2);
            var nine = OrderedPatterns.First(p => p.Count() == 6 && p.Intersect(three).Count() == 5 && p.Intersect(one).Count() == 2);

            var dedcuctedPattern = new Dictionary<string, int>();
            
            dedcuctedPattern.Add(new string(zero), 0);
            dedcuctedPattern.Add(new string(one), 1);
            dedcuctedPattern.Add(new string(two), 2);
            dedcuctedPattern.Add(new string(three), 3);
            dedcuctedPattern.Add(new string(four), 4);
            dedcuctedPattern.Add(new string(five), 5);
            dedcuctedPattern.Add(new string(six), 6);
            dedcuctedPattern.Add(new string(seven), 7);
            dedcuctedPattern.Add(new string(eight), 8);
            dedcuctedPattern.Add(new string(nine), 9);

            var stringOutput = "";

            foreach(var number in Output){
                var orderedNumber = new string(number.ToCharArray().OrderBy(c => c).ToArray());
                stringOutput += dedcuctedPattern[orderedNumber].ToString();
            }

            return int.Parse(stringOutput);
        }

        
    }
}