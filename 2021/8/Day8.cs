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
            yield return new Entry{ Patterns = parts[0].Split(' '), Output = parts[1].Split(' ')};
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
        
        Console.WriteLine("8b: "+null);
    }

    private class Entry{
        public string[] Patterns { get; set; }
        public string[] Output { get; set; }
    }
}