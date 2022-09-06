using System.Linq;
using UnityEngine;

class FirstTargetAheadShootingSelector : TargetSelector
{
    [SerializeField] private float _aheadOffset = 15f;
    
    public override Vector3 SelectTarget()
    {
        Enemy first = EnemyContainer.Enemies.First();
        Vector3 target = first.transform.position;

        if (first.TryGetComponent(out AttackState attackState) && attackState.enabled == false)
            target += first.transform.forward * _aheadOffset;

        target.z = Mathf.Clamp(target.z, first.Target.transform.position.z, target.z);
            
        return target;
    }
}