using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ScholarshipHubAgent
{
    public static class ScholarshipHubWorkflow
    {
        public static WorkflowDefinition Create() =>
            new WorkflowDefinition
            {
                Kind = "workflow",
                Name = "ScholarshipHub",
                Description = string.Empty,
                Trigger = new WorkflowTrigger
                {
                    Kind = "OnConversationStart",
                    Id = "trigger_wf",
                    Actions = new List<WorkflowAction>
                    {
                        new InvokeAzureAgentAction
                        {
                            Kind = "InvokeAzureAgent",
                            Id = "agent1",
                            Description = "First agent in sequential workflow",
                            Agent = new WorkflowAgent { Name = "Scholarship-finder-agent" },
                            ConversationId = "=System.ConversationId",
                            Input = new WorkflowInput { Messages = "=System.LastMessage" },
                            Output = new WorkflowOutput
                            {
                                Messages = "Local.LatestMessage",
                                AutoSend = true
                            }
                        },
                        new InvokeAzureAgentAction
                        {
                            Kind = "InvokeAzureAgent",
                            Id = "node-1782411400315",
                            Agent = new WorkflowAgent { Name = "Scholarship-Researcher" },
                            ConversationId = "=System.ConversationId",
                            Input = new WorkflowInput { Messages = string.Empty },
                            Output = new WorkflowOutput
                            {
                                AutoSend = true
                            }
                        },
                        new EndConversationAction
                        {
                            Kind = "EndConversation",
                            Id = "node-1782411521168"
                        }
                    }
                }
            };
    }

    public class WorkflowDefinition
    {
        [JsonPropertyName("kind")]
        public string Kind { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("trigger")]
        public WorkflowTrigger Trigger { get; set; }
    }

    public class WorkflowTrigger
    {
        [JsonPropertyName("kind")]
        public string Kind { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("actions")]
        public List<WorkflowAction> Actions { get; set; }
    }

    [JsonDerivedType(typeof(InvokeAzureAgentAction), typeDiscriminator: "InvokeAzureAgent")]
    [JsonDerivedType(typeof(EndConversationAction), typeDiscriminator: "EndConversation")]
    public abstract class WorkflowAction
    {
        [JsonPropertyName("kind")]
        public string Kind { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }
    }

    public class InvokeAzureAgentAction : WorkflowAction
    {
        [JsonPropertyName("agent")]
        public WorkflowAgent Agent { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("conversationId")]
        public string ConversationId { get; set; }

        [JsonPropertyName("input")]
        public WorkflowInput Input { get; set; }

        [JsonPropertyName("output")]
        public WorkflowOutput Output { get; set; }
    }

    public class EndConversationAction : WorkflowAction
    {
    }

    public class WorkflowAgent
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class WorkflowInput
    {
        [JsonPropertyName("messages")]
        public string Messages { get; set; }
    }

    public class WorkflowOutput
    {
        [JsonPropertyName("messages")]
        public string Messages { get; set; }

        [JsonPropertyName("autoSend")]
        public bool AutoSend { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var workflow = ScholarshipHubWorkflow.Create();
            var json = JsonSerializer.Serialize(workflow, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(json);
        }
    }
}
