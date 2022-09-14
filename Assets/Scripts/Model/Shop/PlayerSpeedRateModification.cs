using System.Collections.Generic;

namespace BabyStack.Model
{
    public class PlayerSpeedRateModification : Modification<float>
    {
        private const string GUID = "PlayerSpeedRateGUID";

        public PlayerSpeedRateModification()
            : base(GUID) { }

        public override List<ModificationData<float>> Data
        {
            get
            {
                return new List<ModificationData<float>>()
                {
                    new ModificationData<float>(3, 1f),
                    new ModificationData<float>(3, 1.1f),
                    new ModificationData<float>(5, 1.2f),
                    new ModificationData<float>(7, 1.3f),
                    new ModificationData<float>(10, 1.3f),
                    new ModificationData<float>(12, 1.4f),
                    new ModificationData<float>(15, 1.5f),
                };
            }
        }
    }
}