public static class Day3 
{
    public static void Run(){
        var powerConsumption = GetPowerConsumption();
        var lifeSupportRating = GetLifeSupportRating();

        Console.WriteLine("3a: " + powerConsumption);
        Console.WriteLine("3b: " + lifeSupportRating);
    }

    private static int GetGamma(List<char[]> charArrays){
        var gammaString = "";
        for(var i = 0; i < charArrays[0].Length; i++)
        {
            gammaString += charArrays.Select(c => int.Parse(c[i].ToString())).Sum() > charArrays.Count()/2 ? "1" : "0";
        }
        return Convert.ToInt32(gammaString, 2);
    }
    private static int GetEpsilon(List<char[]> charArrays){
        var gammaString = "";
        for(var i = 0; i < charArrays[0].Length; i++)
        {
            gammaString += charArrays.Select(c => int.Parse(c[i].ToString())).Sum() > charArrays.Count()/2 ? "0" : "1";
        }
        return Convert.ToInt32(gammaString, 2);
    }
    private static int GetPowerConsumption(){
        var input = FileHelper.ReadInput("2021/3/input.txt");
        var charInput = input.Select(s => s.ToCharArray()).ToList();
        var gamma = GetGamma(charInput);        
        var epsilon = GetEpsilon(charInput);

        return gamma * epsilon;
    }

    private static List<string> GetBitResult(List<string> binaryArray, int bitIndex, bool selectMostCommon){
        var Ones = binaryArray.Where(b => b.Substring(bitIndex,1)=="1").Count();
        var Zeros = binaryArray.Count - Ones;

        char desiredResult = selectMostCommon ? Ones >= Zeros ? '1' : '0' : Ones < Zeros ? '1' : '0';

        return binaryArray.Where(b => b.ToCharArray()[bitIndex] == desiredResult).ToList();
    }

    private static int GetOxygenGeneratorRating(){
        var binaryArray = FileHelper.ReadInput("2021/3/input.txt");
        var i = 0;
        do{
            binaryArray = GetBitResult(binaryArray,i,true);
            i++;
        }while (binaryArray.Count > 1);

        return Convert.ToInt32(binaryArray[0], 2);
    }

    private static int GetCo2ScrubberRating(){
        var binaryArray = FileHelper.ReadInput("2021/3/input.txt");
        var i = 0;
        do{
            binaryArray = GetBitResult(binaryArray,i,false);
            i++;
        }while (binaryArray.Count > 1);

        return Convert.ToInt32(binaryArray[0], 2);
    }
    private static int GetLifeSupportRating(){

        var ogr = GetOxygenGeneratorRating();
        var co2sr = GetCo2ScrubberRating();

        return ogr * co2sr;
    }
}