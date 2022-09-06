using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ReloadSlider : MonoBehaviour
{
    [SerializeField] private BulletSpawner _bulletSpawner;

    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
       // _slider.maxValue = _bulletSpawner.Delay;
    }

    private void Update()
    {
        //_slider.value = _bulletSpawner.Timer;
    }
}
