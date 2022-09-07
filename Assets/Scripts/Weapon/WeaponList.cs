using UnityEngine;

public class WeaponList : ReferenceObjectList<Weapon>
{
    [SerializeField] private ListProgress _weaponsProgress;
    [SerializeField] private EnemyContainer _enemyContainer;

    protected override void AfterUnlocked(Weapon reference, bool onLoad, string guid)
    {
        reference.GetComponentInChildren<TargetSelector>().Init(_enemyContainer);
        
        if (_weaponsProgress.Contains(guid))
            return;

        _weaponsProgress.Add(guid);
        _weaponsProgress.Save();
    }
}
