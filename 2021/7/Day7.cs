public static class Day7{
    public static void Run(){
        A();
        B();
    }

    private static List<int> GetInput(){
        //var stringList = FileHelper.ReadInput("2021/7/exampleInput.txt");
        var stringList = FileHelper.ReadInput("2021/7/input.txt");
        return stringList[0].Split(',').Select(s => int.Parse(s)).ToList();
    }

    private static int CalculateMinFuel(List<int> positions){
        var min = positions.Min();
        var max = positions.Max();
        var minFuel = 0;
        for(int i = min; i < max; i++){
            var fuel = CalculateFuel(positions, i);
            if(minFuel == 0 || fuel < minFuel)
                minFuel = fuel;
        }
        return minFuel;
    }

    private static int CalculateFuel(List<int> positions, int target){
        var fuel = 0;
        foreach(var position in positions){
            fuel += Math.Abs(position-target);
        }
        return fuel;
    }

    private static int CalculateMinBFuel(List<int> positions){
        var min = positions.Min();
        var max = positions.Max();
        var minFuel = 0;
        for(int i = min; i < max; i++){
            var fuel = CalculateBFuel(positions, i);
            if(minFuel == 0 || fuel < minFuel)
                minFuel = fuel;
        }
        return minFuel;
    }
    private static int CalculateBFuel(List<int> positions, int target){
        var fuel = 0;
        foreach(var position in positions){
            var steps = Math.Abs(position-target);
            var fuelCost = 1;
            if(steps.IsEven()){
                fuelCost = steps/2 * steps + (steps/2);
            }
            else{
                fuelCost = (steps+1)/2 * steps;
            }
            fuel += fuelCost;
        }
        return fuel;
    }

//1,3,6,10,15,21,28,36
//96798233

    private static bool IsEven(this int input){
        return input % 2 == 0;
    }

    private static void A(){
        var positions = GetInput();
        var result = CalculateMinFuel(positions);

        Console.WriteLine("7a: "+result);
    }

    private static void B(){
        var positions = GetInput();
        var result = CalculateMinBFuel(positions);

        Console.WriteLine("7b: "+result);
    }
}