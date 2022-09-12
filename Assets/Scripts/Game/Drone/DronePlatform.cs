using System.Collections;
using UnityEngine;

public class DronePlatform : Trigger<CharacterMovement>
{
    [SerializeField] private PlayerJoystickInput _playerJoystickInput;
    [SerializeField] private PlayerJoystickInput _droneJoystickInput;
    [SerializeField] private NullInput _playerNullInput;
    [SerializeField] private NullInput _droneNullInput;
    [SerializeField] private DroneMovement _droneMovement;
    [SerializeField] private CharacterMovement _playerMovement;
    [SerializeField] private DetailCollector _droneCollector;
    [SerializeField] private CameraBlend _camera;
    
    private Coroutine _switchControl;
    private CharacterMovement _character;

    public bool IsPlayerMover = false;

    protected override void OnEnter(CharacterMovement triggered)
    {
        if(_character == null)
            _character = triggered;

        IsPlayerMover = false;
    }

    protected override void OnStay(CharacterMovement triggered)
    {
        if(triggered.IsMoving || _switchControl != null)
            return;

        _playerMovement.SetInput(_playerNullInput);
        _droneMovement.SetInput(_droneJoystickInput);

        _camera.ShowDrone();
        _droneCollector.enabled = true;
        _switchControl = StartCoroutine(SwitchControlBack());
    }

    protected override void OnExit(CharacterMovement triggered)
    {
        if(triggered == _character)
            _character = null;

        IsPlayerMover = true;
    }

    private IEnumerator SwitchControlBack()
    {
        yield return new WaitForSeconds(3f);
        yield return new WaitUntil(() => _droneJoystickInput.Destination == _droneMovement.transform.position);
        
        _playerMovement.SetInput(_playerJoystickInput);
        _droneMovement.SetInput(_droneNullInput);

        _camera.ShowPlayer();
        _droneCollector.enabled = false;
        yield return new WaitUntil(() => _character == null);
        _switchControl = null;
    }
}