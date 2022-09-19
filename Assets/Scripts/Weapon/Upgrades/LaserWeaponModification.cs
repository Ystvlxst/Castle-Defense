using System.Collections.Generic;
using BabyStack.Model;

public class LaserWeaponModification : Modification<float>
{
    private const string GUID = "LaserWeaponGUID";

    public LaserWeaponModification(string guid)
        : base(GUID + guid)
    {
    }

    public override List<ModificationData<float>> Data
    {
        get
        {
            return new List<ModificationData<float>>()
            {
                new ModificationData<float>(1, 1f),
                new ModificationData<float>(50, 1.5f),
                new ModificationData<float>(70, 2f),
            };
        }
    }
}