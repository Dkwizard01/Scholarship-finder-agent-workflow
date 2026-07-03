using System.Threading.Tasks;

namespace ScholarshipHubNet.Agents;

public interface IAgent
{
    Task<string> InvokeAsync(string conversationId, string messages);
}
