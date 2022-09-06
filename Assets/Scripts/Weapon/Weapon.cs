using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private int _capacity;
    [SerializeField] private Transform _target;
    [SerializeField] private float _duration;

    public void Reload(int countBullets)
    {
        _capacity += countBullets;
    }

    public void Shot()
    {
        if (_spawner.Spawned != 0 && _capacity > 0)
        {
            _bulletSpawner.InstantiateBullet(_target, _duration);
        }
    }
}
