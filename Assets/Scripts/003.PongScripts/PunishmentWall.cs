using UnityEngine;

public class PunishmentWall : MonoBehaviour
{
    [SerializeField] private PongAgent pongAgent;
    [SerializeField] private ScoreText scoreText;
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
        if (other.TryGetComponent<Ball>(out Ball ball2) && scoreText != null)
        {
            scoreText.IncrementOpponentScore();
        }
    }
}
