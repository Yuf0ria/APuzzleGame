using UnityEngine;
using System.Collections;

public class EnemyCloseOpen : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private Vector3 smallerScale = new Vector3(0.5f, 0.5f, 1f);
    [SerializeField] private Vector3 originalScale = new Vector3(1f, 1f, 1f);
    [SerializeField] private float scaleDuration = 3f;

    private void Start()
    {
        // Start scaling loop
        StartCoroutine(ScaleLoop());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && gameOverUI != null && PlayerControls.isDetectable == true)
        {
            gameOverUI.SetActive(true);
        }
    }

    IEnumerator ScaleLoop()
    {
        while (true)
        {
            // Shrink
            yield return StartCoroutine(ScaleOverTime(transform.localScale, smallerScale, scaleDuration));

            // Expand
            yield return StartCoroutine(ScaleOverTime(transform.localScale, originalScale, scaleDuration));
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

        transform.localScale = targetScale; // Ensure exact final value
    }
}
