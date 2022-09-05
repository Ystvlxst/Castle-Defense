using UnityEngine;

public class DroppedStackableInteractable : TimerStackInteractableZone
{
    [SerializeField] private Stackable _item;
    
    private bool _taken;

    public override void InteractAction(StackPresenter enteredStack)
    {
        enteredStack.AddToStack(_item);
        _taken = true;
    }

    public override bool CanInteract(StackPresenter enteredStack)
    {
        return _taken == false && enteredStack.CanAddToStack(_item.Type);
    }
}