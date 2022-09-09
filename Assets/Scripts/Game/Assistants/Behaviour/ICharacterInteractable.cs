using UnityEngine;

public interface ICharacterInteractable
{
    bool CanInteract(StackPresenter enteredStack);
    Vector3 InteractPoint { get; }
}