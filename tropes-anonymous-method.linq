<Query Kind="Statements">
  <Namespace>System.Net.Http</Namespace>
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

const string tvtropesPrefix = "https://tvtropes.org/pmwiki/pmwiki.php/Main/";
const string allthetropesPrefix = "https://allthetropes.org/wiki/";
const string theotheronePrefix = "https://allthetropes.fandom.com/wiki/";

Func<string[], string> snakeCase = delegate(string[] words) {
    return string.Join("_", words);
};

Func<string[], string> camelCase = delegate(string[] words) {
    return string.Join("", words);
};

var title = "Badass Decay";

var titleWords = title.Split(default(char[]?),
                             StringSplitOptions.RemoveEmptyEntries);

var client = new HttpClient();

Func<string, string, Task<int?>> findInPage = async delegate (string url, string term) {
    var page = await client.GetStringAsync(url);
    var index = page.ToLowerInvariant().IndexOf(term.ToLowerInvariant());
    return index < 0 ? default(int?) : index;
};

var term = "tropes are not bad";

Func<string, string, Task> report = async delegate (string url, string description) {
    var summary = await findInPage(url, term) switch {
        int index => $"found at index {index}",
        null      => "not found"
    };

    Console.WriteLine($"Search term \"{term}\" {summary} on {description}.");
};

var tvtropes = report(tvtropesPrefix + camelCase(titleWords),
                      "TV Tropes");

var allthetropes = report(allthetropesPrefix + snakeCase(titleWords),
                          "All The Tropes");

var theotherone = report(theotheronePrefix + snakeCase(titleWords),
                         "All The Tropes (Fandom)");

await tvtropes;
await allthetropes;
await theotherone;
