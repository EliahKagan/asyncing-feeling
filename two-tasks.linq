<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

internal static class Program {
    private static void Main()
    {
        var a = Task.Run(() => {
            Console.WriteLine("A starting.");
            Thread.Sleep(5000);
            Console.WriteLine("A finished.");
        });
        
        var b = Task.Run(() => {
            Console.WriteLine("B starting.");
            Thread.Sleep(5000);
            Console.WriteLine("B finished.");
        });
        
        Task.WaitAll(a, b);
        
        Console.WriteLine("All done.");
    }
}