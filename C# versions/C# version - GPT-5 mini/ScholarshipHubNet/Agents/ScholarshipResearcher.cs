using System;
using System.Threading.Tasks;

namespace ScholarshipHubNet.Agents;

// Represents the Scholarship-Researcher agent from the declarative workflow.
public class ScholarshipResearcher : IAgent
{
    public async Task<string> InvokeAsync(string conversationId, string messages)
    {
        // In a real integration this would call Azure Agent/Foundry or other services.
        await Task.Delay(50);
        Console.WriteLine($"[ScholarshipResearcher] Invoked (ConversationId={conversationId})");
        Console.WriteLine($"[ScholarshipResearcher] Input messages: {messages}");

        // Simulated output
        var output = "Detailed research completed: scholarship A (deadline 2026-08-01), scholarship B (deadline 2026-09-15).";
        Console.WriteLine($"[ScholarshipResearcher] Output: {output}");
        return output;
    }
}
