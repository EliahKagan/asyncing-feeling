<Query Kind="Statements">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

const string tvtropesPrefix = "https://tvtropes.org/pmwiki/pmwiki.php/Main/";
const string allthetropesPrefix = "https://allthetropes.org/wiki/";
const string theotheronePrefix = "https://allthetropes.fandom.com/wiki/";

Func<string[], string> snakeCase = words => string.Join("_", words);
Func<string[], string> camelCase = words => string.Join("", words);

var title = "Badass Decay";

var titleWords = title.Split(default(char[]?),
                             StringSplitOptions.RemoveEmptyEntries);

var client = new HttpClient();

Func<string, string, Task<int?>> findInPage = async (url, term) => {
    var page = await client.GetStringAsync(url);
    var index = page.ToLowerInvariant().IndexOf(term.ToLowerInvariant());
    return index < 0 ? default(int?) : index;
};

var term = "tropes are not bad";

Func<string, string, Task> report = async (url, description) => {
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
