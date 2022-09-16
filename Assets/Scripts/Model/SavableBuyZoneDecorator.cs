using System;
using System.Collections;
using UnityEngine;

namespace BabyStack.Model
{
    [Serializable]
    public class SavableBuyZoneDecorator : SavedObject<SavableBuyZoneDecorator>, IBuyZone, IDisposable
    {
        private static Hashtable _buyZones = new Hashtable();
        
        [SerializeField]private BuyZone _buyZone;

        private SavableBuyZoneDecorator(int totalCost, string guid)
            : base(guid)
        {
            SetBuyZone(new BuyZone(totalCost));
        }

        public event Action<int> CostUpdated;
        public event Action Unlocked;
        
        public int TotalCost => _buyZone.TotalCost;
        public int CurrentCost => _buyZone.CurrentCost;

        public static SavableBuyZoneDecorator GetZone(int totalCost, string guid)
        {
            if (_buyZones.ContainsKey(guid))
                return (SavableBuyZoneDecorator) _buyZones[guid];
            
            SavableBuyZoneDecorator buyZoneDecorator = new SavableBuyZoneDecorator(totalCost, guid);
            _buyZones.Add(guid, buyZoneDecorator);
            buyZoneDecorator.Load();

            return buyZoneDecorator;
        }

        public void ReduceCost(int value)
        {
            _buyZone.ReduceCost(value);
            Save();
        }

        public void Dispose()
        {
            _buyZone.Unlocked -= OnUnlocked;
            _buyZone.CostUpdated -= OnCostUpdated;
        }

        protected override void OnLoad(SavableBuyZoneDecorator loadedObject)
        {
            if(_buyZone?.CurrentCost == loadedObject.CurrentCost)
                return;
            
            SetBuyZone(loadedObject._buyZone);

            if (_buyZone.CurrentCost == 0)
                Unlocked?.Invoke();
        }

        private void SetBuyZone(BuyZone buyZone)
        {
            if (_buyZone != null)
            {
                _buyZone.Unlocked -= OnUnlocked;
                _buyZone.CostUpdated -= OnCostUpdated;
            }
            
            _buyZone = buyZone;
            _buyZone.Unlocked += OnUnlocked;
            _buyZone.CostUpdated += OnCostUpdated;
        }

        private void OnCostUpdated(int cost) => 
            CostUpdated?.Invoke(cost);

        private void OnUnlocked() => 
            Unlocked?.Invoke();
    }
}