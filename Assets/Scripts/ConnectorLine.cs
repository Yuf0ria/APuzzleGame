using UnityEngine;

public class ConnectorLine : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;

    private Transform from;
    private Transform to;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (!lineRenderer.enabled || from == null || to == null) return;

        lineRenderer.SetPosition(0, from.position);
        lineRenderer.SetPosition(1, to.position);
    }

    public void SetPoints(Transform a, Transform b)
    {
        from = a;
        to = b;
        lineRenderer.enabled = true;
    }
}
