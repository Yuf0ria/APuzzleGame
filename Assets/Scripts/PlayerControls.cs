using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerControls : MonoBehaviour
{
    [Header("Assign star particle systems in order")]
    [SerializeField] private ParticleSystem[] starParticles;

    [Header("Assign matching Star scripts (same order)")]
    [SerializeField] private ActivateStar[] activateStar;

    [SerializeField] private GameObject constellationBook;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float hideDuration = 3f;

    public static bool isDetectable = true;

    private Coroutine hideCoroutine;

    private int currentStarIndex = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryRippleCurrentStar();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (hideCoroutine == null)
            {
                hideCoroutine = StartCoroutine(HideFromEnemies());
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (constellationBook != null)
            {
                bool isNowOpen = !constellationBook.activeSelf;
                constellationBook.SetActive(isNowOpen);

                Time.timeScale = isNowOpen ? 0f : 1f;
            }

        }
    }

    void TryRippleCurrentStar()
    {
        if (currentStarIndex >= starParticles.Length || currentStarIndex >= activateStar.Length)
        {
            Debug.Log("All stars processed.");
            return;
        }

        ActivateStar currentStar = activateStar[currentStarIndex];

        if (!currentStar.isActivated)
        {
            if (starParticles[currentStarIndex] != null)
            {
                starParticles[currentStarIndex].Play();
            }
        }
        else
        {
            currentStarIndex++;
            Debug.Log($"Moving to next star. Index: {currentStarIndex}");
        }
    }

    IEnumerator HideFromEnemies()
    {
        // Step 1: Make player semi-transparent and undetectable
        isDetectable = false;

        Color originalColor = spriteRenderer.color;
        Color hiddenColor = originalColor;
        hiddenColor.a = 0.1f;
        spriteRenderer.color = hiddenColor;

        // Step 2: Wait for 3 seconds
        yield return new WaitForSeconds(hideDuration);

        // Step 3: Restore visibility and detectability
        spriteRenderer.color = originalColor;
        isDetectable = true;
        hideCoroutine = null;
    }
}
