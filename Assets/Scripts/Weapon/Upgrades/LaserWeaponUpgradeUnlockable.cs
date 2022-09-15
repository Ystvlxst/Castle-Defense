using BabyStack.Model;

public class LaserWeaponUpgradeUnlockable : UpgradeUnlockable<float>
{
    protected override Modification<float> GetModification() => 
        ModificationsContainer.Get<LaserWeaponModification>();
}