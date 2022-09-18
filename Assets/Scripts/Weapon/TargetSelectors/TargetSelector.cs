using System.Linq;
using UnityEngine;

public abstract class TargetSelector : MonoBehaviour
{
    protected EnemyContainer EnemyContainer;

    public void Init(EnemyContainer enemyContainer)
    {
        EnemyContainer = enemyContainer;
    }

    public bool HasTarget(float maxDistance)
    {
        if (EnemyContainer == null)
            return false;

        bool Condition(Enemy enemy) => Vector3.Distance(enemy.transform.position, transform.position) < maxDistance;

        bool hasTarget = EnemyContainer.Enemies.Any(Condition);
        return hasTarget;
    }

    public abstract ShootTarget SelectTarget();
}