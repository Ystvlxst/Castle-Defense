using UnityEngine;

public abstract class ClickStackInteractableZone : StackInteractableZoneBase
{
    [SerializeField] private PlayerJoystickInput playerJoystickInput;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && !playerJoystickInput.StoppedMoving && !playerJoystickInput.Moving)
            TryInteract();
    }

    public void TryInteract()
    {
        if (EnteredStack && CanInteract(EnteredStack))
            Interact();
    }

    private void Interact()
    {
        InteractAction(EnteredStack);
    }

    public abstract void InteractAction(StackPresenter enteredStack);
    public abstract bool CanInteract(StackPresenter enteredStack);
}