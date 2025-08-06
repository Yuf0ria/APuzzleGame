using UnityEngine;

public class SpawnOtherStars : MonoBehaviour
{
    [SerializeField] GameObject starE;
    [SerializeField] GameObject starF;
    [SerializeField] GameObject starG;

    [SerializeField] private Renderer glowRendererE;
    [SerializeField] private Renderer glowRendererF;
    [SerializeField] private Renderer glowRendererG;
    private void Start()
    {
        starE.SetActive(false);
        starF.SetActive(false);
        starG.SetActive(false);
    }

    private void Update()
    {
        if (ActivateStar.activatedAllStars)
        {
            Color glowColor = new Color(1f, 1f, 1f, 1f) * 10f; // white HDR with intensity 10
            glowRendererE.material.SetColor("_GlowColor", glowColor);
            glowRendererF.material.SetColor("_GlowColor", glowColor);
            glowRendererG.material.SetColor("_GlowColor", glowColor);
            starE.SetActive(true);
            starF.SetActive(true);
            starG.SetActive(true);
            Debug.Log("All 4 stars are activated!");
        }
    }
}
