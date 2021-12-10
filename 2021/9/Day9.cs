public static class Day9{
    public static void Run(){
        A();
        B();
    }

    private static Dictionary<int, int[]> GetInput(){
        //var stringList = FileHelper.ReadInput("2021/9/exampleInput.txt");
        var stringList = FileHelper.ReadInput("2021/9/input.txt");
        var result = new Dictionary<int,int[]>();
        for(int i = 0; i < stringList.Count(); i++){
            result.Add(i, (stringList[i].ToCharArray().Select(c => int.Parse(c.ToString())).ToArray()));
        }

        return result;
    }

    private static List<Point> FindLowpoints(Dictionary<int, int[]> input){
        var result = new List<Point>();

        foreach(var row in input){
            var index = row.Key;
            var prevRow = input.ContainsKey(index-1) ? input[index-1] : null;
            var nextRow = input.ContainsKey(index+1) ? input[index+1] : null;

            var heights = row.Value;

            for(int i = 0; i < heights.Length; i++){
                int value=heights[i],top=10,left=10,right=10,bottom=10;

                if(prevRow != null){
                    top = prevRow[i];
                }
                if(nextRow != null){
                    bottom = nextRow[i];
                }
                if(i > 0)
                    left = heights[i-1];
                if(i < heights.Length-1)
                    right = heights[i+1];
                
                if(value < top && value < left && value < right && value < bottom)
                    result.Add(new Point(value, top, left, right, bottom, row.Key, i));
            }
        }

        return result;
    }

    private static void A(){
        var input = GetInput();
        var lowpoints = FindLowpoints(input);
        var result = lowpoints.Select(l => l.RiskLevel).Sum();
        Console.WriteLine("9a: "+result);
    }

    private static int FindBasins(Dictionary<int, int[]> input){
        
        var evaluatedPoints = new Dictionary<int, int>();
        

        return 0;
    }

    private static void B(){
        var input = GetInput();
        var basins = FindBasins(input);
        Console.WriteLine("9b: "+null);
    }

    private class Point{
        public Point(int value, int top, int left, int right, int bottom, int row, int column)
        {
            Value = value;
            TopNeighbour = top;
            LeftNeigbour = left;
            RightNeighbour = right;
            BottomNeighbour = bottom;   
            Row = row;
            Column = column;
        }
        public int Value { get; set; }
        public int TopNeighbour { get; set; }
        public int LeftNeigbour { get; set; }
        public int RightNeighbour { get; set; }
        public int BottomNeighbour { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int RiskLevel => Value + 1;
    }
}