using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ZoomOutCutscene : MonoBehaviour
{
    [SerializeField] private GameObject zoomOutCam;
    [SerializeField] private float cutsceneDuration = 6f;

    [SerializeField] private Image blackFadeImage;
    [SerializeField] private float fadeDuration = 1f;

    public static GameObject currentCam;

    private void Start()
    {
        blackFadeImage.color = new Color(blackFadeImage.color.r, blackFadeImage.color.g, blackFadeImage.color.b, 0f);
    }
    public void TriggerZoomOut()
    {
        blackFadeImage.enabled = true;
        blackFadeImage.color = new Color(blackFadeImage.color.r, blackFadeImage.color.g, blackFadeImage.color.b, 0f);

        StartCoroutine(ZoomOutRoutine());
    }

    private IEnumerator ZoomOutRoutine()
    {
        yield return StartCoroutine(FadeIn());

        if (currentCam != null)
            currentCam.SetActive(false);

        zoomOutCam.SetActive(true);

        yield return StartCoroutine(FadeOut());

        yield return new WaitForSeconds(cutsceneDuration);

        yield return StartCoroutine(FadeIn());

        zoomOutCam.SetActive(false);
        if (currentCam != null)
            currentCam.SetActive(true);

        yield return StartCoroutine(FadeOut());
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
