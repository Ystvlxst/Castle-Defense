using System.Linq;
using UnityEngine;

public abstract class TargetSelector : MonoBehaviour
{
    [SerializeField] protected EnemyContainer EnemyContainer;
    
    public bool HasTarget => EnemyContainer.Enemies.Any();

    public abstract Vector3 SelectTarget();
}