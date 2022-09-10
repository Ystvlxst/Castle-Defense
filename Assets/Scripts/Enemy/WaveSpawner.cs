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

    private void OnEnable() => 
        _tower.Damaged += OnBaseDamaged;

    private void Start() =>
        StartCoroutine(Spawn());

    private void OnDisable() => 
        _tower.Damaged -= OnBaseDamaged;

    private IEnumerator Spawn()
    {
        int wave = 0;

        while (true)
        {
            wave++;

            if (_decreaseWave)
            {
                wave--;
                _decreaseWave = false;
            }

            wave = Mathf.Clamp(wave, 0, _waves.Count);
            
            for (int i = 0; i < wave; i++) 
                StartCoroutine(Spawn(_waves[i].EnemySpawner));

            yield return new WaitUntil(() => _spawned == 0);
        }
    }

    private IEnumerator Spawn(EnemySpawner enemySpawner)
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

    private void OnBaseDamaged()
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