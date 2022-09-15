using System.Collections.Generic;
using BabyStack.Model;

public class BigWeaponModification : Modification<float>
{
    private const string GUID = "BigWeaponGUID";

    public BigWeaponModification()
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
                new ModificationData<float>(30, 1.5f),
                new ModificationData<float>(50, 2f),
            };
        }
    }
}