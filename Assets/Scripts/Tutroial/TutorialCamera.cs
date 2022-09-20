using UnityEngine;
using Cinemachine;

public class TutorialCamera : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    [Space(10)]
    [SerializeField] private InputSwither _inputSwither;

    
    public void Show(string trigger)
    {
        if (trigger == CameraAnimatorParameters.ShowPlayer)
            _inputSwither.Enable();
        else
            _inputSwither.Disable();

        ResetAllTrigger();
        _animator.SetTrigger(trigger);
    }

    private void ResetAllTrigger()
    {
        _animator.ResetTrigger(CameraAnimatorParameters.ShowPlayer);
        _animator.ResetTrigger(CameraAnimatorParameters.ShowConveyor);
        
    }
}
