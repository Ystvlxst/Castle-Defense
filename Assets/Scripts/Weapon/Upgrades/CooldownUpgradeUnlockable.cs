using BabyStack.Model;

public class CooldownUpgradeUnlockable : UpgradeUnlockable<float>
{
    protected override Modification<float> LoadModification() => 
        new WeaponCooldownModification();
}