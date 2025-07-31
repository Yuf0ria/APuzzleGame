using UnityEngine;

public class ConnectorLine : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    private Transform lastStar; 
    private Transform currentStar;
    private Transform player;

    private bool isConnectedToPlayer = true;

    void Awake()
    {
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
    }

    public void SetPlayer(Transform playerTransform)
    {
        player = playerTransform;
    }

    void Update()
    {
        if (!lineRenderer.enabled) return;

        if (isConnectedToPlayer)
        {
            lineRenderer.SetPosition(0, player.position);
            lineRenderer.SetPosition(1, currentStar.position);
        }
        else
        {
            lineRenderer.SetPosition(0, lastStar.position);
            lineRenderer.SetPosition(1, currentStar.position);
        }
    }

    public void ConnectStar(Transform newStar)
    {
        lineRenderer.enabled = true;

        if (currentStar == null)
        {
            currentStar = newStar;
            isConnectedToPlayer = true;
        }
        else
        {
            if (isConnectedToPlayer)
            {
                lastStar = currentStar;
                currentStar = newStar;
                isConnectedToPlayer = false;
            }
            else
            {
                currentStar = newStar;
                isConnectedToPlayer = true;
            }
        }
    }
}
