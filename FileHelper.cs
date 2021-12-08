
public static class FileHelper
{
    public static List<string> ReadInput(string path){
       return System.IO.File.ReadAllLines(path).ToList();
    }
}