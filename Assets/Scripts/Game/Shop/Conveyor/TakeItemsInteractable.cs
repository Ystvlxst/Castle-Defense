using System;
using System.Linq;
using BabyStack.Model;
using DG.Tweening;
using UnityEngine;

public class TakeItemsInteractable : TimerStackInteractableZone
{
    [SerializeField] private StackPresenter _conveyorEndStack;
    
    public StackPresenter ConveyorEndStack => _conveyorEndStack;

    public event Action<Stackable> TookItem;

    public override void InteractAction(StackPresenter enteredStack)
    {
        var stackable = _conveyorEndStack.RemoveFromStack(GetFirstInOutput());
        stackable.transform.DOComplete(true);
        enteredStack.AddToStack(stackable);
        TookItem?.Invoke(stackable);
    }

    public override bool CanInteract(StackPresenter enteredStack) => 
        base.CanInteract(enteredStack) && _conveyorEndStack.Count > 0 && enteredStack.CanAddToStack(GetFirstInOutput());

    private StackableType GetFirstInOutput() => 
        _conveyorEndStack.Data.First().Type;
}
