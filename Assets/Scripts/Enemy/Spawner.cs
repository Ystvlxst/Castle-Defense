using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Enemy _enemyTemplate;
    [SerializeField] private EnemyTarget[] _enemyTargets;
    [SerializeField] private int _amount;
    [SerializeField] private EnemyContainer _enemyContainer;

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

            while (true)
            {
                foreach (var spawnPoint in _spawnPoints)
                {
                    for (int i = 0; i < _amount; i++)
                    {
                        SpawnEnemy(spawnPoint);
                        yield return new WaitForSeconds(0.1f);
                    }
                }

                yield return new WaitForSeconds(30);
            }
        }
    }

    private void SpawnEnemy(Transform spawnPoint)
    {
        Enemy enemy = Instantiate(_enemyTemplate, spawnPoint.position + Random.insideUnitSphere, spawnPoint.rotation);
        StartCoroutine(InitEnemyDelayed(spawnPoint, enemy));
        _spawned++;
        enemy.Dying += OnEnemyDying;
    }

    private IEnumerator InitEnemyDelayed(Transform spawnPoint, Enemy enemy)
    {
        yield return new WaitForSeconds(1);
        enemy.Init(GetClosestTarget(spawnPoint, SelectTargets()));
        _enemyContainer.Add(enemy);
        InitTarget(enemy);
    }

    private EnemyTarget[] SelectTargets()
    {
        int smallest = _enemyTargets.Select(enemyTarget => enemyTarget.Queue).Prepend(int.MaxValue).Min();

        return _enemyTargets.Where(target => target.Queue == smallest).ToArray();
    }

    private EnemyTarget GetClosestTarget(Transform spawnPoint, EnemyTarget[] enemyTargets)
    {
        float minDistance = Single.MaxValue;
        EnemyTarget closest = null;

        foreach (EnemyTarget target in enemyTargets)
        {
            float distance = Vector3.Distance(spawnPoint.transform.position, target.transform.position);
            
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = target;
            }
        }

        return closest;
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