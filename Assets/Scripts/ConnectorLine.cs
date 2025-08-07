using UnityEngine;

public class ConnectorLine : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;

    private Transform from;
    private Transform to;

    [SerializeField] private float textureRepeatRate = 1.0f;

    void Awake()
    {
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;

        lineRenderer.textureMode = LineTextureMode.Tile;

        if (lineRenderer.material != null && lineRenderer.material.mainTexture != null)
        {
            lineRenderer.material.mainTexture.wrapMode = TextureWrapMode.Repeat;
        }
    }

    void Update()
    {
        if (!lineRenderer.enabled || from == null || to == null) return;

        Vector3 start = from.position;
        Vector3 end = to.position;

        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);

        float distance = Vector3.Distance(start, end);

        if (lineRenderer.material != null)
        {
            lineRenderer.material.mainTextureScale = new Vector2(distance * textureRepeatRate, 1f);
        }
    }

    public void SetPoints(Transform a, Transform b)
    {
        from = a;
        to = b;
        lineRenderer.enabled = true;
    }
}
