using System;
using BabyStack.Model;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class BuyZonePresenter : MonoBehaviour
{
    [Space(10)] [SerializeField] private int _totalCost;
    [SerializeField] private MoneyHolderTrigger _trigger;
    [SerializeField] private BuyZoneView _view;
    [SerializeField] private UnlockableObject _unlockable;

    private IBuyZone _buyZone;
    private Coroutine _tryBuy;
    private float _betweenPayDelay = 0.11f;

    public event UnityAction<BuyZonePresenter> FirstTimeUnlocked;
    public event UnityAction<BuyZonePresenter> Unlocked;
    public abstract event UnityAction Unlocking;
    
    public int TotalCost => _totalCost;
    public bool IsUnlocked => _buyZone.CurrentCost == 0;


#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_view)
            _view.RenderCost(_totalCost);
    }
#endif
    
    public void Init(BuyZone buyZone, UnlockableObject unlockable)
    {
        _unlockable = unlockable;
        Init(buyZone);
    }

    public void Init(IBuyZone buyZone) =>
        _buyZone = buyZone;

    private void OnEnable()
    {
        _trigger.Enter += OnPlayerTriggerEnter;
        _trigger.Exit += OnPlayerTriggerExit;

        OnEnabled();
    }

    private void OnDisable()
    {
        _trigger.Enter -= OnPlayerTriggerEnter;
        _trigger.Exit -= OnPlayerTriggerExit;

        OnDisabled();
    }

    private void Start()
    {
        if (_buyZone == null)
            throw new InvalidOperationException("Not initialized");

        _buyZone.Unlocked += OnBuyZoneUnlocked;
        _buyZone.CostUpdated += OnCostUpdated;

        if (IsUnlocked)
            OnBuyZoneUnlockedOnLoad();

        UpdateCost();

        OnBuyZoneLoaded(_buyZone);
    }

    private void OnDestroy()
    {
        _buyZone.Unlocked -= OnBuyZoneUnlocked;
        _buyZone.CostUpdated -= OnCostUpdated;
    }

    public void PlayNewText() =>
        _view.PlayNewText();

    private void OnPlayerTriggerEnter(MoneyHolder moneyHolder)
    {
        var movement = moneyHolder.GetComponent<CharacterMovement>();
        var stackPresenter = moneyHolder.GetComponent<StackPresenter>();

        if (_tryBuy != null)
            StopCoroutine(_tryBuy);

        _tryBuy = StartCoroutine(TryBuy(moneyHolder, stackPresenter, movement));

        OnEnter();
    }

    private void OnPlayerTriggerExit(MoneyHolder moneyHolder)
    {
        StopCoroutine(_tryBuy);

        OnExit();
    }

    private void OnBuyZoneUnlockedOnLoad() =>
        OnBuyZoneUnlocked(true);

    private void OnBuyZoneUnlocked() =>
        OnBuyZoneUnlocked(false);

    private void OnBuyZoneUnlocked(bool onLoad)
    {
        _trigger.Disable();
        _view.Hide();
        _unlockable.Unlock(transform, onLoad);

        Unlocked?.Invoke(this);

        if (onLoad == false)
            FirstTimeUnlocked?.Invoke(this);
    }

    private IEnumerator TryBuy(MoneyHolder moneyHolder, StackPresenter stackPresenter,
        CharacterMovement characterMovement)
    {
        yield return null;

        bool delayed = false;
        while (true)
        {
            if (characterMovement.IsMoving == false)
            {
                if (delayed == false)
                    yield return new WaitForSeconds(0.75f);

                BuyFrame(_buyZone, moneyHolder, stackPresenter);
                UpdateCost();
                delayed = true;
            }
            else
            {
                delayed = false;
            }

            yield return new WaitForSeconds(_betweenPayDelay);
        }
    }

    private void OnCostUpdated(int value) =>
        UpdateCost();

    private void UpdateCost() =>
        _view.RenderCost(_buyZone.CurrentCost);

    protected virtual void OnBuyZoneLoaded(IBuyZone buyZone)
    {
    }

    protected virtual void OnEnabled()
    {
    }

    protected virtual void OnDisabled()
    {
    }

    protected virtual void OnEnter()
    {
    }

    protected virtual void OnExit()
    {
    }

    protected abstract void BuyFrame(IBuyZone buyZone, MoneyHolder moneyHolder, StackPresenter stackPresenter);
}