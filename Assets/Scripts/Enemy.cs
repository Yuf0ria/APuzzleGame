using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Vector2 pointA;
    [SerializeField] private Vector2 pointB;
    [SerializeField] private float speed = 1f;

    [SerializeField] private GameObject gameOverUI;

    private float hiddenZ = -11.57f;
    private float visibleZ = 0f;
    private bool isHidden = false;

    void OnEnable()
    {
        if (gameOverUI != null)
            gameOverUI.SetActive(false);
    }

    void Update()
    {
        float rawT = Mathf.PingPong(Time.time * speed, 1f);
        float easedT = Mathf.SmoothStep(0f, 1f, rawT);
        Vector2 pos = Vector2.Lerp(pointA, pointB, easedT);

        float zValue = isHidden ? hiddenZ : visibleZ;
        transform.position = new Vector3(pos.x, pos.y, zValue);
    }

    public void HideEnemy()
    {
        isHidden = true;
    }

    public void ShowEnemy()
    {
        isHidden = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && gameOverUI != null && PlayerControls.isDetectable)
        {
            Animator playerAnimator = other.GetComponent<Animator>();
            StartCoroutine(StunPlayer(playerAnimator));
        }
    }

    IEnumerator StunPlayer(Animator animator)
    {
        PlayerMovement.isStunned = true;
        animator.SetBool("isHit", true);

        yield return new WaitForSeconds(3f);

        animator.SetBool("isHit", false);
        PlayerMovement.isStunned = false;

        GameObject playerGameObject = GameObject.FindWithTag("Player");
        if (playerGameObject != null)
        {
            PlayerControls player = playerGameObject.GetComponent<PlayerControls>();

            if (player != null)
            {
                int lastActivatedIndex = -1;
                for (int i = player.activateStar.Length - 1; i >= 0; i--)
                {
                    if (player.activateStar[i].isActivated)
                    {
                        lastActivatedIndex = i;
                        break;
                    }
                }

                if (lastActivatedIndex != -1)
                {
                    Vector3 lastStarPos = player.activateStar[lastActivatedIndex].transform.position;
                    playerGameObject.transform.position = lastStarPos;
                }
                else
                {
                    Debug.Log("No activated star found to respawn the player.");
                }
            }
        }
    }
}
