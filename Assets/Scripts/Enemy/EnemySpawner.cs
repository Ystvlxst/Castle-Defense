using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyTemplate;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private EnemyTarget[] _enemyTargets;

    public Transform GetRandomSpawnPoint() => 
        _spawnPoints[Random.Range(0, _spawnPoints.Count)];

    public Enemy SpawnEnemy(Vector3 position, Quaternion rotation)
    {
        Enemy enemy = Instantiate(_enemyTemplate, position + Random.insideUnitSphere, rotation);
        
        return enemy;
    }

    public EnemyTarget[] SelectTargets()
    {
        int smallest = _enemyTargets.Select(enemyTarget => enemyTarget.Queue).Prepend(int.MaxValue).Min();

        return _enemyTargets.Where(target => target.Queue == smallest).ToArray();
    }

    public EnemyTarget GetClosestTarget(Transform spawnPoint, EnemyTarget[] enemyTargets)
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
}