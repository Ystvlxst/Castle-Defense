using System.Collections.Generic;

namespace BabyStack.Model
{
    public class PlayerStackCapacityModification : Modification<int>
    {
        private const string GUID = "PlayerStackCapacityModificationGUID";

        public PlayerStackCapacityModification()
            : base(GUID) { }

        public override List<ModificationData<int>> Data
        {
            get
            {
                return new List<ModificationData<int>>()
                {
                    new ModificationData<int>(5, 5),
                    new ModificationData<int>(5, 8),
                    new ModificationData<int>(8, 11),
                    new ModificationData<int>(11, 14),
                    new ModificationData<int>(14, 17),
                    new ModificationData<int>(17, 20),
                };
            }
        }
    }
}