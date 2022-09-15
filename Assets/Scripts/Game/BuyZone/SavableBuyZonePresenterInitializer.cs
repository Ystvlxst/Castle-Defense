using BabyStack.Model;
using UnityEngine;

namespace Game.BuyZone
{
    [RequireComponent(typeof(GUIDObject))]
    [RequireComponent(typeof(BuyZonePresenter))]
    public class SavableBuyZonePresenterInitializer : MonoBehaviour
    {
        private BuyZonePresenter _buyZonePresenter;
        private GUIDObject _guidObject;
        
        private void Awake()
        {
            _buyZonePresenter = GetComponent<BuyZonePresenter>();
            _guidObject = GetComponent<GUIDObject>();
            _buyZonePresenter.Init(SavableBuyZoneDecorator.GetZone(_buyZonePresenter.TotalCost, _guidObject.GUID));
        }
    }
}