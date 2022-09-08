using System;
using UnityEngine;

public class MoneyCollector : MonoBehaviour
{
    [SerializeField] private ItemMagnet _magnet;
    [SerializeField] private MoneyHolder _moneyHolder;
    [SerializeField] private Trigger<DroppableDollar> _trigger;
    [SerializeField] private Trigger<MoneyZone> _moneyZoneTrigger;

    public event Action<int> Collected;


    private void OnEnable()
    {
        _trigger.Stay += OnStay;
        _moneyZoneTrigger.Stay += OnStay;
    }

    private void OnDisable()
    {
        _trigger.Stay -= OnStay;
        _moneyZoneTrigger.Stay -= OnStay;
    }

    private void OnStay(DroppableDollar droppableDollar)
    {
        if (droppableDollar.CanTake == false)
            return;

        Dollar dollar = droppableDollar.Take();
        _magnet.Attract(droppableDollar, () => OnDollarAttracted(dollar.Value, dollar.transform));
    }

    private void OnStay(MoneyZone moneyZone)
    {
        if (moneyZone.Dollars == 0)
            return;

        for (int i = 0; i < 5; i++)
        {
            var dollar = moneyZone.Remove();
            _magnet.Attract(dollar.transform, () => OnDollarAttracted(dollar.Value, dollar.transform));

            if (moneyZone.Dollars == 0)
                break;
        }
    }

    public void OnDollarAttracted(int dollarValue, Transform dollarTransform)
    {
        _moneyHolder.AddMoney(dollarValue);
        Collected?.Invoke(dollarValue);
        Destroy(dollarTransform.gameObject);
    }
}