using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Game.Assistants.Behaviour
{
    public class CanInteract : Conditional
    {
        public SharedStackPresenter SelfStackPresenter;
        public SharedInteractable CurrentInteractable;

        public override TaskStatus OnUpdate()
        {
            if (CurrentInteractable.Value?.CharacterInteractable == null)
                return TaskStatus.Failure;
            
            bool canInteract = CurrentInteractable.Value.CharacterInteractable.CanInteract(SelfStackPresenter.Value);
            return BehaviourHelper.ToTaskStatus(canInteract);
        }
    }
}