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

    private HashSet<string> connectedPairs = new HashSet<string>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && starInRange != null && ActivateStar.activatedAllStars)
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
                    // Sort and store connection key
                    string pairKey = GetSortedPairKey(selectedStar.starID, currentStar.starID);

                    // Only connect if not already connected
                    if (connectedPairs.Add(pairKey))
                    {
                        Debug.Log($"Connected pair: {pairKey} ({connectedPairs.Count}/{requiredConnections})");

                        GameObject finalLine = Instantiate(connectorPrefab);
                        ConnectorLine line = finalLine.GetComponent<ConnectorLine>();
                        line.SetPoints(selectedStar.transform, currentStar.transform);

                        // Unlock object if enough unique pairs are made
                        if (connectedPairs.Count >= requiredConnections && levelComplete != null)
                        {
                            levelComplete.SetActive(true);
                            Debug.Log(" All star pairs connected! Unlocking object.");
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
