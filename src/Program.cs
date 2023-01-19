///
/// This is almost exact copy from:
/// https://github.com/binarythistle/S06E01---GPT3-API-Client/blob/main/Program.cs
///
/// Additions:
/// 1. Now using retry policies
/// 2. Using 

using System;
using System.Linq;
using OpenAI.GPT3;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;

// if (args.Length == 0)
// {
//     Console.WriteLine(">>Need to provide a question for bot");
//     Console.WriteLine(">>Press ENTER to quit.");
//     Console.ReadLine();
//     return;
// }

var prompt = "Once upon a time, everyone said that the";

var apiKey = Environment.GetEnvironmentVariable("OPEN_AI_API_KEY");
var openAiService = new OpenAIService(new OpenAiOptions()
{
    ApiKey = apiKey ?? string.Empty
});

for( int step = 0; step < 10; step++)
{
    var completionResult = await openAiService.Completions.CreateCompletion(new CompletionCreateRequest()
    {
        Prompt = prompt,
        MaxTokens = 5,
        Temperature = 1,
    }, Models.Davinci);

    if (completionResult.Successful)
    {
        var save = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Blue;

        Console.WriteLine($"Prompt: {prompt}");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"ChatGPT says: {completionResult.Choices.First().Text}");

        Console.ForegroundColor = save;

        prompt = $"{prompt} {completionResult.Choices.First().Text}";
    }
    else
    {
        if (completionResult.Error == null)
        {
            throw new Exception("Unknown Error");
        }
        Console.WriteLine($"{completionResult.Error.Code}: {completionResult.Error.Message}");
    }

}

