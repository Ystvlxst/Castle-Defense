using System;
using BehaviorDesigner.Runtime;

namespace Game.Assistants.Behaviour
{
    [Serializable]
    public class SharedInteractable : SharedVariable<CharacterInteractableReference>
    {
        public static implicit operator SharedInteractable(CharacterInteractableReference value) =>
            new SharedInteractable {Value = value};
    }
}