using System;
using System.Collections.Generic;
using System.Linq;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Game.Assistants.Behaviour
{
    public class HasInteractableTarget : Conditional
    {
        public SharedInteractablesContainer SharedInteractablesContainer;
        public SharedStackPresenter SelfStackPresenter;
        public SharedInteractable CurrentInteractable;

        public override TaskStatus OnUpdate()
        {
            IEnumerable<ICharacterInteractable> interactables = SharedInteractablesContainer.Value.Interactables;

            Debug.Log(SharedInteractablesContainer.Value.Interactables);
            
            foreach (var interactable in SharedInteractablesContainer.Value.Interactables)
                Debug.Log(interactable.InteractPoint);
            
            bool hasInteractable = interactables.Any(CanInteract());
            
            ICharacterInteractable characterInteractable = hasInteractable ? interactables.First(CanInteract()) : null;
            
            CurrentInteractable.Value = new CharacterInteractableReference(characterInteractable);

            return hasInteractable ? TaskStatus.Success : TaskStatus.Failure;
        }

        private Func<ICharacterInteractable, bool> CanInteract() => 
            interactable => Interactables.CanInteract(interactable, SelfStackPresenter.Value);
    }
}