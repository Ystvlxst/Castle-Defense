using UnityEngine;

namespace Game.Assistants.Behaviour
{
    class SetDestimationInteractable : SetDestination
    {
        public SharedInteractable CurrentInteractable;

        protected override Vector3 GetPosition() => CurrentInteractable.Value.CharacterInteractable.InteractPoint;
    }
}