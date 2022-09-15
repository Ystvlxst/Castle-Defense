using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Game.Assistants.Behaviour
{
    public class InteractableObjectsContainer : MonoBehaviour
    {
        private List<InteractablePriority> _interactablePriorities = new List<InteractablePriority>();

        public void AddRange(ICharacterInteractable[] interactables, int priority)
        {
            foreach (ICharacterInteractable interactable in interactables)
                Add(interactable, priority);
        }

        public void Add(ICharacterInteractable getComponentInChildren, int priority)
        {
            for (var i = 0; i < _interactablePriorities.Count; i++)
            {
                if (_interactablePriorities[i].Priority > priority)
                {
                    _interactablePriorities.Insert(i, new InteractablePriority(getComponentInChildren, priority));
                    return;
                }
            }

            _interactablePriorities.Add(new InteractablePriority(getComponentInChildren, priority));
        }

        public bool Has(Func<ICharacterInteractable, bool> canInteract) =>
            _interactablePriorities.Any(priority => canInteract.Invoke(priority.CharacterInteractable));

        public ICharacterInteractable Get(Func<ICharacterInteractable, bool> canInteract)
        {
            int priority = _interactablePriorities
                .First(interactablePriority => canInteract(interactablePriority.CharacterInteractable)).Priority;
            
            IEnumerable<InteractablePriority> enumerable =
                _interactablePriorities.Where(interactablePriority => interactablePriority.Priority == priority);
            
            return enumerable.Select(priority => priority.CharacterInteractable).Shuffle().First();
        }
    }

    internal class InteractablePriority
    {
        public ICharacterInteractable CharacterInteractable;
        public int Priority;

        public InteractablePriority(ICharacterInteractable characterInteractable, int priority)
        {
            CharacterInteractable = characterInteractable;
            Priority = priority;
        }
    }
}