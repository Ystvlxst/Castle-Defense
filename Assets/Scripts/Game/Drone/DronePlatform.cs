using System.Collections;
using UnityEngine;

public class DronePlatform : Trigger<CharacterMovement>
{
    [SerializeField] private JoystickInput _joystickInput;
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private DroneMovement _droneMovement;
    [SerializeField] private DetailCollector _droneCollector;
    [SerializeField] private CameraBlend _camera;
    
    private Coroutine _switchControl;
    private bool _exit;

    protected override void OnEnter(CharacterMovement triggered) => 
        _exit = false;

    protected override void OnStay(CharacterMovement triggered)
    {
        if(triggered.IsMoving || _switchControl != null)
            return;

        _joystickInput.SetMovement(_droneMovement);
        _camera.ShowDrone();
        _droneCollector.enabled = true;
        _switchControl = StartCoroutine(SwitchControlBack());
    }

    protected override void OnExit(CharacterMovement triggered) => 
        _exit = true;

    private IEnumerator SwitchControlBack()
    {
        yield return new WaitForSeconds(3f);
        yield return new WaitUntil(() => _joystickInput.Moving == false);
        //_joystickInput.SetMovement(_playerMovement);
        _camera.ShowPlayer();
        _droneCollector.enabled = false;
        yield return new WaitUntil(() => _exit);
        _switchControl = null;
    }
}