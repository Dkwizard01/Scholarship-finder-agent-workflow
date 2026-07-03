using System.Threading.Tasks;

namespace ScholarshipHubNet;

internal class Program
{
    static async Task<int> Main(string[] args)
    {
        // Simulate the OnConversationStart trigger with sample conversation id and message
        var conversationId = System.Guid.NewGuid().ToString();
        var startMessage = args.Length > 0 ? string.Join(' ', args) : "User started conversation";

        var workflow = new Workflow.ScholarshipHubWorkflow();
        await workflow.RunAsync(conversationId, startMessage);

        return 0;
    }
}
