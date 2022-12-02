public static class Day11{
    public static void Run(){
        A();
        B();
    }

    private static Dictionary<(int,int),Octopus> GetInput(){
        var stringList = FileHelper.ReadInput("2021/11/exampleInput.txt");
        //stringList = FileHelper.ReadInput("2021/10/input.txt");
        var returnValue = new Dictionary<(int,int),Octopus>();
        for(int i = 0; i < stringList.Count(); i++){
            var intList = stringList[i].ToCharArray().Select(x => int.Parse(x.ToString())).ToList();
            for(int j = 0; j < intList.Count(); j++){
                returnValue.Add((i,j),new Octopus{ Value = intList[j]});
            }
        }

        return returnValue;
    }

    private static void A(){
        var octopuses = GetInput();        
        Console.WriteLine("11a: "+null);
    }

    private static void B(){
        
        Console.WriteLine("11b: "+null);
    }

    private class Octopus{
        public int Value { get; set; }
        public bool HasFlashed { get; set; }
    }
}