using Game.Assistants.Behaviour;
using UnityEngine;

public class ConveyorList : ReferenceObjectList<Conveyor>
{
    [SerializeField] private ListProgress _conveyorsProgress;
    [SerializeField] private InteractableObjectsContainer _interactableObjectsContainer;

    protected override void AfterUnlocked(Conveyor reference, bool onLoad, string guid)
    {
        _interactableObjectsContainer.AddRange(reference.GetComponentsInChildren<ICharacterInteractable>());
        
        if (_conveyorsProgress.Contains(guid))
            return;

        _conveyorsProgress.Add(guid);
        _conveyorsProgress.Save();
    }
}
