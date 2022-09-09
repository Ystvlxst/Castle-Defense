namespace Game.Assistants.Behaviour
{
    public class CharacterInteractableReference
    {
        public ICharacterInteractable CharacterInteractable { get; }

        public CharacterInteractableReference(ICharacterInteractable characterInteractable)
        {
            this.CharacterInteractable = characterInteractable;
        }
    }
}