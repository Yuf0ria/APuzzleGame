using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] GameObject enemies;

    private void Start()
    {
        enemies.SetActive(false);
    }

    private void Update()
    {
        if(ActivateStar.enemyCanSpawn == true)
        {
            enemies.SetActive(true);

        }
    }
}
