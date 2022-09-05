using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Bullet _bulletTemplate;
    [SerializeField] private float _speed;
    [SerializeField] private int _delay;

    private float _timeAfterLastShot;

    public int Delay => _delay;
    public float Timer => _timeAfterLastShot;

    private void Update()
    {
        _timeAfterLastShot += Time.deltaTime;
    }

    public void Shot()
    {
        if (_timeAfterLastShot >= _delay)
        {
            Bullet bullet = Instantiate(_bulletTemplate, transform.position, transform.rotation);
            bullet.Rigidbody.velocity = Vector3.forward * _speed;
            _timeAfterLastShot = 0;
        }
        else
            return;
    }
}
