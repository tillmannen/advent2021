public static class Day10{
    public static void Run(){
        A();
        B();
    }

    public static List<string> GetInput(){
        var stringList = FileHelper.ReadInput("2021/10/exampleInput.txt");
        stringList = FileHelper.ReadInput("2021/10/input.txt");
        return stringList;
    }

    private static char[] startChars = new char[]{'(','[','{','<'};
    private static Dictionary<char, int> endCharScores = new Dictionary<char, int>(){ {')',3},{']',57},{'}',1197},{'>',25137}};
    private static Dictionary<char, int> endCharMultipliers = new Dictionary<char, int>(){ {'(',1},{'[',2},{'{',3},{'<',4}};

    private static int GetErrorScore(List<string> input){
        input = RemovePairs(input);

        var errorscore = 0;
        foreach(var row in input){
            var charArray = row.ToCharArray();
            foreach(var c in charArray){
                if(endCharScores.ContainsKey(c)){
                    errorscore += endCharScores[c];
                    break;
                }
            }
        }

        return errorscore;
    }

    private static List<string> RemovePairs(List<string> input){

       for(var i = 0; i < input.Count(); i++){
            var row = input[i];
            
            var count = 0;
            do {
                count = 0;
                while(row.IndexOf("()") > -1){
                    row = row.Remove(row.IndexOf("()"),2);
                    count++;
                }
                while(row.IndexOf("{}") > -1){
                    row = row.Remove(row.IndexOf("{}"),2);
                    count++;
                }
                while(row.IndexOf("<>") > -1){
                    row = row.Remove(row.IndexOf("<>"),2);
                    count++;
                }
                while(row.IndexOf("[]") > -1){
                    row = row.Remove(row.IndexOf("[]"),2);
                    count++;
                }
            } while(count > 0);

            input[i] = row;
       }

        return input;
    }

    private static void A(){
        var stringList = GetInput();
        var errorScore = GetErrorScore(stringList);

        Console.WriteLine("10a: "+errorScore);
    }

    private static List<long> GetScores(List<string> rows){
        var scoreList = new List<long>();
        foreach(var row in rows){
            long score = 0;
            var chars = row.ToCharArray().Reverse();
            foreach(var c in chars){
                score *= 5;
                score += endCharMultipliers[c];
            }
            scoreList.Add(score);
        }
        return scoreList;
    }

    private static long GetMiddleScore(List<string> input){
        input = RemovePairs(input);

        var incompleteRows = new List<string>();

        foreach(var row in input){
            var charArray = row.ToCharArray();
            var corrupt = false;
            foreach(var c in charArray){
                if(endCharScores.ContainsKey(c)){
                    corrupt = true;
                    break;
                }
            }
            if(!corrupt)
                incompleteRows.Add(row);
        }

        var scores = GetScores(incompleteRows);
        scores.Sort();
        var middleScore = scores.ToArray()[(scores.Count()-1)/2];

        return middleScore;
    }


    private static void B(){
        var stringList = GetInput();
        var middleScore = GetMiddleScore(stringList);

        Console.WriteLine("10b: " + middleScore);
    }
}