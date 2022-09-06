using System.Collections.Generic;
using UnityEngine;

public class EnemyContainer : MonoBehaviour
{
    private readonly List<Enemy> _enemies = new List<Enemy>();

    public IEnumerable<Enemy> Enemies => _enemies;

    public void Add(Enemy enemy)
    {
        _enemies.Add(enemy);
    }

    public void Remove(Enemy enemy)
    {
        _enemies.Remove(enemy);
    }
}