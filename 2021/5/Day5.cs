public static class Day5 {
    public static void Run(){
        A();
        B();
    }

    private static List<Line> GetLines(bool considerDiagonal = false){
        var stringList = FileHelper.ReadInput("2021/5/input.txt");
        //var stringList = FileHelper.ReadInput("2021/5/exampleInput.txt");
        
        return stringList.Select(s => new Line(s, considerDiagonal)).ToList();
    }

    private static void A(){
        var lines = GetLines();
        var grid = new Grid();

        var points = lines.SelectMany(l => l.Points).ToList();

        grid.MarkGrid(points);
        var result = grid.DPoints.Count(i => i.Value > 1);

        Console.WriteLine("5a: "+result);
    }
    private static void B(){
        var lines = GetLines(true);
        var grid = new Grid();

        var points = lines.SelectMany(l => l.Points).ToList();

        grid.MarkGrid(points);
        var result = grid.DPoints.Count(i => i.Value > 1);

        Console.WriteLine("5b: "+result);
    }

    private class Line {

        public Line(string input, bool considerDiagonal)
        {
            var splitString = input.Split(" -> ");

            var start = splitString[0].Split(',');
            StartPoint = new Point(int.Parse(start[0]), int.Parse(start[1]));

            var end = splitString[1].Split(',');
            EndPoint = new Point(int.Parse(end[0]), int.Parse(end[1]));

            var result = new List<Point>();
            var from = StartPoint;
            var to = EndPoint;

            if(IsHorizontalLine){
                var x = from.X;
                if(EndPoint.Y < StartPoint.Y){
                    from = EndPoint;
                    to = StartPoint;
                }

                for(var y = from.Y; y<=to.Y; y++){
                    result.Add(new Point(x,y));
                }
            }
            else if(IsVerticalLine){
                var y = from.Y;
                if(EndPoint.X < StartPoint.X){
                    from = EndPoint;
                    to = StartPoint;
                }

                for(var x = from.X; x<=to.X; x++){
                    result.Add(new Point(x,y));
                }
            }
            else if(considerDiagonal) {
                var yIncreases = false;
                if(EndPoint.X < StartPoint.X){
                    from = EndPoint;
                    to = StartPoint;
                }
                if(from.Y < to.Y)
                    yIncreases = true;
                var y = from.Y;

                for(var x = from.X; x<=to.X; x++){
                    result.Add(new Point(x,y));
                    if(yIncreases)
                        y++;
                    else 
                        y--;
                }
            }

            points = result;
        }

        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        
        public bool IsHorizontalLine => StartPoint.X == EndPoint.X;
        public bool IsVerticalLine => StartPoint.Y == EndPoint.Y;

        private List<Point> points;
        public List<Point> Points {
            get {
                return points;
            }
        }
    }

    private class Grid
    {
        public Dictionary<(int,int),int> DPoints { get; set; }

        public void MarkGrid(List<Point> points){
            if(DPoints == null)
                DPoints = new Dictionary<(int, int), int>();
            
            foreach(var point in points)
            {
                if(!DPoints.ContainsKey((point.X, point.Y)))
                    DPoints[(point.X, point.Y)] = 0;
                DPoints[(point.X, point.Y)]++;
            }
        }
    }

    private class Point {
        public Point(int x, int y)
        {
            X = x; 
            Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }
    }

    private class GridPoint : Point
    {
        public GridPoint(int x, int y) : base(x,y)
        {
            LinesTouching = 0;
        }

        public int LinesTouching { get; set; }
        public bool TouchedByAtLeastTwo => LinesTouching >= 2;
    }
}