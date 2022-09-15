using Game.Assistants.Behaviour;
using UnityEngine;

public class WeaponList : ReferenceObjectList<Weapon>
{
    [SerializeField] private ListProgress _weaponsProgress;
    [SerializeField] private EnemyContainer _enemyContainer;
    [SerializeField] private InteractableObjectsContainer _interactableObjectsContainer;
    [SerializeField] private WeaponsBreaker _weaponsBreaker;

    protected override void AfterUnlocked(Weapon reference, bool onLoad, string guid)
    {
        reference.GetComponentInChildren<TargetSelector>().Init(_enemyContainer);

        _interactableObjectsContainer.Add(reference.GetComponentInChildren<ICharacterInteractable>(), 1);
        _weaponsBreaker.Add(reference.GetComponentInChildren<BreakdownStatus>());
        
        if (_weaponsProgress.Contains(guid))
            return;
        
        _weaponsProgress.Add(guid);
        _weaponsProgress.Save();
    }
}
