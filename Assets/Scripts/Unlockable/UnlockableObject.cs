using UnityEngine;

public abstract class UnlockableObject : MonoBehaviour
{
    [SerializeField] protected UnlockableObjectType _type;

    public UnlockableObjectType Type => _type;

    public abstract GameObject Unlock(Transform parent, bool onLoad);
}

public enum UnlockableObjectType
{
    Conveyor,
    Weapon,
    Upgrade,
    WeaponRepair,
    MapZone
}