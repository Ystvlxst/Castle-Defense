using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Game.Assistants.Behaviour
{
    public class InteractableObjectsContainer : MonoBehaviour
    {
        [SerializeField] private List<MonoBehaviour> _interactableBehaviours;

        public IEnumerable<ICharacterInteractable> Interactables =>
            _interactableBehaviours.OfType<ICharacterInteractable>();

        [ContextMenu(nameof(FindInteractables))]
        private void FindInteractables()
        {
            _interactableBehaviours = new List<MonoBehaviour>();

            foreach (MonoBehaviour monoBehaviour in FindObjectsOfType<MonoBehaviour>())
            {
                if(monoBehaviour is ICharacterInteractable)
                    _interactableBehaviours.Add(monoBehaviour);
            }

            foreach (ICharacterInteractable interactable in _interactableBehaviours)
            {
                Debug.Log(interactable.InteractPoint);
            }
        }
    }
}