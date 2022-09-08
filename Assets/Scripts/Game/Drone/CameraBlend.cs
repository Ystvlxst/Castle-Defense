using UnityEngine;

public class CameraBlend : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void ShowPlayer()
    {
        _animator.SetTrigger(Parameters.ShowPlayer);
    }

    public void ShowDrone()
    {
        _animator.SetTrigger(Parameters.ShowDrone);
    }

    private static class Parameters
    {
        public static readonly string ShowPlayer = nameof(ShowPlayer);
        public static readonly string ShowDrone = nameof(ShowDrone);
    }
}