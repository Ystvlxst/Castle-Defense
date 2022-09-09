namespace Game.Assistants.Behaviour
{
    public static class Interactables
    {
        public static bool CanInteract(ICharacterInteractable interactable, StackPresenter stackPresenter) =>
            interactable.CanInteract(stackPresenter);
    }
}