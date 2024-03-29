﻿// Copyright (C) 2020 Eliah Kagan <degeneracypressure@gmail.com>
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
