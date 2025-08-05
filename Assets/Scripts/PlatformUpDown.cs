using UnityEngine;

public class PlatformUpDown : MonoBehaviour
{
    [SerializeField] private Vector2 localOffsetA;
    [SerializeField] private Vector2 localOffsetB;
    [SerializeField] private float speed = 1f;

    private Vector2 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float rawT = Mathf.PingPong(Time.time * speed, 1f);
        float easedT = Mathf.SmoothStep(0f, 1f, rawT);

        Vector2 localTarget = Vector2.Lerp(localOffsetA, localOffsetB, easedT);
        transform.position = startPos + localTarget;
    }
}
