using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class AgentBehavior : Agent
{
    private Transform playerTransform;
    private Transform agentTransform;
    private Vector3 initialAgentPosition;

    public float moveSpeed = 5f;

    public override void Initialize()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        agentTransform = transform;
        initialAgentPosition = agentTransform.position;
    }

    public override void OnEpisodeBegin()
    {
        // Reset the environment for a new episode
        // You can randomize the player and agent positions here if desired

        // Move the agent back to its initial position
        agentTransform.position = initialAgentPosition;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Observe the player's position and agent's position
        sensor.AddObservation(playerTransform.position);
        sensor.AddObservation(agentTransform.position);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // Perform actions based on the input received
        // Example: Move the agent based on actions

        // Example: Calculate distance between agent and player
        float distanceToPlayer = Vector3.Distance(agentTransform.position, playerTransform.position);

        // Move the agent towards the player
        Vector3 moveDirection = (playerTransform.position - agentTransform.position).normalized;
        agentTransform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Reward the agent based on its behavior
        if (distanceToPlayer < 1.0f)
        {
            // Player found, end the episode with a negative reward
            SetReward(2.0f);
            EndEpisode();
        }
    }
}
