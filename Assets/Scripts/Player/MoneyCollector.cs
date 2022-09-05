using UnityEngine;
using UnityEngine.Events;

public class MoneyCollector : MonoBehaviour
{
    public event UnityAction MoneyBalanceChange;

    public int Money { get; private set; }

    private void Awake()
    {
        AddMoney(0);
    }

    public void AddMoney(int reward)
    {
        Money += reward;
        MoneyBalanceChange?.Invoke();
    }
}
