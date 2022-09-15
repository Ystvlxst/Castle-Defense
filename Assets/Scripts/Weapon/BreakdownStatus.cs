using BabyStack.Model;
using UnityEngine;

public class BreakdownStatus : UnlockableObject
{
    [SerializeField] private BuyZonePresenter _repairZoneTemplate;
    [SerializeField] private int _repairCost;
    
    private BuyZonePresenter _buyZonePresenter;

    public bool Broken { get; private set; }

    public void Break()
    {
        if(Broken)
            return;
        
        Broken = true;
        _buyZonePresenter = Instantiate(_repairZoneTemplate, transform);
        _buyZonePresenter.Init(new BuyZone(_repairCost), this);
    }

    private void Repair()
    {
        if(!Broken)
            return;

        Broken = false;
    }

    public override GameObject Unlock(Transform parent, bool onLoad)
    {
        Destroy(_buyZonePresenter.gameObject);
        Repair();
        return gameObject;
    }
}