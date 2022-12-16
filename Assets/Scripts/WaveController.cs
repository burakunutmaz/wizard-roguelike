using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [Header("AreaEnemies")]
    [SerializeField] List<GameObject> enemies = new List<GameObject>();
    [SerializeField] List<Transform> spawnPoints = new List<Transform>();

    public void SpawnAreaWave()
    {
        for (int i=0; i<20; i++) {
            int randomEnemy = Random.Range(0, enemies.Count);
            int randomPoint = Random.Range(0, spawnPoints.Count);
            Instantiate(enemies[randomEnemy], spawnPoints[randomPoint].position, Quaternion.identity);
        }
    }

    public void SpawnBossWave()
    {

    }
}
