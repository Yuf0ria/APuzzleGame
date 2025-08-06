using UnityEngine;

public class PlayerStarConnector : MonoBehaviour
{
    [SerializeField] private GameObject connectorPrefab;

    private Transform starInRange;
    private StarPair selectedStar = null;
    private GameObject tempLineObj;

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
                    GameObject finalLine = Instantiate(connectorPrefab);
                    ConnectorLine line = finalLine.GetComponent<ConnectorLine>();
                    line.SetPoints(selectedStar.transform, currentStar.transform);

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
}
