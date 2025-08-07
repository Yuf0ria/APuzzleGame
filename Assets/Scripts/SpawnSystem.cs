using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] GameObject enemies;
    [SerializeField] GameObject enemies1;
    [SerializeField] GameObject enemies2;
    [SerializeField] GameObject enemies3;

    private void Start()
    {
        enemies.SetActive(false);
        enemies1.SetActive(false);
        enemies2.SetActive(false);
        enemies3.SetActive(false);
    }

    private void Update()
    {
        if(ActivateStar.enemyCanSpawn == true)
        {
            enemies.SetActive(true);
            enemies1.SetActive(true);
            enemies2.SetActive(true);
            enemies3.SetActive(true);

        }
    }
}
