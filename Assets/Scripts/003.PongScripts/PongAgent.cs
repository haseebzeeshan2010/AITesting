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
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
        sensor.AddObservation(targetRigidbody.linearVelocity);

    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        // Debug.Log(actions.DiscreteActions[0]); //For Discrete Actions
        // Debug.Log(actions.ContinuousActions[0]); //For Continuous Actions


        float moveX = actions.ContinuousActions[0];
        // agentRigidbody.AddForce(new Vector3(moveX, 0, 0) * Time.deltaTime * moveSpeed);
        transform.localPosition = new Vector3(moveX * 12.5f, 0f, 0f);
        Debug.Log(moveX);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
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
