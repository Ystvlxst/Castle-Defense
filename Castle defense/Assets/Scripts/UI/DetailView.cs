using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DetailView : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private DetailCollector _player;

    private void OnEnable()
    {
        _player.DetailBalanceChange += OnDetailBalanceChanged;
    }

    private void OnDisable()
    {
        _player.DetailBalanceChange -= OnDetailBalanceChanged;
    }

    private void OnDetailBalanceChanged()
    {
        _moneyText.text = _player.Detail.ToString();
    }
}
