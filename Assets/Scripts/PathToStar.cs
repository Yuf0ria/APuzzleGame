using UnityEngine;
using UnityEngine.VFX;
using System.Collections.Generic;

public class PathToStar : MonoBehaviour
{
    [Header("VFX & Player")]
    [Tooltip("The VisualEffect asset which has ClearStart & ClearEnd exposed")]
    [SerializeField] private VisualEffect vfxGraph;
    [Tooltip("Your player Transform")]
    [SerializeField] private Transform player;

    [Header("Stars to Clear (in order)")]
    [Tooltip("List of star Transforms, in the sequence you want to clear toward")]
    [SerializeField] private List<Transform> stars;

    [Tooltip("Key to press to advance the clear beam")]
    [SerializeField] private KeyCode clearKey = KeyCode.Q;

    private int currentStarIndex = 0;

    void Update()
    {
        // On key down, if there's still a star left in the list:
        if (Input.GetKeyDown(clearKey) && currentStarIndex < stars.Count)
        {
            // 1) Send player & star positions into the graph
            vfxGraph.SetVector3("ClearStart", player.position);
            vfxGraph.SetVector3("ClearEnd", stars[currentStarIndex].position);

            // 2) (Optional) Trigger an event if your graph listens for it
            vfxGraph.SendEvent("ClearLine");

            // 3) Move to the next star for the following press
            currentStarIndex++;
        }
    }
}
