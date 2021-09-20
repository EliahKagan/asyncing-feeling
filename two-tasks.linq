<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

// Copyright (C) 2020 Eliah Kagan <degeneracypressure@gmail.com>
//
// Permission to use, copy, modify, and/or distribute this software for any
// purpose with or without fee is hereby granted.
//
// THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES
// WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF
// MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY
// SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES
// WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN ACTION
// OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF OR IN
// CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.

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
