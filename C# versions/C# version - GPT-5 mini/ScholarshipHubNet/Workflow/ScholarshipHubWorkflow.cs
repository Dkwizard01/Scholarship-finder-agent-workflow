using System;
using System.Threading.Tasks;
using ScholarshipHubNet.Agents;

namespace ScholarshipHubNet.Workflow;

public class ScholarshipHubWorkflow
{
    // This method mirrors the declarative workflow: OnConversationStart -> Invoke Scholarship-finder-agent -> Invoke Scholarship-Researcher -> EndConversation
    public async Task RunAsync(string conversationId, string initialMessage)
    {
        Console.WriteLine($"[Workflow] Trigger: OnConversationStart (ConversationId={conversationId})");
        Console.WriteLine($"[Workflow] Initial message: {initialMessage}");

        // Invoke first agent: Scholarship-finder-agent
        var finder = new ScholarshipFinderAgent();
        var finderResult = await finder.InvokeAsync(conversationId, initialMessage);

        // Pass the output to the next agent (Scholarship-Researcher)
        var researcher = new ScholarshipResearcher();
        var researcherResult = await researcher.InvokeAsync(conversationId, finderResult);

        Console.WriteLine("[Workflow] EndConversation. Latest message from researcher:");
        Console.WriteLine(researcherResult);
    }
}
