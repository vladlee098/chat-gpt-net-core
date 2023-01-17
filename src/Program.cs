///
/// This is almost exact copy from:
/// https://github.com/binarythistle/S06E01---GPT3-API-Client/blob/main/Program.cs
///
/// Additions:
/// 1. Now using retry policies
/// 2. Using 

using OpenAI.GPT3;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;

if (args.Length > 0)
{

    var apiKey = Environment.GetEnvironmentVariable("OPEN_AI_API_KEY");
    var openAiService = new OpenAIService(new OpenAiOptions()
    {
        ApiKey = apiKey ?? string.Empty
    });

    // var content = new StringContent("{\"model\": \"text-davinci-001\", \"prompt\": \""+ args[0] +"\",\"temperature\": 1,\"max_tokens\": 100}", 
    //     Encoding.UTF8, "application/json"); 

    var completionResult = openAiService.Completions.CreateCompletion(new CompletionCreateRequest()
    {
        Prompt = args[0],
        MaxTokens = 5,
        Temperature = 1,
    }, Models.Davinci);

    if (completionResult.IsCompletedSuccessfully)
    {
        Console.WriteLine(completionResult.Result.Choices.FirstOrDefault());
    }
    else
    {
        if (completionResult.Result.Error == null)
        {
            throw new Exception("Unknown Error");
        }
        Console.WriteLine($"{completionResult.Result.Error.Code}: {completionResult.Result.Error.Message}");
    }
}
else
{
    Console.WriteLine(">>Need to provide a question for bot");
}

Console.ReadLine();


// static HttpResponse RequestAnswer( string question )
// {
//         var client = _clientFactory.CreateClient();
//         return await _clientPolicy.ExponentialHttpRetry.ExecuteAsync(()=> client.PostAsync("https://api.openai.com/v1/completions"));    
// }