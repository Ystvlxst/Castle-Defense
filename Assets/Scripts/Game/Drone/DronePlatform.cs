using System.Collections;
using UnityEngine;

public class DronePlatform : Trigger<PlayerMovement>
{
    [SerializeField] private JoystickInput _joystickInput;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private DroneMovement _droneMovement;
    [SerializeField] private CameraBlend _camera;
    private Coroutine _switchControl;
    private bool _exit;

    protected override void OnEnter(PlayerMovement triggered) => 
        _exit = false;

    protected override void OnStay(PlayerMovement triggered)
    {
        if(triggered.IsMoving || _switchControl != null)
            return;

        _joystickInput.SetMovement(_droneMovement);
        _camera.ShowDrone();
        _switchControl = StartCoroutine(SwitchControlBack());
    }

    protected override void OnExit(PlayerMovement triggered) => 
        _exit = true;

    private IEnumerator SwitchControlBack()
    {
        yield return new WaitForSeconds(3f);
        yield return new WaitUntil(() => _joystickInput.Moving == false);
        _joystickInput.SetMovement(_playerMovement);
        _camera.ShowPlayer();
        yield return new WaitUntil(() => _exit);
        _switchControl = null;
    }
}