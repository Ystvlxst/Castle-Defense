using System.Linq;
using UnityEngine;

public class LaserTargetSelector : TargetSelector
{
    public override ShootTarget SelectTarget()
    {
        Enemy enemy = EnemyContainer.Enemies.ElementAt(Random.Range(0, EnemyContainer.Enemies.Count()));

        return new ShootTarget(enemy.Damageable, enemy.transform);
    }
}