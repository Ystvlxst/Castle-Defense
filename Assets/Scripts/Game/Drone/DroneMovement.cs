using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

internal class DroneMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _speedRotate;
    [SerializeField] private MonoBehaviour _inputBehaviour;
    [SerializeField] private Transform _targetBase;
    [SerializeField] private DronePlatform _dronePlatform;
    
    private IInputSource _input;
    private Vector3 _moveVector;

    private void Start()
    {
        _input = (IInputSource) _inputBehaviour;
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        if (_dronePlatform.IsPlayerMover == false)
        {
            _moveVector = (_input.Destination - transform.position).normalized;
            transform.Translate(_moveVector * Time.deltaTime * _speed);
            Quaternion quaternion = Quaternion.LookRotation(new Vector3(_moveVector.x, _moveVector.y, _moveVector.z));
            transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, Time.deltaTime * _speedRotate);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetBase.position, Time.deltaTime * _speed);
        }
    }

    public void SetInput(IInputSource input)
    {
        _input = input;
    }
}