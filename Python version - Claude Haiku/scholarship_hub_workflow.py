"""
ScholarshipHub Workflow

This module implements the ScholarshipHub workflow that chains two agents:
1. Scholarship-finder-agent: Finds relevant scholarships based on user input
2. Scholarship-Researcher: Researches and provides detailed information about scholarships

The workflow runs on conversation start and processes user messages through both agents sequentially.
"""

import json
import logging
from typing import Optional
from azure.ai.projects import AIProjectClient
from azure.identity import DefaultAzureCredential

# Configure logging
logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)


class ScholarshipHubWorkflow:
    """
    Orchestrates the ScholarshipHub workflow.
    
    Workflow Description:
    - Trigger: OnConversationStart
    - Step 1: Invoke Scholarship-finder-agent with user message
    - Step 2: Invoke Scholarship-Researcher with the output from step 1
    - Step 3: Return final result and end conversation
    """
    
    def __init__(
        self,
        project_endpoint: str,
        credential: Optional[object] = None
    ):
        """
        Initialize the ScholarshipHub workflow.
        
        Args:
            project_endpoint: The Foundry project endpoint URL
            credential: Azure credential object (defaults to DefaultAzureCredential)
        """
        self.project_endpoint = project_endpoint
        self.credential = credential or DefaultAzureCredential()
        self.client = AIProjectClient(
            endpoint=project_endpoint,
            credential=self.credential
        )
    
    def execute_workflow(self, user_message: str, conversation_id: str) -> dict:
        """
        Execute the ScholarshipHub workflow.
        
        Args:
            user_message: The initial user message to process
            conversation_id: The conversation ID for maintaining context
            
        Returns:
            Dictionary containing the workflow results
        """
        logger.info(f"Starting ScholarshipHub workflow for conversation: {conversation_id}")
        
        try:
            # Step 1: Invoke Scholarship-finder-agent
            logger.info("Step 1: Invoking Scholarship-finder-agent")
            finder_result = self._invoke_agent(
                agent_name="Scholarship-finder-agent",
                user_message=user_message,
                conversation_id=conversation_id
            )
            logger.info(f"Scholarship-finder-agent result: {finder_result}")
            
            # Step 2: Invoke Scholarship-Researcher with the output from step 1
            logger.info("Step 2: Invoking Scholarship-Researcher")
            researcher_input = finder_result.get("messages", [])
            if not researcher_input:
                researcher_input = [{"role": "assistant", "content": finder_result.get("content", "")}]
            
            researcher_result = self._invoke_agent(
                agent_name="Scholarship-Researcher",
                user_message=researcher_input,
                conversation_id=conversation_id
            )
            logger.info(f"Scholarship-Researcher result: {researcher_result}")
            
            # Step 3: Prepare final output
            final_result = {
                "status": "completed",
                "conversation_id": conversation_id,
                "workflow_steps": {
                    "scholarship_finder": finder_result,
                    "scholarship_researcher": researcher_result
                },
                "final_messages": researcher_result.get("messages", [])
            }
            
            logger.info("ScholarshipHub workflow completed successfully")
            return final_result
            
        except Exception as e:
            logger.error(f"Error executing workflow: {str(e)}", exc_info=True)
            return {
                "status": "failed",
                "error": str(e),
                "conversation_id": conversation_id
            }
    
    def _invoke_agent(
        self,
        agent_name: str,
        user_message: str | list,
        conversation_id: str
    ) -> dict:
        """
        Invoke an agent and return its response.
        
        Args:
            agent_name: The name of the agent to invoke
            user_message: The message(s) to send to the agent
            conversation_id: The conversation ID for context
            
        Returns:
            Dictionary containing the agent's response
        """
        try:
            # Format messages if needed
            if isinstance(user_message, str):
                messages = [{"role": "user", "content": user_message}]
            else:
                messages = user_message
            
            # Invoke the agent
            response = self.client.agents.invoke(
                agent_name=agent_name,
                conversation_id=conversation_id,
                messages=messages
            )
            
            return {
                "agent_name": agent_name,
                "messages": response.get("messages", []) if response else [],
                "content": response.get("content", "") if response else "",
                "status": "success"
            }
            
        except Exception as e:
            logger.error(f"Error invoking agent {agent_name}: {str(e)}")
            return {
                "agent_name": agent_name,
                "status": "failed",
                "error": str(e)
            }
    
    def handle_conversation_start(
        self,
        user_message: str,
        conversation_id: Optional[str] = None
    ) -> dict:
        """
        Handle the OnConversationStart trigger event.
        
        This method implements the workflow trigger that runs when a conversation starts.
        
        Args:
            user_message: The initial message from the user
            conversation_id: Optional conversation ID (will be auto-generated if not provided)
            
        Returns:
            Dictionary containing the workflow execution results
        """
        import uuid
        if not conversation_id:
            conversation_id = str(uuid.uuid4())
        
        logger.info(f"Conversation started with message: {user_message}")
        return self.execute_workflow(user_message, conversation_id)


def main():
    """
    Example usage of the ScholarshipHub workflow.
    """
    import os
    from datetime import datetime
    
    # Get credentials from environment
    project_endpoint = os.getenv("AZURE_AI_PROJECT_ENDPOINT")
    if not project_endpoint:
        raise ValueError("AZURE_AI_PROJECT_ENDPOINT environment variable not set")
    
    # Initialize the workflow
    workflow = ScholarshipHubWorkflow(project_endpoint=project_endpoint)
    
    # Example: Handle a conversation start event
    user_query = "I'm looking for scholarships for engineering students in the US"
    
    result = workflow.handle_conversation_start(
        user_message=user_query,
        conversation_id=None  # Auto-generate a conversation ID
    )
    
    # Print the results
    print("\n" + "="*60)
    print("ScholarshipHub Workflow Execution Result")
    print("="*60)
    print(json.dumps(result, indent=2))
    print("="*60 + "\n")
    
    return result


if __name__ == "__main__":
    main()
