using System;
using BehaviorDesigner.Runtime;

namespace Game.Assistants.Behaviour
{
    [Serializable]
    public class SharedInteractablesContainer : SharedVariable<InteractableObjectsContainer>
    {
        public static implicit operator SharedInteractablesContainer(InteractableObjectsContainer value) =>
            new SharedInteractablesContainer {Value = value};
    }
}