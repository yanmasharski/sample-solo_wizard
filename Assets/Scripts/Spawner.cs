using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private static HashSet<Enemy> enemies = new HashSet<Enemy>();
    [SerializeField] private Enemy[] enemyPrefab;
    [SerializeField] private int maxEnemies = 10;

    private void OnEnemyKilled(Enemy enemy)
    {
        enemies.Remove(enemy);
        TrySpawnEnemy();
    }

    private void TrySpawnEnemy()
    {
        if (enemies.Count < maxEnemies)
        {
            var enemy = Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)], transform.position, Quaternion.identity);
            enemies.Add(enemy);
            enemy.OnKilled += OnEnemyKilled;
        }
    }

    private void Start()
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            TrySpawnEnemy();
        }
    }


}
