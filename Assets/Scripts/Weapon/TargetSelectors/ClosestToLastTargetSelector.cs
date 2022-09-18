using System;
using System.Linq;
using UnityEngine;

class ClosestToLastTargetSelector : TargetSelectorWithOffset
{
    protected override Enemy SelectEnemy(Enemy first)
    {
        if (LastSelected == null)
            return first;

        float Selector(Enemy enemy) => Vector3.SqrMagnitude(enemy.transform.position - LastSelected.transform.position);
        float min = EnemyContainer.Enemies.Min(Selector);

        return EnemyContainer.Enemies.First(enemy => Math.Abs(Selector(enemy) - min) < 1f);
    }
}