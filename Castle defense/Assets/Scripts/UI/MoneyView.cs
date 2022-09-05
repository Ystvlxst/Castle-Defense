using UnityEngine;
using TMPro;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private MoneyCollector _player;

    private void OnEnable()
    {
        _player.MoneyBalanceChange += OnMoneyBalanceChanged;
    }

    private void OnDisable()
    {
        _player.MoneyBalanceChange -= OnMoneyBalanceChanged;
    }

    private void OnMoneyBalanceChanged()
    {
        _moneyText.text = _player.Money.ToString();
    }
}
