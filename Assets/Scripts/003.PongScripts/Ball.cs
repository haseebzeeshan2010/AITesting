using System.Text.RegularExpressions;
using Unity.MLAgents.SideChannels;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField] private Rigidbody ballRigidbody;
    [SerializeField] private float bounceMultiplier = 1.1f;

    [SerializeField] private float maxSpeed = 30f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (ballRigidbody.linearVelocity.magnitude > maxSpeed)
        {
            ballRigidbody.linearVelocity = ballRigidbody.linearVelocity.normalized * maxSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PongAgent>(out PongAgent pongAgent))
        {
            ballRigidbody.linearVelocity *= bounceMultiplier;
        }
        if (collision.gameObject.TryGetComponent<BounceWall>(out BounceWall bounceWall) && Mathf.Abs(ballRigidbody.linearVelocity.z) < 10f)
        {
            ballRigidbody.AddForce(new Vector3(0, 0, -5f));
        }

        if (collision.gameObject.TryGetComponent<BounceWall>(out BounceWall bounce))
        {
            ballRigidbody.AddForce(new Vector3(Random.Range(-3f, 3f), 0, 0));
        }
    }
}
