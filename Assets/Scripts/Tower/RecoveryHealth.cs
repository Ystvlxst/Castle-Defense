using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryHealth : MonoBehaviour
{
    [SerializeField] private Tower _tower;
    [SerializeField] private StackPresenter _stackPresenter;
    [SerializeField] private ParticleSystem _healing;

    private void OnEnable() =>
        _stackPresenter.Added += OnAdd;

    private void OnDisable() =>
        _stackPresenter.Added -= OnAdd;

    private void OnAdd(Stackable stackable)
    {
        _tower.AddHealth(_stackPresenter.Count * 10);
        _stackPresenter.RemoveFromStack(stackable);
        _healing.Play();
    }
}
