using BabyStack.Model;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(StackPresenter))]
public class MoneyHolder : MonoBehaviour
{
    [SerializeField] private StackableType _currencyType = StackableType.Detail;
    
    private StackPresenter _stackPresenter;
    public event UnityAction<int> BalanceChanged;

    public int Value => _stackPresenter.CalculateCount(_currencyType);
    public bool HasMoney => Value > 0;

    private void Awake()
    {
        _stackPresenter = GetComponent<StackPresenter>();
    }

    private void OnEnable()
    {
        _stackPresenter.Added += OnAdded;
        _stackPresenter.Removed += OnAdded;
    }
    
    private void OnDisable()
    {
        _stackPresenter.Added += OnAdded;
        _stackPresenter.Removed += OnAdded;
    }

    private void OnAdded(Stackable stackable)
    {
        if (stackable.Type == _currencyType) 
            BalanceChanged?.Invoke(Value);
    }

    public void SpendMoney(int value)
    {
        for (int i = 0; i < value; i++)
        {
            Stackable item = _stackPresenter.RemoveFromStack(_currencyType);
            Destroy(item.gameObject);
        }
    }

}
