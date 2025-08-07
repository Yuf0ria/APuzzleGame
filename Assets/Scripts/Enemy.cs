using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Vector2 pointA;
    [SerializeField] private Vector2 pointB;
    [SerializeField] private float speed = 1f;


    [SerializeField] private GameObject gameOverUI;

    void Start()
    {
        if (gameOverUI != null)
            gameOverUI.SetActive(false);
    }

    void Update()
    {
        float rawT = Mathf.PingPong(Time.time * speed, 1f);

        float easedT = Mathf.SmoothStep(0f, 1f, rawT);

        Vector2 pos = Vector2.Lerp(pointA, pointB, easedT);
        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && gameOverUI != null && PlayerControls.isDetectable == true)
        {
            Animator playerAnimator = other.GetComponent<Animator>();

            StartCoroutine(StunPlayer(playerAnimator));

            //gameOverUI.SetActive(true);
        }
    }

    IEnumerator StunPlayer(Animator animator)
    {
        PlayerMovement.isStunned = true;
        animator.SetBool("isHit", true);

        yield return new WaitForSeconds(3f);

        animator.SetBool("isHit", false);
        PlayerMovement.isStunned = false;
    }
}
