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
                    new ModificationData<int>(300, 5),
                    new ModificationData<int>(200, 8),
                    new ModificationData<int>(400, 11),
                    new ModificationData<int>(700, 14),
                    new ModificationData<int>(1000, 17),
                    new ModificationData<int>(1500, 20),
                };
            }
        }
    }
}