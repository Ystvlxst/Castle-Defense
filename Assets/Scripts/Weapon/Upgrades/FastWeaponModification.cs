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
                new ModificationData<float>(1, 1f),
                new ModificationData<float>(20, 1.5f),
                new ModificationData<float>(40, 2f),
            };
        }
    }
}