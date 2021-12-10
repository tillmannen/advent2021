public static class Day6{
    public static void Run(){
        A();
        B();
    }

    private static List<int> GetInput(){
        var stringList = FileHelper.ReadInput("2021/6/input.txt");
        var ints = stringList[0].Split(',').Select(i => int.Parse(i));
        return ints.ToList();
    }

    private static int CountFishesAfterDays(List<int> input, int Days){
        var currentFishes = input;
        var nextDaysFishes = new List<int>();
        for(var i = 0; i < Days; i++){
            foreach(var fish in currentFishes){
                if(fish == 0){
                    nextDaysFishes.Add(6);
                    nextDaysFishes.Add(8);
                }
                else
                    nextDaysFishes.Add(fish-1);
            }
            currentFishes = nextDaysFishes;
            nextDaysFishes = new List<int>();
        }

        return currentFishes.Count();
    }

    private static void A(){
        var input = GetInput();
        var result = CountFishesAfterDays(input, 80);

        Console.WriteLine("6a: "+result);
    }

    private static long BCountFishesAfterDays(List<int> input, int Days){
        long[] fishAgeCount = new long[9];
        foreach(var fishAge in input) {
            fishAgeCount[fishAge]++;
        }

        for(var i = 0; i < Days; i++){
            fishAgeCount = AgeFishes(fishAgeCount);
        }

        return CountFishes(fishAgeCount);
    }

    static long[] AgeFishes(long[] fishAges) {
        long[] newFishAges = new long[9];
        newFishAges[6] = fishAges[0];
        newFishAges[8] = fishAges[0];
        for(int i = 0; i < 8; i++) {
            newFishAges[i] += fishAges[i+1];
        }
        return newFishAges;
    }

    private static long CountFishes(long[] fishAges){
        long fishcount = 0;
        foreach(var age in fishAges){
            fishcount += age;
        }
        return fishcount;
        //1601616884019
    }

    private static void B(){
        var input = GetInput();
        var result = BCountFishesAfterDays(input, 256);

        Console.WriteLine("6b: "+result);
    }
}