<Query Kind="Statements">
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

const int maxDelay = 256;

var producerDelays = new Random();

async IAsyncEnumerable<int> ProduceAsync(int count)
{
    foreach (var value in Enumerable.Range(0, count)) {
        var delay = producerDelays.Next(maxDelay);
        await Task.Delay(delay);
        yield return value;
    }
}

var consumerDelays = new Random(); // As Random is not thread-safe.

async Task ConsumeAsync(int count)
{
    await foreach (var value in ProduceAsync(count))
        await Task.Delay(consumerDelays.Next(maxDelay));
}

ConsumeAsync(10).Dump();
