using BabyStack.Model;

public class BigWeaponUpgradeUnlockable : UpgradeUnlockable<float>
{
    protected override Modification<float> GetModification() => 
        ModificationsContainer.Get<BigWeaponModification>();
}