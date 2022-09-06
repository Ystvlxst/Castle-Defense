using UnityEngine;

public class DetailCollector : Trigger<Detail>
{
    [SerializeField] private StackPresenter _stackPresenter;
    
    protected override void OnEnter(Detail detail)
    {
        if(detail.Taken)
            return;

        if (_stackPresenter.CanAddToStack(detail.Type) == false) 
            return;
        
        detail.Take();
        _stackPresenter.AddToStack(detail);
    }
}
