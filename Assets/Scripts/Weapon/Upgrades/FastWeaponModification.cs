using System.Collections.Generic;
using BabyStack.Model;

public class FastWeaponModification : Modification<float>
{
    private const string GUID = "WeaponCooldownGUID";

    public FastWeaponModification()
        : base(GUID)
    {
    }

    public override List<ModificationData<float>> Data
    {
        get
        {
            return new List<ModificationData<float>>()
            {
                new ModificationData<float>(100, 1f),
                new ModificationData<float>(1400, 1.5f),
                new ModificationData<float>(1400, 2f),
            };
        }
    }
}