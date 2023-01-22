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

#nullable enable

static int Finish(Stopwatch timer)
{
    timer.Stop();
    return (int)timer.Elapsed.TotalMilliseconds;
}

const int maxDelay = 256;

Random producerDelays = new();

async IAsyncEnumerable<(int value, int delay)> ProduceAsync(int count)
{
    foreach (var value in Enumerable.Range(0, count)) {
        var delay = producerDelays.Next(maxDelay);
        await Task.Delay(delay);
        yield return (value, delay);
    }
}

var consumerDelays = new Random(); // As Random is not thread-safe.

async Task<(int totalDelays, int elaspedTime)> ConsumeAsync(int count)
{
    var totalDelays = 0;
    var timer = Stopwatch.StartNew();

    await foreach (var (value, producerDelay) in ProduceAsync(count)) {
        totalDelays += producerDelay;
        var consumerDelay = consumerDelays.Next(maxDelay);
        Console.WriteLine($"Got value {value} with {producerDelay} ms delay."
                        + $" Delaying {consumerDelay} ms more.");
        await Task.Delay(consumerDelay);
        totalDelays += consumerDelay;
    }

    return (totalDelays, Finish(timer));
}

const int count = 24;
const int otherDelay = maxDelay * count / 2;

var consumer = ConsumeAsync(count);
var otherTimer = Stopwatch.StartNew();

Console.WriteLine("** Consuming sequence. "
               + $"Pretending to do {otherDelay} ms of other work.");

await Task.Delay(maxDelay * count / 2);

Console.WriteLine("** Done pretending to do other work for"
               + $" {Finish(otherTimer)} ms.");

var (totalDelays, elapsedTime) = await consumer;

Console.WriteLine($"** Finished with total delay of {totalDelays} ms,"
                + $" elapsed time {elapsedTime} ms.");
