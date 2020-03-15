using System;
using System.Threading;
using System.Threading.Tasks;
using SharpPad;

namespace AsyncingFeeling {
    internal static class Program {
        private static async Task Main()
        {
            await "Hello, world!".Dump();
            var sum = await FirstNumber() + await SecondNumber();
            await sum.Dump(nameof(sum));
        }

        private static async Task<int> FirstNumber()
        {
            await Task.Run(() => Thread.Sleep(5000));
            return 42;
        }

        private static async Task<int> SecondNumber()
        {
            await Task.Run(() => Thread.Sleep(1000));
            return 76;
        }
    }
}
