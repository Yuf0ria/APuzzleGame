using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FinalCutscene : MonoBehaviour
{
    [SerializeField] private GameObject finalCutsceneCam;
    [SerializeField] private float cutsceneDuration = 6f;

    [SerializeField] private Image blackFadeImage;
    [SerializeField] private float fadeDuration = 1f;

    public static bool doneFadeOut = false;

    private void Start()
    {
        doneFadeOut = false;
        blackFadeImage.color = new Color(blackFadeImage.color.r, blackFadeImage.color.g, blackFadeImage.color.b, 0f);
    }

    public void TriggerFinalCutscene()
    {
        blackFadeImage.enabled = true;

        StartCoroutine(CameraRoutine());
    }

    private IEnumerator CameraRoutine()
    {
        yield return StartCoroutine(FadeIn());

        if (ZoomOutCutscene.currentCam != null)
            ZoomOutCutscene.currentCam.SetActive(false);

        finalCutsceneCam.SetActive(true);

        yield return StartCoroutine(FadeOut());

        yield return new WaitForSeconds(cutsceneDuration);

        yield return StartCoroutine(FadeIn());

        doneFadeOut = true;
    }

    IEnumerator FadeIn()
    {
        float timer = 0f;
        Color color = blackFadeImage.color;
        while (timer < fadeDuration)
        {
            float t = timer / fadeDuration;
            blackFadeImage.color = new Color(color.r, color.g, color.b, Mathf.Lerp(0f, 1f, t));
            timer += Time.unscaledDeltaTime;
            yield return null;
        }
        blackFadeImage.color = new Color(color.r, color.g, color.b, 1f);
    }

    IEnumerator FadeOut()
    {
        float timer = 0f;
        Color color = blackFadeImage.color;
        while (timer < fadeDuration)
        {
            float t = timer / fadeDuration;
            blackFadeImage.color = new Color(color.r, color.g, color.b, Mathf.Lerp(1f, 0f, t));
            timer += Time.unscaledDeltaTime;
            yield return null;
        }
        blackFadeImage.color = new Color(color.r, color.g, color.b, 0f);
    }
}
