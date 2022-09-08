using UnityEngine;
using BabyStack.Model;

[RequireComponent(typeof(AIMovement))]
public class PlayerMovement : MonoBehaviour, IMovement, IModificationListener<float>
{
    [SerializeField] private DoctorAnimation _animation;
    [SerializeField] private Transform _playerModel;
    [SerializeField] private float _speed;

    private AIMovement _movement;
    private float _speedRate = 1f;
    private float _flySpeedRate = 1f;

    public bool IsMoving { get; private set; }

    private void Awake()
    {
        _movement = GetComponent<AIMovement>();
    }

    public void Move(Vector3 direction)
    {
        _playerModel.LookAt(_playerModel.position + direction);
        _movement.Move(transform.position + direction);
        _movement.SetSpeed(_speedRate * _speed * direction.magnitude);
        
        _animation.SetSpeed(direction.magnitude);
        IsMoving = true;
    }

    public void Stop()
    {
        if (_movement != null)
            _movement.Move(transform.position);

        _animation.SetSpeed(0);
        IsMoving = false;
    }

    public void OnModificationUpdate(float value)
    {
        _speedRate = value;
    }

    public void SetFlySpeedRate(float rate)
    {
        if (rate <= 0)
            throw new System.ArgumentOutOfRangeException(nameof(rate));

        _flySpeedRate = rate;
    }
}
