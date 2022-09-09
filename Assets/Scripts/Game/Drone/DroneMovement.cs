using System;
using TMPro;
using UnityEngine;

internal class DroneMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private MonoBehaviour _inputBehaviour;
    
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
    }

    public void Stop()
    {
        
    }

    public void SetInput(IInputSource input)
    {
        _input = input;
    }
}