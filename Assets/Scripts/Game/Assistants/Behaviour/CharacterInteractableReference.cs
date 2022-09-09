using System;

namespace Game.Assistants.Behaviour
{
    [Serializable]
    public class CharacterInteractableReference
    {
        public ICharacterInteractable CharacterInteractable { get; set; }

        public CharacterInteractableReference(ICharacterInteractable characterInteractable)
        {
            this.CharacterInteractable = characterInteractable;
        }
    }
}