using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class CubeAgentRays : Agent
{
    public Transform Target;
    public float speedMultiplier = 0.5f;
    public float rotationMultiplier = 5;
    private bool targetTouched = false;

    public override void OnEpisodeBegin()
    {
        // Reset the agent's position and orientation if it has fallen below the platform
        if (this.transform.localPosition.y < 0)
        {
            this.transform.localPosition = new Vector3(-3, 0.5f, 5);
            this.transform.localRotation = Quaternion.identity;
        }

        targetTouched = false;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Add the agent's position and target position to the observation
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(Target.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Get the continuous actions from the actionBuffers
        Vector3 controlSignal = new Vector3(actionBuffers.ContinuousActions[0], 0, actionBuffers.ContinuousActions[1]);

        // Translate and rotate the agent based on the control signal
        transform.Translate(controlSignal * speedMultiplier);
        transform.Rotate(0.0f, rotationMultiplier * actionBuffers.ContinuousActions[2], 0.0f);

        // Check if the agent collides/touches the target
        if (targetTouched)
        {
            // Reward the agent for touching the target
            SetReward(1.0f);
            EndEpisode();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the agent collides with the target
        if (other.CompareTag("Player"))
        {
            targetTouched = true;
        }
    }

    void Update()
    {
        // Manually trigger the agent's actions and observations
        RequestDecision(); // Request the agent to take an action
        RequestAction(); // Collect the agent's action and apply it
    }
}
