using System;
using UnityEngine;

public class TutorialRemoveFromStack : MonoBehaviour, ITutorialAnalyticsEventSource, ITutorialObjectEventSource
{
    [SerializeField] private StackPresenter _stackPresenter;
    [SerializeField] private string _eventName;
    [SerializeField] private int _targetAmount = 5;

    private TutorialCondition _removeFromStackCondition;
    private int _count;

    public event Action<string> EventSended;

    [field: SerializeField] public Transform Point { get; private set; }
    
    public ITutorialStepCondition RemoveFromStackCondition => _removeFromStackCondition;

    private void Awake()
    {
        _removeFromStackCondition = new TutorialCondition();
    }

    public void Enable()
    {
        _stackPresenter.Removed += OnTookItem;
    }

    public void Disable()
    {
        _stackPresenter.Removed -= OnTookItem;
    }

    private void OnTookItem(Stackable item)
    {
        _count++;

        if(_count != _targetAmount)
            return;

        EventSended?.Invoke(_eventName);
        _removeFromStackCondition.Complete();
    }
}
