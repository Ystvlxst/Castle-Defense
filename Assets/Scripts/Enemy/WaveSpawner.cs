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
    private int _damage;
    private int _damageToDecreaseWave = 50;

    private void OnEnable() =>
        _tower.Damaged += OnBaseDamaged;

    private void Start() => 
        StartCoroutine(SpawnStartWaves());

    private IEnumerator SpawnStartWaves()
    {
        StartCoroutine(Spawn(_waves[0], 0, Vector3.back * 100f));
        yield return new WaitForSeconds(4f);
        StartCoroutine(Spawn(_waves[1], 0, Vector3.zero));
        
        _wave = 2;
        StartCoroutine(Spawn());
    }

    private void OnDisable() =>
        _tower.Damaged -= OnBaseDamaged;

    private IEnumerator Spawn()
    {
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
                StartCoroutine(Spawn(_waves[i], 0.1f, Vector3.zero));

            _tower.ReestablishHealth();

            yield return new WaitUntil(() => _spawned == 0);
        }
    }

    private IEnumerator Spawn(SpawnWave enemySpawner, float SpawnDelay, Vector3 spawnOffset)
    {
        EnemySpawner spawner = enemySpawner.EnemySpawner;

        for (int i = 0; i < enemySpawner.Amount; i++)
        {
            Transform spawnPoint = spawner.GetRandomSpawnPoint();

            Enemy enemy = spawner.SpawnEnemy(spawnPoint.position + spawnOffset, spawnPoint.rotation);
            _spawned++;
            enemy.Dying += OnEnemyDying;
            enemy.Init(spawner.GetClosestTarget(spawnPoint, spawner.SelectTargets()));
            _enemyContainer.Add(enemy);
            InitTarget(enemy);

            yield return new WaitForSeconds(SpawnDelay);
        }
    }

    private void OnBaseDamaged(int i)
    {
        _damage += i;

        if (_damage > _damageToDecreaseWave)
        {
            _damage = 0;
            _decreaseWave = true;
        }
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