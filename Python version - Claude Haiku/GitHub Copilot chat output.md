Optimized tool selection


Completed with input: {
  "language": "Python"
}


Completed with input: {
  "moreIntent": "convert declarative workflow to Python agent code",
  "requiredHost": "Foundry"
}

Read skill [microsoft-foundry](file:///home/danis/.agents/skills/microsoft-foundry/SKILL.md?vscodeLinkType%3Dskill#1-1), lines 1 to 100

Read skill `microsoft-foundry`: [](file:///home/danis/.agents/skills/microsoft-foundry/foundry-agent/create/create-hosted.md#1-1), lines 1 to 200

Now I'll convert your declarative workflow to Python code. This workflow chains two agents sequentially: the Scholarship-finder-agent processes the initial message, then the Scholarship-Researcher agent uses that output. 

Created [](file:///home/danis/azuredev-1f66/scholarship_hub_workflow.py)

Now let me create supporting files: 

Created [](file:///home/danis/azuredev-1f66/requirements.txt)

 

Created [](file:///home/danis/azuredev-1f66/README.md)

Created [](file:///home/danis/azuredev-1f66/.env.example)

Done! I've converted your ScholarshipHub declarative workflow to Python code. Here's what was created:

## Files Generated

1. **scholarship_hub_workflow.py** - Main implementation featuring:
   - `ScholarshipHubWorkflow` class that orchestrates the two-agent workflow
   - `handle_conversation_start()` - Implements the OnConversationStart trigger
   - `execute_workflow()` - Chains agents sequentially (Scholarship-finder-agent → Scholarship-Researcher)
   - Error handling and structured logging

2. **requirements.txt** - Dependencies for running the code

3. **README.md** - Complete documentation including setup, usage, and conversion mapping

4. **.env.example** - Environment configuration template

## Key Conversion Details

| YAML | Python |
|------|--------|
| `OnConversationStart` trigger | `handle_conversation_start()` method |
| Sequential agent invocation | `execute_workflow()` chains agents and passes output between them |
| `System.LastMessage` input | `user_message` parameter |
| `autoSend: true` (auto message passing) | Output from agent 1 fed to agent 2 |

## Quick Start

```bash
pip install -r requirements.txt
export AZURE_AI_PROJECT_ENDPOINT="<your-endpoint>"
python scholarship_hub_workflow.py
```

The Python code maintains the same sequential workflow logic while providing a programmatic interface you can integrate into applications or extend with additional features.

Made changes.