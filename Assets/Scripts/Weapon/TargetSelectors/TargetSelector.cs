using System.Linq;
using UnityEngine;

public abstract class TargetSelector : MonoBehaviour
{
    protected EnemyContainer EnemyContainer;

    public bool HasTarget => EnemyContainer != null && EnemyContainer.Enemies.Any();

    public void Init(EnemyContainer enemyContainer)
    {
        EnemyContainer = enemyContainer;
    }

    public abstract Vector3 SelectTarget();
}