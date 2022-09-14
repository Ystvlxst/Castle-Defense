using BabyStack.Model;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class NormalBuyZonePresenter : BuyZonePresenter
{
    [Space(5)] [SerializeField] private bool _alwaysUnlocked = false;
    [SerializeField] private StackableType _currencyType;
    [SerializeField] private StackPresenter _stackPresenter;
    
    private int _reduceValue = 1;
    private bool _paying;
    private BuyZone _currentBuyZone;

    public override event UnityAction Unlocking;

    protected override void OnBuyZoneLoaded(BuyZone buyZone)
    {
        if (_alwaysUnlocked && buyZone.CurrentCost > 0)
        {
            buyZone.ReduceCost(buyZone.CurrentCost);
            buyZone.Save();
        }
    }

    protected override void BuyFrame(BuyZone buyZone, MoneyHolder moneyHolder, StackPresenter stackPresenter)
    {
        _currentBuyZone = buyZone;

        if (AvailableCurrency(moneyHolder, stackPresenter) == 0)
            return;

        _reduceValue = Mathf.Clamp((int) (TotalCost * 1.5f * Time.deltaTime), 1, TotalCost);

        if (buyZone.CurrentCost < _reduceValue)
            _reduceValue = buyZone.CurrentCost;

        _reduceValue = Mathf.Clamp(_reduceValue, 1, AvailableCurrency(moneyHolder, stackPresenter));
            
        if (_currencyType != StackableType.Dollar)
                _reduceValue = 1;

        SpendMoney(moneyHolder, stackPresenter, buyZone);

        Unlocking?.Invoke();
    }

    private void SpendMoney(MoneyHolder moneyHolder, StackPresenter stackPresenter, BuyZone buyZone)
    {
        if (_currencyType == StackableType.Dollar)
        {
            moneyHolder.SpendMoney(_reduceValue);
            ReduceCost(buyZone);
        }
        else
        {
            for (int i = 0; i < _reduceValue; i++)
            {
                Stackable removed = stackPresenter.RemoveFromStack(_currencyType);
                _paying = true;
                _stackPresenter.AddToStack(removed);
                _stackPresenter.Added += OnAddedToStack;
            }
        }
    }

    private void OnAddedToStack(Stackable stackable)
    {
        _stackPresenter.Added -= OnAddedToStack;
        _paying = false;
        _stackPresenter.RemoveFromStack(stackable);
        Destroy(stackable.gameObject);
        ReduceCost(_currentBuyZone);
    }

    private void ReduceCost(BuyZone buyZone)
    {
        buyZone.ReduceCost(_reduceValue);
    }

    private int AvailableCurrency(MoneyHolder moneyHolder, StackPresenter stackPresenter) =>
        _currencyType == StackableType.Dollar ? moneyHolder.Value : stackPresenter.CalculateCount(_currencyType);
}