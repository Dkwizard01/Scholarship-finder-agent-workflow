using System;
using System.Threading.Tasks;

namespace ScholarshipHubNet.Agents;

// Represents the Scholarship-finder-agent from the declarative workflow.
public class ScholarshipFinderAgent : IAgent
{
    public async Task<string> InvokeAsync(string conversationId, string messages)
    {
        // In a real integration this would call Azure Agent/Foundry or other services.
        await Task.Delay(50);
        Console.WriteLine($"[ScholarshipFinderAgent] Invoked (ConversationId={conversationId})");
        Console.WriteLine($"[ScholarshipFinderAgent] Input messages: {messages}");

        // Simulated output
        var output = "Found 3 matching scholarships; summarized results.";
        Console.WriteLine($"[ScholarshipFinderAgent] Output: {output}");
        return output;
    }
}
