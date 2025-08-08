using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class EnemyCloseOpen : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private Vector3 smallerScale = new Vector3(0.5f, 0.5f, 1f);
    [SerializeField] private Vector3 originalScale = new Vector3(1f, 1f, 1f);
    [SerializeField] private float scaleDuration = 3f;
    [SerializeField] private float hiddenZ = -11.57f;
    [SerializeField] private float visibleZ = 0f;

    private Coroutine scaleCoroutine;
    private Collider2D col;

    public AudioManager audioManager;
    public AudioClip clip;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        StartScaling();
    }

    public void StartScaling()
    {
        if (scaleCoroutine != null)
            StopCoroutine(scaleCoroutine);

        transform.localScale = originalScale;
        scaleCoroutine = StartCoroutine(ScaleLoop());
    }

    public void HideEnemy()
    {
        col.enabled = false;
        transform.position = new Vector3(transform.position.x, transform.position.y, hiddenZ);
    }

    public void ShowEnemy()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, visibleZ);
        col.enabled = true;
        //StartScaling();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && gameOverUI != null && PlayerControls.isDetectable == true)
        {
            Animator playerAnimator = other.GetComponent<Animator>();
            StartCoroutine(StunPlayer(playerAnimator));
            audioManager.soundEffectsAudio[5].PlayOneShot(clip);
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

    IEnumerator ScaleLoop()
    {
        while (true)
        {
            yield return StartCoroutine(ScaleOverTime(originalScale, smallerScale, scaleDuration));
            yield return StartCoroutine(ScaleOverTime(smallerScale, originalScale, scaleDuration));
        }
    }

    IEnumerator ScaleOverTime(Vector3 startScale, Vector3 targetScale, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;
    }
}
