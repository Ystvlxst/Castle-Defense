using System;
using TMPro;
using UnityEngine;

internal class DroneMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private MonoBehaviour _inputBehaviour;
    [SerializeField] private Transform _targetBase;
    [SerializeField] private DronePlatform _dronePlatform;
    
    private IInputSource _input;

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
        transform.Translate((_input.Destination - transform.position).normalized * Time.deltaTime * _speed);

        if (_dronePlatform.IsPlayerMover == true)
            transform.position = Vector3.MoveTowards(transform.position, _targetBase.position, Time.deltaTime * _speed);
    }

    public void SetInput(IInputSource input)
    {
        _input = input;
    }
}