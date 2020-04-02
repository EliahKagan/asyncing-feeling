<Query Kind="Statements">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

const string tvtropesPrefix = "https://tvtropes.org/pmwiki/pmwiki.php/Main/";
const string allthetropesPrefix = "https://allthetropes.org/wiki/";
const string theotheronePrefix = "https://allthetropes.fandom.com/wiki/";

static string SnakeCase(string[] words) => string.Join("_", words);
static string CamelCase(string[] words) => string.Join("", words);

var title = "Badass Decay";

var titleWords = title.Split(default(char[]?),
                             StringSplitOptions.RemoveEmptyEntries);

var client = new HttpClient();

async Task<int?> FindInPage(string url, string term)
{
    var page = await client.GetStringAsync(url);
    var index = page.ToLowerInvariant().IndexOf(term.ToLowerInvariant());
    return index < 0 ? default(int?) : index;
};

var term = "tropes are not bad";

async Task Report(string url, string description)
{
    var summary = await FindInPage(url, term) switch {
        int index => $"found at index {index}",
        null      => "not found"
    };
    
    Console.WriteLine($"Search term \"{term}\" {summary} on {description}.");
};

var tvtropes = Report(tvtropesPrefix + CamelCase(titleWords),
                      "TV Tropes");

var allthetropes = Report(allthetropesPrefix + SnakeCase(titleWords),
                          "All The Tropes");

var theotherone = Report(theotheronePrefix + SnakeCase(titleWords),
                         "All The Tropes (Fandom)");

await tvtropes;
await allthetropes;
await theotherone;
