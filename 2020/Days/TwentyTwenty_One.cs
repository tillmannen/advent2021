public static class TwentyTwenty_One{
    public static void Run(){
        One_A();        
        One_B();
    }

    public static void One_A(){
        var stringList = FileHelper.ReadInput("2020/Days/one.txt");
        var numberList = stringList.Select(s => int.Parse(s)).ToList();

        foreach(var number in numberList){
            foreach(var anotherNumber in numberList){
                if(number + anotherNumber == 2020){
                    Console.WriteLine("2020 1A: "+ number * anotherNumber);
                    return;
                }
            }
        }

    }

        public static void One_B(){
        var stringList = FileHelper.ReadInput("2020/Days/one.txt");
        var numberList = stringList.Select(s => int.Parse(s)).ToList();

        foreach(var number in numberList){
            foreach(var anotherNumber in numberList){
                foreach(var yetAnotherNumber in numberList){
                    if(number + anotherNumber + yetAnotherNumber == 2020){
                        Console.WriteLine("2020 1B: "+ number * anotherNumber * yetAnotherNumber);
                        return;
                    }
                }
            }
        }

    }
}