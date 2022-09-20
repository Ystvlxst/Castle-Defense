using UnityEngine;

public class RedConveyorAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private StackPresenter _stackPresenter;

    private void OnEnable()
    {
        _stackPresenter.Added += OnAdded;
    }

    private void OnDisable()
    {
        _stackPresenter.Added -= OnAdded;
    }

    private void OnAdded(Stackable stackable)
    {
        _animator.SetTrigger("Add");
    }
}
