using UnityEngine;

public class PlayerStarConnector : MonoBehaviour
{
    [SerializeField] private ConnectorLine connector;
    private Transform starInRange;

    void Start()
    {
        connector.SetPlayer(transform);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && starInRange != null)
        {
            connector.ConnectStar(starInRange);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("star"))
        {
            starInRange = other.transform;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("star"))
        {
            if (starInRange == other.transform)
                starInRange = null;
        }
    }
}
