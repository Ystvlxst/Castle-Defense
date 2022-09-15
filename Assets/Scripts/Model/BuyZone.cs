using System;
using UnityEngine;

namespace BabyStack.Model
{
    [Serializable]
    public class BuyZone : IBuyZone
    {
        [SerializeField] protected DynamicCost _dynamicCost;

        public BuyZone(int totalCost) => 
            _dynamicCost = new DynamicCost(totalCost);

        public virtual event Action Unlocked;
        public virtual event Action<int> CostUpdated;
        public int TotalCost => _dynamicCost.TotalCost;
        public int CurrentCost => _dynamicCost.CurrentCost;

        public void ReduceCost(int value)
        {
            _dynamicCost.Subtract(value);
            CostUpdated?.Invoke(_dynamicCost.CurrentCost);

            if (_dynamicCost.CurrentCost == 0)
                Unlocked?.Invoke();
        }
    }
}