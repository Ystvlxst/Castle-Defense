using System;
using UnityEngine;

public class JoystickInput : MonoBehaviour
{
    [RequireInterface(typeof(IMovement))]
    [SerializeField] private MonoBehaviour _startMovement;
    [SerializeField] private Joystick _joystick;

    private IMovement _movement;

    public bool LastFrameMoving { get; private set; }

    public bool Moving => _joystick.Direction != Vector2.zero;
    public bool StoppedMoving => !Moving && LastFrameMoving;
    
    public event Action Moved;

    private void Awake()
    {
        Input.multiTouchEnabled = false;
        _movement = (IMovement) _startMovement;
    }

    private void OnDisable()
    {
        _movement?.Stop();
    }

    public void Update()
    {
        if (!Moving)
        {
            _movement.Stop();
            return;
        }

        Vector3 rawDirection = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y);
        //_movement.Move(rawDirection);

        Moved?.Invoke();
    }

    public void SetMovement(IMovement movement)
    {
        _movement = movement;
    }

    private void LateUpdate()
    {
        LastFrameMoving = Moving;
    }
}