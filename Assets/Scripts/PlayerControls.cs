using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private GameObject constellationBook;

    private ParticleSystem[] callParticleSystem;
    void Start()
    {
        GameObject[] starObjects = GameObject.FindGameObjectsWithTag("star");

        callParticleSystem = new ParticleSystem[starObjects.Length];

        for (int i = 0; i < starObjects.Length; i++)
        {
            callParticleSystem[i] = starObjects[i].GetComponent<ParticleSystem>();

            if (callParticleSystem[i] == null)
            {
                Debug.LogWarning($"{starObjects[i].name} does not have a ParticleSystem component!");
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (ParticleSystem ps in callParticleSystem)
            {
                if (ps != null)
                {
                    ps.Play();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (constellationBook != null)
                constellationBook.SetActive(!constellationBook.activeSelf);
        }
    }
}
