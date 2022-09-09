using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LaserTargetSelector : TargetSelector
{
    private Enemy _lastSelected;

    public override Vector3 SelectTarget()
    {
        Enemy first = EnemyContainer.Enemies.First();

        if (first == _lastSelected && EnemyContainer.Enemies.Count() > 1)
            first = EnemyContainer.Enemies.ElementAt(Random.Range(0, EnemyContainer.Enemies.Count()));

        _lastSelected = first;
        Vector3 target = first.transform.position;

        if (!first.IsDying)
            target += first.transform.forward;

        target.z = Mathf.Clamp(target.z, first.Target.transform.position.z, target.z);

        return target;
    }
}
