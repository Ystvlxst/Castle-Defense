using UnityEngine;

class WeaponUpgradeUnlockableView : UpgradeUnlockableView
{
    [SerializeField] private GameObject[] _unlockedActiveObject;
    
    public override void Unlock(int level)
    {
        level = Mathf.Clamp(level, 0, _unlockedActiveObject.Length - 1);

        for (int i = 0; i < _unlockedActiveObject.Length; i++)
            _unlockedActiveObject[i].gameObject.SetActive(i == level);
    }
}