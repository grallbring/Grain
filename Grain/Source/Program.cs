namespace Grain;

public static class Program
{

    static Program()
    {

    }

    public static void Main(string[] arguments)
    {
        using (ProgramGame programGame = new ProgramGame())
        {
            programGame.Run();
        }
    }

}
