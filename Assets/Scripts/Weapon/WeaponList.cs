using Game.Assistants.Behaviour;
using Source.UI.EnemyPointersUI;
using UnityEngine;

public class WeaponList : ReferenceObjectList<Weapon>
{
    [SerializeField] private ListProgress _weaponsProgress;
    [SerializeField] private EnemyContainer _enemyContainer;
    [SerializeField] private InteractableObjectsContainer _interactableObjectsContainer;
    [SerializeField] private WeaponsBreaker _weaponsBreaker;
    [SerializeField] private Player _player;

    protected override void AfterUnlocked(Weapon reference, bool onLoad, string guid)
    {
        reference.GetComponentInChildren<TargetSelector>().Init(_enemyContainer);

        _interactableObjectsContainer.Add(reference.GetComponentInChildren<ICharacterInteractable>(), 1);
        
        if(_weaponsBreaker != null)
            _weaponsBreaker.Add(reference);
        
        reference.GetComponentInChildren<BrokenObjectPointer>().Init(_player);

        LastWeaponRecharger lastWeaponRecharger = reference.GetComponentInChildren<LastWeaponRecharger>();

        if (lastWeaponRecharger != null)
            lastWeaponRecharger.Init(_weaponsBreaker);
            
        if (_weaponsProgress.Contains(guid))
            return;
        
        _weaponsProgress.Add(guid);

        _weaponsProgress.Save();
    }
}
