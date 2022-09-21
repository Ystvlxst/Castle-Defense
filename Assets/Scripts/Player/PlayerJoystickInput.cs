using System;
using UnityEngine;

class PlayerJoystickInput : MonoBehaviour, IInputSource
{
    [SerializeField] private Joystick _joystick;

    public Vector3 Destination { get; set; }

    private void Update()
    {
        Vector3 direction = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y).normalized;
        Destination = transform.position + direction;
        
        if(Moving)
            Moved?.Invoke();
    }

    public bool StoppedMoving => false;
    public bool Moving => _joystick.Direction != Vector2.zero;
    public event Action Moved;
}