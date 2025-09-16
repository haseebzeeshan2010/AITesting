using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class PongAgent : Agent
{
    [SerializeField] private Rigidbody agentRigidbody;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Rigidbody targetRigidbody;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float launchForce = 15f;

    public override void OnEpisodeBegin()
    {
        targetRigidbody.linearVelocity = new Vector3(Random.Range(-launchForce, launchForce), 0, launchForce);
        targetTransform.localPosition = new Vector3(Random.Range(-7f, 7f), 0f, 17.25f);
        transform.localPosition = new Vector3(0f, 0f, 0f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        Vector3 localpos = transform.localPosition;
        sensor.AddObservation(localpos.x);
        sensor.AddObservation(localpos.z);

        Vector3 targetPos = targetTransform.localPosition;
        sensor.AddObservation(targetPos.x);
        sensor.AddObservation(targetPos.z);

        Vector3 targetVelocity = targetRigidbody.linearVelocity;
        sensor.AddObservation(targetVelocity.z);
        sensor.AddObservation(targetVelocity.x);

        Vector3 relativePos = targetPos - localpos;
        sensor.AddObservation(relativePos.x);
        sensor.AddObservation(relativePos.z);


    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // Get the discrete action (0: left, 1: idle, 2: right)
        int discreteAction = actions.DiscreteActions[0];
        // Convert discrete action to movement value: -1, 0, or 1.
        float moveX = discreteAction - 1; 

        agentRigidbody.AddForce(new Vector3(moveX, 0, 0) * Time.deltaTime * moveSpeed);
        // Debug.Log("Discrete Action: " + discreteAction);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        // Use input axis to determine discrete action.
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput < 0)
            discreteActions[0] = 0; // move left
        else if (horizontalInput > 0)
            discreteActions[0] = 2; // move right
        else
            discreteActions[0] = 1; // no movement
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Ball>(out Ball ball))
        {
            Debug.Log("Returned Ball");
            AddReward(2.0f);
        }
    }

    public void Punish()
    {
        AddReward(-1.0f);
        EndEpisode();
        Debug.Log("Punish Called");
    }
}
