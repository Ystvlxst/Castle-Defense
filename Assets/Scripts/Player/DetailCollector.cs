using UnityEngine;

public class DetailCollector : Trigger<Detail>
{
    [SerializeField] private ItemMagnet _itemMagnet;
    [SerializeField] private DronePlatform _dronePlatform;
    [SerializeField] private StackPresenter _stackPresenter;
    
    protected override void OnEnter(Detail detail)
    {
        if(!enabled || detail.Taken)
            return;
        
        detail.Take();
        _itemMagnet.Attract(detail.GetComponent<DropableItem>(), () => OnAttracted(detail));
    }

    private void OnAttracted(Detail detail)
    {
        detail.transform.position = _dronePlatform.transform.position;
        _stackPresenter.AddToStack(detail);
    }
}
