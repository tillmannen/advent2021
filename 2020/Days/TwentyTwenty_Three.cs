public static class TwentyTwenty_Three{
    public static void Run(){
        ThreeA();
        ThreeB();
    }

    private static int GoRight(int currentPos, int steps, string row){
        for(var i = 0; i < steps; i++){
            currentPos++;
            if(currentPos > row.Length-1)
                currentPos = 0;
        }

        return currentPos;
    }
    private static string GetThreeOnePath(List<string> stringList){
        string fullPath = "";
        var rightPos = 0;
        foreach(var row in stringList){
            fullPath += row.Substring(rightPos,1);
            rightPos = GoRight(rightPos, 3,row);
        }

        return fullPath;
    }

    private static void ThreeA(){
        var stringList = FileHelper.ReadInput("2020/Days/three.txt");
        var path = GetThreeOnePath(stringList);

        Console.WriteLine("2020 3A: "+path.ToCharArray().Where(c => c == '#').Count());
    }
    private static void ThreeB(){
        var stringList = FileHelper.ReadInput("2020/Days/three.txt");

        Console.WriteLine("2020 3B: "+null);
    }
}