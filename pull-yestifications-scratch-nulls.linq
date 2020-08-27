<Query Kind="Statements">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

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
