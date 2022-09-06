using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private int _amount;
    [SerializeField] private EnemyContainer _enemyContainer;
    [SerializeField] private List<EnemySpawner> _enemySpawners;

    private int _spawned;

    private void Start() =>
        StartCoroutine(Spawn());

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitUntil(() => _spawned == 0);
            
            foreach (var enemySpawner in _enemySpawners)
            {
                var spawnPoint = enemySpawner.GetRandomSpawnPoint();

                for (int i = 0; i < enemySpawner.Amount; i++)
                {
                    Enemy enemy = enemySpawner.SpawnEnemy(spawnPoint);
                    _spawned++;
                    enemy.Dying += OnEnemyDying;
                    StartCoroutine(InitEnemyDelayed(spawnPoint, enemy, enemySpawner));

                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
    }

    private IEnumerator InitEnemyDelayed(Transform spawnPoint, Enemy enemy, EnemySpawner enemySpawner)
    {
        yield return new WaitForSeconds(1);
        enemy.Init(enemySpawner.GetClosestTarget(spawnPoint, enemySpawner.SelectTargets()));
        _enemyContainer.Add(enemy);
        InitTarget(enemy);
    }

    private void InitTarget(Enemy enemy)
    {
        enemy.Target.AddToQueue();
    }

    private void OnEnemyDying(Enemy enemy)
    {
        enemy.Target.RemoveFromQueue();
        _enemyContainer.Remove(enemy);
        _spawned--;
        enemy.Dying -= OnEnemyDying;
    }
}