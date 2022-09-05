using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Bullet _bulletTemplate;

    private Bullet _bullet;

    public void InstantiateBullet(Transform target, float duration)
    {
        _bullet = Instantiate(_bulletTemplate, transform.position, transform.rotation);
        _bullet.transform.DOMove(target.position, duration);
    }
}
