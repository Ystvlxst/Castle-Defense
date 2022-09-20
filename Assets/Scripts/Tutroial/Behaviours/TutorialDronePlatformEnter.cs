using System;
using UnityEngine;

public class TutorialDronePlatformEnter : MonoBehaviour, ITutorialStepCondition, ITutorialAnalyticsEventSource, ITutorialObjectEventSource
{
    [SerializeField] private DronePlatform _platformTrigger;

    public event Action<string> EventSended;

    [field: SerializeField] public Transform Point { get; private set; }
    public bool Completed { get; private set; }
    public ITutorialStepCondition Condition => this;

    public void Enable()
    {
        _platformTrigger.Enter += OnPlatformTriggerEnter;
    }

    public void Disable()
    {
        _platformTrigger.Enter -= OnPlatformTriggerEnter;
    }

    private void OnPlatformTriggerEnter(CharacterMovement _)
    {
        Completed = true;
        EventSended?.Invoke("enter_drone_control_zone");   
    }
}
