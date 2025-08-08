using UnityEngine;
using UnityEngine.VFX;
public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;
    [SerializeField] private VisualEffect vfxRenderer;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;


    [SerializeField] private float normalGravity = 2.0f;
    [SerializeField] private float ascendGravity = 1f;
    [SerializeField] private float descendGravity = 4f;

    [SerializeField] private float trampolinePower = 24f;

    [SerializeField] Animator animator;

    public static bool isStunned = false;

    void Start()
    {
        isStunned = false;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = normalGravity;

        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (isStunned) return;

        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && rb.linearVelocity.y > 0f)
        {
            animator.SetBool("isJumping", true);
            if (rb.linearVelocity.y > 0f)
                rb.gravityScale = ascendGravity;
            else
                rb.gravityScale = descendGravity;
        }

        /*if (!IsGrounded())
        {
            if (rb.linearVelocity.y > 0f)
                rb.gravityScale = ascendGravity;  
            else
                rb.gravityScale = descendGravity;  
        }
        else
        {
            rb.gravityScale = normalGravity;
        }
*/
        animator.SetBool("isRunning", Mathf.Abs(horizontal) > 0.1f && IsGrounded());

        vfxRenderer.SetVector3("CollidePos", transform.position);

        Flip();
    }



    private void FixedUpdate()
    {
        if (isStunned) return;

        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cloud"))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, trampolinePower);
        }
    }
    private bool IsGrounded()
    {
        animator.SetBool("isJumping", false);
        //animator.SetBool("isHit", false);
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}