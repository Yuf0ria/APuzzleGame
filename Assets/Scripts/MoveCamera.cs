using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] GameObject virtualCam;

    private void OnTriggerEnter2D(Collider2D other)
    {
        virtualCam.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        virtualCam.SetActive(false);
    }

}
