using System;
using BabyStack.Model;
using UnityEngine;

public abstract class UpgradeUnlockable<T> : UnlockableObject
{
    [SerializeField] private MonoBehaviour _upgradeable;
    [SerializeField] private UpgradeUnlockableView _view;
    [SerializeField] private GUIDObject _guid;

    public Modification<T> Modification { get; private set; }

    protected string GUID => _guid.GUID;


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
        Modification = GetModification();
        Modification.Load();
        Modification.Upgraded += UpdateUpgradable;
    }

    private void OnDestroy()
    {
        Modification.Upgraded -= UpdateUpgradable;
    }

    private void Start()
    {
        Modification.Load();
        UpdateUpgradable();
    }

    protected abstract Modification<T> GetModification();

    public override GameObject Unlock(Transform parent, bool onLoad)
    {
        if(Modification.TryGetNextModification(out ModificationData<T> _))
            Modification.Upgrade();
        
        Modification.Save();
        
        return gameObject;
    }

    private void UpdateUpgradable()
    {
        (_upgradeable as IModificationListener<T>).OnModificationUpdate(Modification.CurrentModificationValue);
        _view.Unlock(Modification.CurrentModificationLevel);
    }
}