# ScholarshipHubNet

This small .NET console app is a converted representation of the declarative workflow `ScholarshipHub.yaml`.

It simulates the workflow:
- Trigger: `OnConversationStart`
- Invoke `Scholarship-finder-agent` (simulated)
- Invoke `Scholarship-Researcher` (simulated)
- EndConversation

Build and run:

```bash
dotnet build ScholarshipHubNet
dotnet run --project ScholarshipHubNet -- "Optional start message"
```

The agents are simple placeholders. Replace calls to the agents with real Foundry/Agent client calls when integrating.
