using UnityEngine;

public class ActivateStar : MonoBehaviour
{

    public bool isActivated = false;
    private bool playerNearby = false;
    public static bool activatedAllStars = false;
    public static int activatedStarCount = 0;
    public static bool enemyCanSpawn = true;

    [SerializeField] private Renderer glowRenderer; // assign if known

    private Material glowMaterialInstance;

    void Start()
    {

        // If not assigned manually, auto-find the Renderer in child
        if (glowRenderer == null)
        {
            glowRenderer = GetComponentInChildren<Renderer>();
        }

        if (glowRenderer != null)
        {
            // Always create an instance so we don't modify the shared material
            glowMaterialInstance = glowRenderer.material;
        }
        else
        {
            Debug.LogWarning("No glow renderer found on child.");
        }
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.R) && !isActivated)
        {
            isActivated = true;
            activatedStarCount++;
            Debug.Log($"{gameObject.name} activated!");

            if (glowMaterialInstance != null)
            {
                Debug.Log($"Activating glow for {gameObject.name}");
                // Change the color intensity (e.g., increase brightness)
                Color glowColor = new Color(1f, 1f, 1f, 1f) * 10f; // white HDR with intensity 10
                glowMaterialInstance.SetColor("_GlowColor", glowColor);
            }

            if (activatedStarCount == 4)
            {
                activatedAllStars = true;
                Debug.Log("All 4 stars are activated!");
            }

            if (activatedStarCount == 2)
            {
                enemyCanSpawn = true;
                Debug.Log("EnemySpawned!");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}
