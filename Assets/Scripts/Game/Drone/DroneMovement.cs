using System.Collections.Generic;
using UnityEngine;

internal class DroneMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _speedRotate;
    [SerializeField] private MonoBehaviour _inputBehaviour;
    [SerializeField] private DronePlatform _dronePlatform;
    [SerializeField] private Transform _startPoint;

    private IInputSource _input;
    private Vector3 _moveVector;

    public bool IsFirstMovementStoped = false;

    private void Start()
    {
        _input = (IInputSource) _inputBehaviour;
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
            lookRotation = Quaternion.LookRotation(_startPoint.forward);
            transform.position = Vector3.MoveTowards(transform.position, _startPoint.position, Time.deltaTime * _speed);
        }
        
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * _speedRotate);
    }
}