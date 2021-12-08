
public static class Day2
{

    public static void Run(){
        var position = GetPosition();
        var advancedPosition = GetAdvancedPosition();

        Console.WriteLine("2a: " + position);
        Console.WriteLine("2b: " + advancedPosition);

    }
    private static List<string> ReadInput()
    {
       return System.IO.File.ReadAllLines("2021/2/input.txt").ToList();
    }

    private static List<int> GetCourse(string course)
    {
        var list = ReadInput().Where(x => x.ToLower().Contains(course.ToLower()));
        return list.Select(i => int.Parse(i.Substring(i.Length-1,1))).ToList();

    }

    private static int GetPosition(){
        var forward = GetCourse("forward").Sum();
        var up = GetCourse("up").Sum();
        var down = GetCourse("down").Sum();

        var result = forward * (down - up);

        return result;
    }

    private static List<Command> GetCommands()
    {
        return ReadInput().Select(c => new Command{Text = c.Substring(0, c.Length-2), Value = int.Parse(c.Substring(c.Length-1,1))}).ToList();
    }

    private static int GetAdvancedPosition(){

        var aim = 0;
        var depth = 0;
        var horizontalPosition = 0;

        var commands = GetCommands();

        foreach(var command in commands)
        {
            switch(command.Text)
            {
                case "forward":
                    horizontalPosition = horizontalPosition + command.Value;
                    depth = depth + aim * command.Value;
                    break;
                case "up":
                    aim = aim - command.Value;
                    break;
                case "down":
                    aim = aim + command.Value;
                    break;
            }
        }

        return horizontalPosition * depth;
    }

    private class Command 
    {
        public string? Text { get; set; }
        public int Value { get; set; }
    }
    
}