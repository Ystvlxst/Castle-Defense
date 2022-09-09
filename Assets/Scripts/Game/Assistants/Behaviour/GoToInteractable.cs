using UnityEngine;

namespace Game.Assistants.Behaviour
{
    class GoToInteractable : GoTo
    {
        public SharedInteractable CurrentInteractable;

        protected override Vector3 GetPosition() => CurrentInteractable.Value.CharacterInteractable.InteractPoint;
    }
}