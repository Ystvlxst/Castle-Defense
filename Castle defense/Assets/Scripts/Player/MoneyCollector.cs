using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCollector : MonoBehaviour
{
    public int Money { get; private set; }

    private void OnEnemyDied(int reward)
    {
        Money += reward;
    }
}
