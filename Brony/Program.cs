namespace Brony;

public class Program
{
    static async Task Main(string[] args)
    {
        var dapper = new Dapper();

        await dapper.GetByIdAsync(1);
    }
}