using System;
using System.Linq;
using UnityEngine;

class ClosestToLastTargetSelector : TargetSelector
{
    [SerializeField] private float _aheadOffset;
    
    private Enemy _lastSelected;

    public override Vector3 SelectTarget()
    {
        Enemy first = EnemyContainer.Enemies.First();

        if(_lastSelected != null)
        {
            float Selector(Enemy enemy) => Vector3.SqrMagnitude(enemy.transform.position - _lastSelected.transform.position);
            float min = EnemyContainer.Enemies.Min(Selector);
            first = EnemyContainer.Enemies.First(enemy => Math.Abs(Selector(enemy) - min) < 1f);
        }

        _lastSelected = first;
        Vector3 target = first.transform.position;

        if (first.TryGetComponent(out AttackState attackState) && attackState.enabled == false)
            target += first.transform.forward * _aheadOffset;

        if (target.z < first.Target.transform.position.z)
            target.z = first.Target.transform.position.z;

        return target;
    }

    public override Enemy SelectEnemyTarget()
    {
        return null;
    }
}