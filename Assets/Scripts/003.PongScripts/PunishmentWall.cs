using UnityEngine;

public class PunishmentWall : MonoBehaviour
{
    [SerializeField] private PongAgent pongAgent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Ball>(out Ball ball))
        {
            pongAgent.Punish();
        }
    }
}
