using BabyStack.Model;
using UnityEngine;

public abstract class UpgradeUnlockable<T> : UnlockableObject
{
    [SerializeField] private MonoBehaviour _upgradeable;
    
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

    private void Awake() => 
        _modification = LoadModification();

    protected abstract Modification<T> LoadModification();

    public override GameObject Unlock(Transform parent, bool onLoad, string guid)
    {
        _modification.Upgrade();
        (_upgradeable as IModificationListener<T>)?.OnModificationUpdate(_modification.CurrentModificationValue);
        
        return gameObject;
    }
}