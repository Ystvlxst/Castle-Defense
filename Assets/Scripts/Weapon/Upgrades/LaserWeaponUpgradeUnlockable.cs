using BabyStack.Model;

public class LaserWeaponUpgradeUnlockable : UpgradeUnlockable<float>
{
    protected override Modification<float> LoadModification() => 
        new LaserWeaponModification();
}