using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class SpeedAnimation : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash("Speed");
    
    [SerializeField] private NavMeshAgent _navMeshAgent;

    private Animator _animator;

    private void Awake() => 
        _animator = GetComponent<Animator>();

    private void Update()
    {
        _animator.SetFloat(Speed, _navMeshAgent.velocity.magnitude);
    }
}