public static class TwentyTwenty_Two{
    public static void Run(){
        TwoA();
        TwoB();
    }

    private static void TwoA(){
        var stringList = FileHelper.ReadInput("2020/Days/two.txt");
        var entries = stringList.Select(s => new Entry(s)).ToList();

        Console.WriteLine("2020 2A: "+entries.Where(e => e.IsValidPartOne).Count());
    }
    private static void TwoB(){
        var stringList = FileHelper.ReadInput("2020/Days/two.txt");
        var entries = stringList.Select(s => new Entry(s)).ToList();

        Console.WriteLine("2020 2B: "+entries.Where(e => e.IsValidPartTwo).Count());
    }
    

    private class Entry{

        public Entry(string stringInput)
        {
            FirstNumber = int.Parse(stringInput.Substring(0, stringInput.IndexOf('-')));
            SecondNumber = int.Parse(stringInput.Substring(stringInput.IndexOf('-')+1, 2));
            Char = stringInput.ToCharArray()[stringInput.IndexOf(' ')+1];
            Password = stringInput.Substring(stringInput.IndexOf(':')+2);
        }
        public int FirstNumber { get; set; }
        public int SecondNumber { get; set; }
        public char Char { get; set; }
        public string Password { get; set; }

        public bool IsValidPartOne {
            get {
                    var count = Password.ToCharArray().Where(c => c == Char).Count();
                    return count >= FirstNumber && count <= SecondNumber;
                }
        }
        public bool IsValidPartTwo {
            get {
                    return
                        Password.ToCharArray()[FirstNumber-1] == Char && Password.ToCharArray()[SecondNumber-1] != Char ||
                        Password.ToCharArray()[FirstNumber-1] != Char && Password.ToCharArray()[SecondNumber-1] == Char;
                    
                }
        }
    }
}