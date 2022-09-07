using UnityEngine;

class WeaponUpgradeUnlockableView : UpgradeUnlockableView
{
    [SerializeField] private GameObject _unlockedActiveObject;
    
    public override void Unlock() => 
        _unlockedActiveObject.gameObject.SetActive(true);
}