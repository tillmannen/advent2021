public static class Day1 {

    public static void Run(){
        var increases = CountIncreases();
        var threeMeasurementIncreases = CountThreeMeasurementIncreases();

        Console.WriteLine("1a: " + increases);
        Console.WriteLine("1b: " + threeMeasurementIncreases);

    }
    private static List<int> ReadInput()
    {
       return System.IO.File.ReadAllLines("2021/1/input.txt").Select(x => int.Parse(x)).ToList();
    }
    private static int CountIncreases()
    {
        var depths = ReadInput();
        var count = 0;
        for(var i = 0; i < depths.Count()-1; i++){
            var currentDepth = depths[i];
            var nextDepth = depths[i+1];
            if(currentDepth < nextDepth)
                count++;
        }
        return count;
    }

    private static int CountThreeMeasurementIncreases()
    {
        var depths = ReadInput();
        var count = 0;
        for(var i = 0; i < depths.Count()-3; i++){
            var currentDepth = depths[i]+depths[i+1]+depths[i+2];
            var nextDepth = depths[i+1]+depths[i+2]+depths[i+3];
            if(currentDepth < nextDepth)
                count++;
        }
        return count;
    }

}