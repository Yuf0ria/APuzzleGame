using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private Vector2 localOffsetA;
    [SerializeField] private Vector2 localOffsetB;
    [SerializeField] private float speed = 1f;
    private Vector2 startPos;
    void Start()
    {
        startPos = door.transform.position;
    }
    private bool isTriggered = false;
    private float lerpTime = 0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        isTriggered = true;
    }


    void Update()
    {
        if (isTriggered)
        {
            lerpTime += Time.deltaTime * speed;
            Vector2 targetPos = Vector2.Lerp(localOffsetA, localOffsetB, Mathf.Clamp01(lerpTime));
            door.transform.localPosition = targetPos;
        }
    }
}
