using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private EnemyContainer _enemyContainer;
    [SerializeField] private List<SpawnWave> _waves;
    [SerializeField] private Tower _tower;

    private int _spawned;
    private bool _decreaseWave;
    private int _wave = 0;

    private void OnEnable() =>
        _tower.Damaged += OnBaseDamaged;

    private void Start() =>
        StartCoroutine(Spawn());

    private void OnDisable() =>
        _tower.Damaged -= OnBaseDamaged;

    private IEnumerator Spawn()
    {
        _wave = 0;

        while (true)
        {
            _wave++;

            if (_decreaseWave)
            {
                _wave--;
                _decreaseWave = false;
            }

            _wave = Mathf.Clamp(_wave, 0, _waves.Count);

            for (int i = 0; i < _wave; i++)
                StartCoroutine(Spawn(_waves[i]));

            yield return new WaitUntil(() => _spawned == 0);
        }
    }

    private IEnumerator Spawn(SpawnWave enemySpawner)
    {
        EnemySpawner spawner = enemySpawner.EnemySpawner;

        for (int i = 0; i < enemySpawner.Amount; i++)
        {
            Transform spawnPoint = spawner.GetRandomSpawnPoint();

            Enemy enemy = spawner.SpawnEnemy(spawnPoint);
            _spawned++;
            enemy.Dying += OnEnemyDying;
            StartCoroutine(InitEnemyDelayed(spawnPoint, enemy, spawner));

            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnBaseDamaged(int i)
    {
        _decreaseWave = true;
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

[Serializable]
internal class SpawnWave
{
    [field: SerializeField] public EnemySpawner EnemySpawner { get; private set; }
    [field: SerializeField] public int Amount { get; private set; }
}