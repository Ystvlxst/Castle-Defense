using UnityEngine;

public abstract class TargetSelector : MonoBehaviour
{
    [SerializeField] protected EnemyContainer EnemyContainer;

    public abstract Vector3 SelectTarget();
}