using BabyStack.Model;

public class FastWeaponUpgradeUnlockable : UpgradeUnlockable<float>
{
    protected override Modification<float> GetModification() => 
        new FastWeaponModification(GUID);
}