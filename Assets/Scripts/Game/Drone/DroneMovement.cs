using UnityEngine;

internal class DroneMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _speedRotate;
    [SerializeField] private MonoBehaviour _inputBehaviour;
    [SerializeField] private DronePlatform _dronePlatform;
    
    private IInputSource _input;
    private Vector3 _moveVector;

    public bool IsFirstMovementStoped = false;
    private Vector3 _startPosition;

    private void Start()
    {
        _input = (IInputSource) _inputBehaviour;
        _startPosition = transform.position;
    }

    private void Update()
    {
        Move();
    }

    public void SetInput(IInputSource input)
    {
        _input = input;
    }

    private void Move()
    {
        if (_dronePlatform.IsPlayerMover == false)
        {
            _moveVector = (_input.Destination - transform.position).normalized;
            transform.Translate(_moveVector * Time.deltaTime * _speed, Space.World);
            Vector3 lookDirection = new Vector3(_moveVector.x, 0, _moveVector.z);
            
            if(lookDirection == Vector3.zero)
                return;
            
            Quaternion quaternion = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, Time.deltaTime * _speedRotate);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _startPosition, Time.deltaTime * _speed);
            IsFirstMovementStoped = true;
        }
    }
}