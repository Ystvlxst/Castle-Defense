using UnityEngine;

class PlayerJoystickInput : MonoBehaviour, IInputSource
{
    [SerializeField] private Joystick _joystick;
    
    public Vector3 Destination { get; set; }

    private void Update() => 
        Destination = transform.position + new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y);
}