using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpLabber : MonoBehaviour
{
    [SerializeField] private DownLabber _downLabber;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.transform.position = _downLabber.transform.position;
            Debug.Log("UpToDown");
        }
    }
}
