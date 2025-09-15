using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
public class MoveToGoalAgent : Agent
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float moveSpeed = 5f;

    public override void OnEpisodeBegin()
    {
        transform.localPosition = Vector3.zero;
        targetTransform.localPosition = new Vector3(Random.Range(-9f, 9f), 0.5f, Random.Range(-9f, 9f));
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        // Debug.Log(actions.DiscreteActions[0]); //For Discrete Actions
        // Debug.Log(actions.ContinuousActions[0]); //For Continuous Actions


        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        if (other.TryGetComponent<Goal>(out Goal goal))
        {
            Debug.Log("Goal Reached");
            SetReward(1.0f);
            EndEpisode();
        }
        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            Debug.Log("Hit Wall");
            SetReward(-1.0f);
            EndEpisode();
        }

    }
}
