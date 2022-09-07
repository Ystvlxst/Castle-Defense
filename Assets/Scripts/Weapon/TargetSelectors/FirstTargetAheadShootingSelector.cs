using System.Linq;
using UnityEngine;

class FirstTargetAheadShootingSelector : TargetSelector
{
    [SerializeField] private float _aheadOffset = 15f;
    
    private Enemy _lastSelected;

    public override Vector3 SelectTarget()
    {
        Enemy first = EnemyContainer.Enemies.First();

        if (first == _lastSelected && EnemyContainer.Enemies.Count() > 1)
            first = EnemyContainer.Enemies.ElementAt(1);
        
        _lastSelected = first;
        Vector3 target = first.transform.position;

        if (first.TryGetComponent(out AttackState attackState) && attackState.enabled == false)
            target += first.transform.forward * _aheadOffset;

        target.z = Mathf.Clamp(target.z, first.Target.transform.position.z, target.z);
            
        return target;
    }
}