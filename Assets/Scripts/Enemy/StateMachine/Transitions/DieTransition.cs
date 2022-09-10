using UnityEngine;

public class DieTransition : Transition
{
    [SerializeField] private Health _health;

    protected override void Enable() => 
        _health.Died += OnDied;

    private void OnDisable() => 
        _health.Died -= OnDied;

    private void OnDied(Health health) => 
        NeedTransit = true;
}