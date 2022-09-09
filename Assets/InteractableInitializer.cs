using System;
using Game.Assistants.Behaviour;
using UnityEngine;

public class InteractableInitializer : MonoBehaviour
{
    [SerializeField] private InteractableObjectsContainer _interactableObjectsContainer;
    [SerializeField] private MonoBehaviour _interactableBehaviour;
    [SerializeField] private int _priority;

    private void OnValidate()
    {
        if (_interactableBehaviour is ICharacterInteractable == false)
            _interactableBehaviour = null;
    }

    private void Awake() => 
        _interactableObjectsContainer.Add((ICharacterInteractable) _interactableBehaviour, _priority);
}
