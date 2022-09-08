using UnityEngine;
using System.Collections;

public class MoneyMagnetHaptics : BaseHaptics
{
    [SerializeField] private MoneyCollector itemMagnet;

    private void OnEnable()
    {
        itemMagnet.Collected += OnCollected;
    }

    private void OnDisable()
    {
        itemMagnet.Collected -= OnCollected;
    }

    private void OnCollected(int count)
    {
        Vibrate();
    }
}
