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

    private static ulong BCountFishesAfterDays(List<int> input, int Days){
        ulong[] fishAgeCount = new ulong[9];
        foreach(var fishAge in input) {
            fishAgeCount[fishAge]++;
        }

        for(var i = 0; i < Days; i++){
            fishAgeCount = MoveNumbers(fishAgeCount);
        }

        return CountFishes(fishAgeCount);
    }
    private static ulong CountFishes(ulong[] fishAges){
        ulong fishcount = 0;
        foreach(var age in fishAges){
            fishcount += age;
        }
        return fishcount;
    }
    static ulong[] MoveNumbers(ulong[] numbers) {
        ulong[] newNumbers = new ulong[9];
        newNumbers[6] = numbers[0];
        newNumbers[8] = numbers[0];
        for(int i = 0; i < 8; i++) {
            newNumbers[i] += numbers[i+1];
        }
        return newNumbers;
    }
    private static void B(){
        var input = GetInput();
        var result = BCountFishesAfterDays(input, 256);

        Console.WriteLine("6b: "+result);
    }
}