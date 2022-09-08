using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Bullet _bulletTemplate;
    [SerializeField] private float _delay;

    private float _time;

    private void Update()
    {
        _time += Time.deltaTime;
    }

    public void InstantiateBullet(Transform target, float duration)
    {
        if (_time >= _delay)
        {
            Bullet bullet = Instantiate(_bulletTemplate, transform.position, transform.rotation);
            bullet.transform.DOMove(target.position, duration);
            _time = 0;
        }
        else
            return;
    }
}
