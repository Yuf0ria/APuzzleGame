using Unity.VisualScripting;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("Assign star particle systems in order")]
    [SerializeField] private ParticleSystem[] starParticles;

    [Header("Assign matching Star scripts (same order)")]
    [SerializeField] private ActivateStar[] activateStar;

    [SerializeField] private GameObject constellationBook;

    private int currentStarIndex = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryRippleCurrentStar();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (constellationBook != null)
                constellationBook.SetActive(!constellationBook.activeSelf);
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
                Debug.Log($"Ripple shown on {currentStar.name}, waiting for activation.");
            }
        }
        else
        {
            currentStarIndex++;
            Debug.Log($"Moving to next star. Index: {currentStarIndex}");
        }
    }
}
