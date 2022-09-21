using System.Collections.Generic;
using UnityEngine;

internal class DroneMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _speedRotate;
    [SerializeField] private MonoBehaviour _inputBehaviour;
    [SerializeField] private DronePlatform _dronePlatform;
    [SerializeField] private List<Transform> _pathPointsTransforms;
    [SerializeField] private float _autoSpeed;

    private List<Vector3> _pathPoints = new List<Vector3>();

    private IInputSource _input;
    private Vector3 _moveVector;

    public bool IsFirstMovementStoped = false;
    private Vector3 _startPosition;
    private int _currentPathPoint;

    private void Start()
    {
        foreach (Transform transformPoint in _pathPointsTransforms) 
            _pathPoints.Add(transformPoint.position);
        
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
        Quaternion lookRotation = Quaternion.identity;
        
        if (_dronePlatform.IsPlayerMover == false)
        {
            _moveVector = (_input.Destination - transform.position).normalized;
            transform.Translate(_moveVector * Time.deltaTime * _speed, Space.World);
            Vector3 lookDirection = new Vector3(_moveVector.x, 0, _moveVector.z);
            
            if(lookDirection == Vector3.zero)
                return;

            lookRotation = Quaternion.LookRotation(lookDirection);
        }
        else
        {
            IsFirstMovementStoped = true;
            Vector3 pathPoint = _pathPoints[_currentPathPoint];
            lookRotation = Quaternion.LookRotation(pathPoint - transform.position);
            transform.position = Vector3.MoveTowards(transform.position, pathPoint, Time.deltaTime * _autoSpeed);

            if (Vector3.SqrMagnitude(transform.position - pathPoint) < 0.5f)
            {
                _currentPathPoint = _currentPathPoint == _pathPoints.Count - 1 ? 0 : _currentPathPoint + 1;
            }
        }
        
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * _speedRotate);
    }
}