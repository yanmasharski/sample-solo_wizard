using System.Collections.Generic;
using UnityEngine;

// TODO: Implement emenies pool not to instantiate them every time
public class Spawner : MonoBehaviour
{
    private Queue<Spawner> spawners = new Queue<Spawner>();
    private static HashSet<Enemy> enemies = new HashSet<Enemy>();
    [SerializeField] private Enemy[] enemyPrefab;
    [SerializeField] private int maxEnemies = 10;

    private void OnEnemyKilled(Enemy enemy)
    {
        enemies.Remove(enemy);
        var spawner = spawners.Dequeue();
        spawner.TrySpawnEnemy();
        spawners.Enqueue(spawner);
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
        spawners.Enqueue(this);
        TrySpawnEnemy();
    }


}
