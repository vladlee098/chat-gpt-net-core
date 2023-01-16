///
/// This is almost exact copy from:
/// https://github.com/binarythistle/S06E01---GPT3-API-Client/blob/main/Program.cs
///
/// Additions:
/// 1. Now using retry policies
/// 2. Using 

using System.Text;
using Newtonsoft.Json;

if (args.Length > 0)
{
 
    var completions = new completions()
    {
        model = "text-davinci-001",
        prompt = args[0],
        temperature = 1,
        max_tokens = 100,
    };

    var content = new StringContent("{\"model\": \"text-davinci-001\", \"prompt\": \""+ args[0] +"\",\"temperature\": 1,\"max_tokens\": 100}", 
        Encoding.UTF8, "application/json"); 
 
    Console.WriteLine(">>Hello, World!");
}
else
{
    Console.WriteLine(">>Need to provide a question for bot");
}

static HttpResponse RequestAnswer( string question )
{
        var client = _clientFactory.CreateClient();
        return await _clientPolicy.ExponentialHttpRetry.ExecuteAsync(()=> client.PostAsync("https://api.openai.com/v1/completions"));    
}