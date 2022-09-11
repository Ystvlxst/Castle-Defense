using System;
using BehaviorDesigner.Runtime.Tasks;

namespace Game.Assistants.Behaviour
{
    public class FindInteractableTarget : Conditional
    {
        public SharedInteractablesContainer SharedInteractablesContainer;
        public SharedStackPresenter SelfStackPresenter;
        public SharedInteractable CurrentInteractable;

        public override TaskStatus OnUpdate()
        {
            bool hasInteractable = SharedInteractablesContainer.Value.Has(CanInteract());
            
            ICharacterInteractable characterInteractable = hasInteractable ? SharedInteractablesContainer.Value.Get(CanInteract()) : null;
            
            CurrentInteractable.Value = new CharacterInteractableReference(characterInteractable);

            TaskStatus taskStatus = hasInteractable ? TaskStatus.Success : TaskStatus.Failure;

            return taskStatus;
        }

        private Func<ICharacterInteractable, bool> CanInteract() => 
            interactable => Interactables.CanInteract(interactable, SelfStackPresenter.Value);
    }
}