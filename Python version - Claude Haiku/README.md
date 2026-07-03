# ScholarshipHub Workflow - Python Implementation

This directory contains a Python implementation of the ScholarshipHub declarative workflow.

## Workflow Overview

The original workflow (ScholarshipHub.yaml) is a declarative specification that:

1. **Trigger**: Runs on conversation start
2. **Step 1**: Invokes the "Scholarship-finder-agent" with the user's message
3. **Step 2**: Invokes the "Scholarship-Researcher" agent with output from step 1
4. **Step 3**: Ends the conversation and returns results

## Converted Python Implementation

### File: `scholarship_hub_workflow.py`

This file contains:

- **ScholarshipHubWorkflow class**: Orchestrates the two-agent workflow
  - `__init__()`: Initializes the Foundry project client
  - `execute_workflow()`: Main workflow orchestration method
  - `_invoke_agent()`: Helper to invoke individual agents
  - `handle_conversation_start()`: Implements the OnConversationStart trigger
  
- **main()**: Example usage showing how to trigger and run the workflow

## Setup

### Prerequisites

1. Python 3.10 or higher
2. Azure Foundry project with deployed agents:
   - `Scholarship-finder-agent`
   - `Scholarship-Researcher`
3. Azure credentials with access to the Foundry project

### Installation

```bash
pip install -r requirements.txt
```

### Configuration

Set the environment variable with your Foundry project endpoint:

```bash
export AZURE_AI_PROJECT_ENDPOINT="https://<account>.services.ai.azure.com/api/projects/<project-id>"
```

Or create a `.env` file:

```env
AZURE_AI_PROJECT_ENDPOINT=https://<account>.services.ai.azure.com/api/projects/<project-id>
```

## Usage

### Option 1: Run the Example

```bash
python scholarship_hub_workflow.py
```

This executes the workflow with a sample query about engineering scholarships.

### Option 2: Use as a Module

```python
from scholarship_hub_workflow import ScholarshipHubWorkflow

# Initialize
workflow = ScholarshipHubWorkflow(project_endpoint="<your-endpoint>")

# Execute workflow
result = workflow.handle_conversation_start(
    user_message="Find scholarships for computer science students"
)

print(result)
```

### Option 3: Direct Workflow Execution

```python
result = workflow.execute_workflow(
    user_message="Your query here",
    conversation_id="optional-conversation-id"
)
```

## Conversion Mapping

| YAML Element | Python Implementation |
|--------------|----------------------|
| `trigger.kind: OnConversationStart` | `handle_conversation_start()` method |
| `agent: Scholarship-finder-agent` | First `_invoke_agent()` call |
| `input.messages: System.LastMessage` | `user_message` parameter |
| `output.autoSend: true` | Message passed to next agent |
| `agent: Scholarship-Researcher` | Second `_invoke_agent()` call |
| `kind: EndConversation` | Workflow returns final result |

## Key Features

✅ **Sequential Agent Chaining**: Agents run in order with output passing between them  
✅ **Error Handling**: Graceful error handling and logging throughout  
✅ **Conversation Context**: Maintains conversation ID for multi-turn interactions  
✅ **Async-Ready**: Can be extended with async/await patterns for concurrent invocations  
✅ **Structured Logging**: Comprehensive logging for debugging and monitoring  

## Next Steps

- Deploy the agents to your Foundry project if not already done
- Configure your Foundry project endpoint in the environment
- Run the example or integrate into your application
- Extend the workflow with additional agents or conditional logic as needed

## References

- [Microsoft Agent Framework](https://github.com/microsoft/agent-framework)
- [Azure AI Projects SDK](https://github.com/Azure/azure-sdk-for-python/tree/main/sdk/ai/azure-ai-projects)
- [Foundry Workflow Documentation](https://learn.microsoft.com/en-us/azure/ai-services/agents/)
