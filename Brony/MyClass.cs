namespace Brony;

public class MyClass : IDisposable
{
    public int Print()
    {
        Console.WriteLine("Hello World!");

        return 1;
    }
    
    public void Dispose()
    {
        Console.WriteLine("Destroyed...");
    }
}