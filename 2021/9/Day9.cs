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

    private static List<Point> GetAllPoints(Dictionary<int, int[]> input){
        var result = new List<Point>();
        
        foreach(var row in input){
            var rowIndex = row.Key;

            var heights = row.Value;

            for(int columnIndex = 0; columnIndex < heights.Length; columnIndex++){
                var point = GetPoint(input, rowIndex, columnIndex);
                
                result.Add(point);
            }
        }

        return result;
    }

    private static Point GetPoint(Dictionary<int, int[]> input, int row, int column){
        var currentRow = input[row];
        var prevRow = input.ContainsKey((int)(row - 1)) ? input[(int)(row - 1)] : null;
        var nextRow = input.ContainsKey((int)(row + 1)) ? input[(int)(row + 1)] : null;


        int value=currentRow[column],top=10,left=10,right=10,bottom=10;

        if(prevRow != null){
            top = prevRow[column];
        }
        if(nextRow != null){
            bottom = nextRow[column];
        }
        if(column > 0)
            left = currentRow[column-1];
        if(column < currentRow.Length-1)
            right = currentRow[column+1];
        
        return new Point(value, top, left, right, bottom, row, column);
    }

    private static List<Point> FindNonNineNotEvaluatedNeigbours(Point point, List<Point> allPoints, List<Point> basin){
        if(point.IsEvaluated)
            return basin;
        
        basin.Add(point);

        allPoints.First(p => p.Row == point.Row && p.Column == point.Column).IsEvaluated = true;

        if(point.TopNeighbour < 9){
            var top = allPoints.First(p => p.Row == point.Row-1 && p.Column == point.Column);
            basin = FindNonNineNotEvaluatedNeigbours(top, allPoints, basin);
        }
        if(point.BottomNeighbour < 9){
            var bottom = allPoints.First(p => p.Row == point.Row+1 && p.Column == point.Column);
            basin = FindNonNineNotEvaluatedNeigbours(bottom, allPoints, basin);
        }
        if(point.LeftNeigbour < 9){
            var left = allPoints.First(p => p.Row == point.Row && p.Column == point.Column-1);
            basin = FindNonNineNotEvaluatedNeigbours(left, allPoints, basin);
        }
        if(point.RightNeighbour < 9){
            var right = allPoints.First(p => p.Row == point.Row && p.Column == point.Column+1);
            basin = FindNonNineNotEvaluatedNeigbours(right, allPoints, basin);
        }


        return basin;
    }

    private static int FindBasinsSizeSum(Dictionary<int, int[]> input){
        var lowpoints = FindLowpoints(input);
        var allPoints = GetAllPoints(input);

        var basinSizes = new List<int>();
        foreach(var lowpoint in lowpoints){
            basinSizes.Add(FindNonNineNotEvaluatedNeigbours(lowpoint, allPoints, new List<Point>()).Count());
        }
        int result = 1;

        foreach(var size in basinSizes.OrderByDescending(b => b).Take(3)){
            result = result * size;
        }

        return result;
    }

    private static void B(){
        var input = GetInput();
        var sum = FindBasinsSizeSum(input);
        Console.WriteLine("9b: "+sum);
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
        public bool IsEvaluated { get; set; }
        public bool IsLowpoint => Value < TopNeighbour && Value < LeftNeigbour && Value < RightNeighbour && Value < BottomNeighbour;
    }
}