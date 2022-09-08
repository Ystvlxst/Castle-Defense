using UnityEngine;

public class Ammo : Stackable
{
    [SerializeField] private StackableType _ammoType = StackableType.Ammo;
    
    public override StackableType Type => _ammoType;
}
