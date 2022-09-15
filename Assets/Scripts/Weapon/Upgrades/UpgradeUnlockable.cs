using System;
using BabyStack.Model;
using UnityEngine;

public abstract class UpgradeUnlockable<T> : UnlockableObject
{
    [SerializeField] private MonoBehaviour _upgradeable;
    [SerializeField] private UpgradeUnlockableView _view;
    
    private Modification<T> _modification;

    
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_upgradeable != null)
        {
            if (_upgradeable is IModificationListener<T> == false)
                _upgradeable = null;
        }
    }
#endif

    private void Awake()
    {
        _modification = LoadModification();
    }

    protected abstract Modification<T> LoadModification();

    public override GameObject Unlock(Transform parent, bool onLoad)
    {
        _modification.Load();

        if(onLoad == false && _modification.TryGetNextModification(out ModificationData<T> _))
            _modification.Upgrade();
        
        _modification.Save();
        (_upgradeable as IModificationListener<T>).OnModificationUpdate(_modification.CurrentModificationValue);
        _view.Unlock();
        
        return gameObject;
    }
}