using UnityEngine;

public class DieTransition : Transition
{
    [SerializeField] private Health _health;

    private void Update() => 
        NeedTransit = _health.Dead;
}