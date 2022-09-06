using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    public int Queue { get; private set; }

    public void AddToQueue()
    {
        Queue++;
    }

    public void RemoveFromQueue()
    {
        Queue--;
    }
}
