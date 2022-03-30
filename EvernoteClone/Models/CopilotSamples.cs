static int AddTwoNumbers(int a, int b)
{
    return a + b;
}   // end AddTwoNumbers

static public async Task<string> GetWebPageContent(string url)
{
    var client = new HttpClient();
    var response = await client.GetAsync(url);
    var content = await response.Content.ReadAsStringAsync();
    return content;
}   // end GetWebPageContent

static async Task<HtmlDocument> GetHtmlDocument(string url)
{
    var content = await GetWebPageContent(url);
    var doc = new HtmlDocument();
    doc.LoadHtml(content);
    return doc;
}   // end GetHtmlDocument

static async Task<string> BuscarNoGoogle(string termo)
{
    var url = $"https://www.google.com.br/search?q={termo}";
    var doc = await GetHtmlDocument(url);
    var result = doc.DocumentNode.SelectSingleNode("//div[@id='resultStats']");
    return result.InnerText;
}   // end BuscaNoGoogle
 


 static async Task Principal(string args){
    var result = args[0];

    //go to Bing and query for first result of "csharp"
    var url = $"https://www.bing.com/search?q={result}";

    //get the html document
    var doc = await GetHtmlDocument(url);

 }

 public static void HeapSort(int[] arr)
 {
     int n = arr.Length;
     for (int i = n / 2 - 1; i >= 0; i--)
         heapify(arr, n, i);
     for (int i = n - 1; i >= 0; i--)
     {
         int temp = arr[0];
         arr[0] = arr[i];
         arr[i] = temp;
         heapify(arr, i, 0);
     }
 }

 public static void BubbleSort(int[] arr)
 {
     int n = arr.Length;
     for (int i = 0; i < n - 1; i++)
         for (int j = 0; j < n - i - 1; j++)
             if (arr[j] > arr[j + 1])
             {
                 int temp = arr[j];
                 arr[j] = arr[j + 1];
                 arr[j + 1] = temp;
             }
 }  // end BubbleSort

 pulic void djistra(int[,] graph, int s, int d)
 {
     int n = graph.GetLength(0);
     int[] dist = new int[n];
     bool[] visited = new bool[n];
     for (int i = 0; i < n; i++)
         dist[i] = int.MaxValue;
     dist[s] = 0;
     for (int i = 0; i < n - 1; i++)
     {
         int u = minDistance(dist, visited);
         visited[u] = true;
         for (int v = 0; v < n; v++)
             if (!visited[v] && graph[u, v] != 0 && dist[u] != int.MaxValue && dist[u] + graph[u, v] < dist[v])
                 dist[v] = dist[u] + graph[u, v];
     }
 }
}
