using System.Collections.Generic;
using UnityEngine;

public class PlayerStarConnector : MonoBehaviour
{
    [SerializeField] private GameObject connectorPrefab;

    [SerializeField] private int requiredConnections = 6;
    [SerializeField] private GameObject levelComplete;

    private Transform starInRange;
    private StarPair selectedStar = null;
    private GameObject tempLineObj;
    public static bool levelIsComplete = false;

    private HashSet<string> connectedPairs = new HashSet<string>();

    void Update()
    {
        //Debug.Log("Activated all stars: " + ActivateStar.activatedAllStars);
        if (Input.GetKeyDown(KeyCode.Q) && starInRange != null && ActivateStar.activatedAllStars == true)
        {
            StarPair currentStar = starInRange.GetComponent<StarPair>();

            if (currentStar == null)
                return;

            if (selectedStar == null)
            {
                tempLineObj = Instantiate(connectorPrefab);
                ConnectorLine line = tempLineObj.GetComponent<ConnectorLine>();
                line.SetPoints(transform, currentStar.transform);
                selectedStar = currentStar;
            }
            else
            {
                if (selectedStar.validPairIDs.Contains(currentStar.starID))
                {
                    string pairKey = GetSortedPairKey(selectedStar.starID, currentStar.starID);

                    if (connectedPairs.Add(pairKey))
                    {
                        Debug.Log($"Connected pair: {pairKey} ({connectedPairs.Count}/{requiredConnections})");

                        GameObject finalLine = Instantiate(connectorPrefab);
                        ConnectorLine line = finalLine.GetComponent<ConnectorLine>();
                        line.SetPoints(selectedStar.transform, currentStar.transform);
                        
                        if (connectedPairs.Count >= requiredConnections && levelComplete != null)
                        {

                            GameObject cutsceneObj = GameObject.FindWithTag("FinalCutsceneCam");
                            cutsceneObj.GetComponent<FinalCutscene>().TriggerFinalCutscene();

                            levelIsComplete = true;

                            if (FinalCutscene.doneFadeOut == true)
                            {
                                levelComplete.SetActive(true);
                            }
                            Debug.Log(" All star pairs connected! Level Complete");
                        }
                    }
                    else
                    {
                        Debug.Log("This pair is already connected.");
                    }
                }
                else
                {
                    Debug.Log("Invalid star pair.");
                }
                if (tempLineObj != null)
                {
                    Destroy(tempLineObj);
                    tempLineObj = null;
                }

                selectedStar = null;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("star"))
            starInRange = other.transform;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("star") && other.transform == starInRange)
            starInRange = null;
    }

    private string GetSortedPairKey(string id1, string id2)
    {
        return string.Compare(id1, id2) < 0 ? $"{id1}-{id2}" : $"{id2}-{id1}";
    }
}
