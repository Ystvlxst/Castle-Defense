using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detail : MonoBehaviour
{
    [SerializeField] private int _reward;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out DetailCollector detailCollector))
        {
            detailCollector.AddDetail(_reward);
            Destroy(gameObject);
        }
    }
}
