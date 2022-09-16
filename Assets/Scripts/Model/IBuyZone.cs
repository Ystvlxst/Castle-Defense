using System;

namespace BabyStack.Model
{
    public interface IBuyZone
    {
        event Action Unlocked;
        event Action<int> CostUpdated;
        int TotalCost { get; }
        int CurrentCost { get; }
        void ReduceCost(int value);
    }
}