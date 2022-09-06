using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Enemy _enemyTemplate;
    [SerializeField] private EnemyTarget _enemyTarget;

    private int _currentWaveNumber = 0;
    private float _timeAfterLastSpawn;
    private int _spawned;

    public event UnityAction AllEnemySpawned;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitUntil(() => _spawned == 0);

            Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];

            for (int i = 0; i < 10; i++)
            {
                SpawnEnemy(spawnPoint);
            }
            
        }
    }

    private void SpawnEnemy(Transform spawnPoint)
    {
        Enemy enemy = Instantiate(_enemyTemplate, spawnPoint.position, spawnPoint.rotation);
        enemy.Init(_enemyTarget);
        InitTarget(enemy);
        _spawned++;
        enemy.Dying += OnEnemyDying;
    }
    private void InitTarget(Enemy enemy)
    {
        if (enemy.Target.FollowingEnemy == null)
        {
            enemy.Target.Follow(enemy);
            return;
        }

        InitLastEnemyInQueueToTarget(enemy.Target.FollowingEnemy, enemy);
    }

    private void InitLastEnemyInQueueToTarget(Enemy enemy, Enemy spawnedEnemy)
    {
        if (enemy.FollowingEnemy == null)
        {
            enemy.Follow(spawnedEnemy);
            return;
        }

        InitLastEnemyInQueueToTarget(enemy.FollowingEnemy, spawnedEnemy);
    }
    private void OnEnemyDying(Enemy enemy)
    {
        _spawned--;
        enemy.Dying -= OnEnemyDying;
    }
}