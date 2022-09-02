using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Bullet _bulletTemplate;
    [SerializeField] private float _speed;

    public void Shot()
    {
        Bullet bullet = Instantiate(_bulletTemplate, transform.position, transform.rotation);
        bullet.Rigidbody.velocity = Vector3.forward * _speed;
    }
}
