using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBigGun : MonoBehaviour
{
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private float _radiusFactor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy) || other.TryGetComponent(out Ground ground))
        {
            StartCoroutine(Boom());
        }
    }

    private IEnumerator Boom()
    {
        _sphereCollider.radius = Mathf.Lerp(_sphereCollider.radius, _sphereCollider.radius * _radiusFactor, 1);
        yield return null;
    }
}
