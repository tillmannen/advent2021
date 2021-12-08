public static class Day4
{
    public static void Run(){
        A();
        B();
    }

    private static List<BingoBoard> GetBingoBoards(){
        var input = FileHelper.ReadInput("2021/4/input.txt");
        
        var bingoBoards = new List<BingoBoard>();
        var board = new BingoBoard();

        var addedRows = 0;
        var index = 0;
        foreach(var row in input){
            if(index < 2){
                index++;
                continue;
            }
            
            if(string.IsNullOrWhiteSpace(row)){
                bingoBoards.Add(board);
                board = new BingoBoard();
                addedRows = 0;
                index++;
                continue;
            }

            board.AddRow(row, addedRows);
            addedRows++;
            
            index++;
        }

        return bingoBoards;
    }

    private static int[] GetDrawnNumbers(){
        var input = FileHelper.ReadInput("2021/4/input.txt");
        
        return input[0].Split(',').Select(n => int.Parse(n)).ToArray();
    }

    private static void A(){

        var bingoBoards = GetBingoBoards();
        var drawnNumbers = GetDrawnNumbers();

        var score = 0;
        foreach(var drawnNumber in drawnNumbers){
            bingoBoards.ForEach(b => b.MarkNumber(drawnNumber));
            if(bingoBoards.Any(b => b.HasBingo)){
                var winningBoard = bingoBoards.FirstOrDefault(b => b.HasBingo);
                score = winningBoard.GetScore(drawnNumber);
                break;
            }
        }

        Console.WriteLine("4a: "+score);
    }

    private static void B(){

        var bingoBoards = GetBingoBoards();
        var drawnNumbers = GetDrawnNumbers();

        var score = 0;
        foreach(var drawnNumber in drawnNumbers){
            bingoBoards.ForEach(b => b.MarkNumber(drawnNumber));
            if(bingoBoards.Any(b => b.HasBingo)){
                if(bingoBoards.Where(b => !b.HasWon).Count() == 1 && bingoBoards.Any(b => b.HasBingo && !b.HasWon)){
                    var winningBoard = bingoBoards.FirstOrDefault(b => b.HasBingo && !b.HasWon);
                    score = winningBoard.GetScore(drawnNumber);
                    break;
                }
                bingoBoards.Where(b => b.HasBingo && !b.HasWon).ToList().ForEach(b => b.HasWon = true);
            }
        }

        Console.WriteLine("4b: "+score);
    }

    private class BingoBoard{

        public BingoBoard()
        {
            Numbers = new List<BingoNumber>();
        }
        public List<BingoNumber> Numbers { get; set; }
        public bool HasWon { get; set; }
        public bool HasBingo { 
            get {
                    
                    var rowBingo = Numbers.Where(b => b.Marked).GroupBy(b => b.Row).Select(group => new{ MarkedNumbers = group.Count()}).Any(r => r.MarkedNumbers == 5);
                    var colBingo = Numbers.Where(b => b.Marked).GroupBy(b => b.Column).Select(group => new{ MarkedNumbers = group.Count()}).Any(r => r.MarkedNumbers == 5);
                    return rowBingo || colBingo;
            }
        }

        public void AddRow(string row, int rowIndex){
            Numbers.AddRange(row.Replace("  "," ").Trim().Split(' ').Select(n => int.Parse(n)).ToArray()
                .Select((number, column) => new BingoNumber{ Number = number, Column = column, Row = rowIndex}));
        }

        public void MarkNumber(int number){
            if(Numbers.Any(n => n.Number == number))
                Numbers.FirstOrDefault(n => n.Number == number).Marked = true;
        }

        public int GetScore(int lastNumberCalled) {
            return Numbers.Where(n => !n.Marked).Select(n => n.Number).Sum() * lastNumberCalled;
        }

        public int SumNotMarked => Numbers.Where(n => !n.Marked).Select(n => n.Number).Sum();
    }

    private class BingoNumber{
        public int Number { get; set; }
        public bool Marked { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }
}