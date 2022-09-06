using System;
using UnityEngine;

public class Assistant : MonoBehaviour
{
    public Transform[] WaitPoints { get; private set; }

    public void Init(Transform[] waitPoints)
    {
        WaitPoints = waitPoints;
    }
}