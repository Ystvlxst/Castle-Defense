using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetailCollector : MonoBehaviour
{
    public event UnityAction DetailBalanceChange;

    public int Detail { get; private set; }

    private void Awake()
    {
        AddDetail(0);
    }

    public void AddDetail(int reward)
    {
        Detail += reward;
        DetailBalanceChange?.Invoke();
    }
}
