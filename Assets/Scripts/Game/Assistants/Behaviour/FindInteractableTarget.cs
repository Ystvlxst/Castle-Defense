using System;
using System.Collections.Generic;
using System.Linq;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Game.Assistants.Behaviour
{
    public class FindInteractableTarget : Conditional
    {
        public SharedInteractablesContainer SharedInteractablesContainer;
        public SharedStackPresenter SelfStackPresenter;
        public SharedInteractable CurrentInteractable;

        public override TaskStatus OnUpdate()
        {
            IEnumerable<ICharacterInteractable> interactables = SharedInteractablesContainer.Value.Interactables;

            
            bool hasInteractable = interactables.Any(CanInteract());

            foreach (ICharacterInteractable interactable in interactables)
            {
                Debug.Log(interactable.CanInteract(SelfStackPresenter.Value));
            }
            ICharacterInteractable characterInteractable = hasInteractable ? interactables.First(CanInteract()) : null;
            
            CurrentInteractable.Value = new CharacterInteractableReference(characterInteractable);

            TaskStatus taskStatus = hasInteractable ? TaskStatus.Success : TaskStatus.Failure;
            return taskStatus;
        }

        private Func<ICharacterInteractable, bool> CanInteract() => 
            interactable => Interactables.CanInteract(interactable, SelfStackPresenter.Value);
    }
}