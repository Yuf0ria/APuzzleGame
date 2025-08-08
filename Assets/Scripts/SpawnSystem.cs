using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] GameObject enemies;

    private EnemyCloseOpen[] enemy2List;
    private Enemy[] enemy1List;

    private void Start()
    {
        GameObject[] enemy2Objects = GameObject.FindGameObjectsWithTag("enemy2");
        GameObject[] enemy1Objects = GameObject.FindGameObjectsWithTag("enemy1");

        enemy2List = new EnemyCloseOpen[enemy2Objects.Length];
        for (int i = 0; i < enemy2Objects.Length; i++)
        {
            enemy2List[i] = enemy2Objects[i].GetComponent<EnemyCloseOpen>();
            if (enemy2List[i] != null)
                enemy2List[i].HideEnemy();
        }

        enemy1List = new Enemy[enemy1Objects.Length];
        for (int i = 0; i < enemy1Objects.Length; i++)
        {
            enemy1List[i] = enemy1Objects[i].GetComponent<Enemy>();
            if (enemy1List[i] != null)
                enemy1List[i].HideEnemy();
        }

        enemies.SetActive(false);
    }

    private void Update()
    {
        if (ActivateStar.enemyCanSpawn)
        {
            enemies.SetActive(true);

            foreach (var e in enemy2List)
                if (e != null) e.ShowEnemy();

            foreach (var e in enemy1List)
                if (e != null) e.ShowEnemy();
        }
    }
}
