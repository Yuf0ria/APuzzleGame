using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] GameObject virtualCam;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            virtualCam.SetActive(true);

            // Register this as the current cam
            ZoomOutCutscene.currentCam = virtualCam;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            virtualCam.SetActive(false);
        }
    }
}
