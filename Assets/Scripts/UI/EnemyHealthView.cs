using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class EnemyHealthView : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();

        _slider.maxValue = _enemy.Health;
    }

    private void Update()
    {
        _slider.value = _enemy.Health;
    }
}
