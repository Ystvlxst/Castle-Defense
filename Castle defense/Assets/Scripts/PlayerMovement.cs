using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash("Speed");
    
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private float _speed = 10;

    private NavMeshAgent _agent;
    private float _speedRate;
    private Coroutine _changeSpeed;
    
    public event UnityAction PositionUpdated;
    
    public NavMeshAgent Agent => _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        _speedRate = 1;
    }

    private void Update()
    {
        var shift = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y);
        _agent.SetDestination(transform.position + shift.normalized);

        _agent.speed = _speed * _speedRate * shift.magnitude;
    }

    public void ChangeSpeed(float speedFactor, float duration)
    {
        if (_changeSpeed != null)
            StopCoroutine(_changeSpeed);

        _changeSpeed = StartCoroutine(ChangeSpeedCoroutine(speedFactor, duration));
    }

    private IEnumerator ChangeSpeedCoroutine(float speedFactor, float duration)
    {
        _speedRate = speedFactor;
        yield return new WaitForSeconds(duration);
        _speedRate = 1;
    }
}
