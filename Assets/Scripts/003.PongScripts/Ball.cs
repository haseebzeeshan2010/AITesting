using System.Text.RegularExpressions;
using Unity.MLAgents.SideChannels;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField] private Rigidbody ballRigidbody;
    [SerializeField] private float bounceMultiplier = 1.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

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
    }
}
