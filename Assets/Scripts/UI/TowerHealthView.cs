using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class TowerHealthView : MonoBehaviour
{
    [SerializeField] private Tower _player;

    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();

        _slider.maxValue = _player.Health;
    }

    private void Update()
    {
        _slider.value = _player.CurrentHealth;
    }
}
