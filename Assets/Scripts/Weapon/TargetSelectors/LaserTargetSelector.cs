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
            first = EnemyContainer.Enemies.ElementAt(1);

        _lastSelected = first;
        Vector3 target = first.transform.position;

        if (!first.IsDying)
            target += first.transform.forward;

        target.z = Mathf.MoveTowards(target.z, first.Target.transform.position.z, 1);

        return target;
    }
}
