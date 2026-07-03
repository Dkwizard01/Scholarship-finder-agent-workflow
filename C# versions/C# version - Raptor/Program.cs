using System;
using System.Text.Json;

namespace ScholarshipHubAgent;

internal static class Program
{
    private static void Main()
    {
        var workflow = ScholarshipHubWorkflow.Create();
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(workflow, options);
        Console.WriteLine(json);
    }
}
