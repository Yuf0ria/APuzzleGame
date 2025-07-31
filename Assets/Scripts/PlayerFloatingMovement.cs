using UnityEngine;
using UnityEngine.VFX;

public class PlayerFloatingMovement : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] private VisualEffect vfxRenderer;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(moveHorizontal, moveVertical);

        if (move.magnitude > 1f)
            move = move.normalized;

        vfxRenderer.SetVector3("CollidePos", transform.position);


        rb.linearVelocity = move * speed;
    }
}
