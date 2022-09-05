using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownLabber : MonoBehaviour
{
    [SerializeField] private UpLabber _labber;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            player.transform.position = _labber.transform.position;
    }
}
