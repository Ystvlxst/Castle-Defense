using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartGun : MonoBehaviour
{
    [SerializeField] private BulletSpawner _bulletSpawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            _bulletSpawner.Shot();
    }
}
