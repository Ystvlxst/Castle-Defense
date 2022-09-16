using System.Collections.Generic;
using BabyStack.Model;

public class LaserWeaponModification : Modification<float>
{
    private const string GUID = "LaserWeaponGUID";

    public LaserWeaponModification()
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
                new ModificationData<float>(40, 1.5f),
                new ModificationData<float>(60, 2f),
            };
        }
    }
}