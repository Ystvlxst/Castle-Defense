using BabyStack.Model;

public class FastWeaponUpgradeUnlockable : UpgradeUnlockable<float>
{
    protected override Modification<float> LoadModification() => 
        new FastWeaponModification();
}