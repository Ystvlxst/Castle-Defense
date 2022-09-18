using System.Linq;
using UnityEngine;

internal abstract class TargetSelectorWithOffset : TargetSelector
{
    [SerializeField] private float _aheadOffset;

    protected Enemy LastSelected { get; private set; }

    public override ShootTarget SelectTarget()
    {
        Enemy firstEnemy = EnemyContainer.Enemies.First();

        firstEnemy = SelectEnemy(firstEnemy);

        LastSelected = firstEnemy;
        Vector3 target = firstEnemy.transform.position;

        if (firstEnemy.TryGetComponent(out AttackState attackState) && attackState.enabled == false)
            target += firstEnemy.transform.forward * _aheadOffset;

        float enemyPositionZ = firstEnemy.Target.transform.position.z;
        
        target.z = Mathf.Clamp(target.z, enemyPositionZ, target.z);

        if (target.z < enemyPositionZ)
            target.z = enemyPositionZ;

        return new ShootTarget(firstEnemy.Damageable, target);
    }

    protected abstract Enemy SelectEnemy(Enemy first);
}