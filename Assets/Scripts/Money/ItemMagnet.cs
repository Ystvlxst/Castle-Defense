using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class ItemMagnet : MonoBehaviour
{
    [SerializeField] private float _attractDuration = 1f;
    [SerializeField] private float _followOffsetDistance = 5f;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _takeShakeDuration = 0.2f;
    [Space]
    [SerializeField] private bool _reduceScale = true;
    [SerializeField] private float _scaleReduceDuration = 0.5f;
    [SerializeField] private float _scaleReduceMoveSpeed = 5;
    [SerializeField] private float _targetOffsetMagnitude;

    private float _followRange => _followOffsetDistance * _followOffsetDistance;

    public void Attract(DropableItem item, Action onDollarAttracted)
    {
        item.DisableGravity();
        Attract(item.transform, onDollarAttracted);
    }
    public void Attract(Transform item, Action onDollarAttracted)
    {
        StartCoroutine(Animate(item.transform, onDollarAttracted));
    }

    private IEnumerator Animate(Transform item, Action onAttracted)
    {
        item.DOComplete(true);
        item.DOShakeScale(_takeShakeDuration, 4f);

        yield return new WaitForSeconds(_takeShakeDuration);

        item.DOLocalRotate(Vector3.zero, _attractDuration);

        while (Vector3.SqrMagnitude(transform.position - item.position) > _followRange)
        {
            float clampedSpeed = Mathf.Clamp(_speed * Time.deltaTime, 0, 1);
            var targetOffset = (transform.position - item.transform.position).normalized * _targetOffsetMagnitude;
            item.position = Vector3.Lerp(item.transform.position, transform.position + targetOffset, clampedSpeed);

            yield return null;
        }

        if (!_reduceScale)
        {
            onAttracted?.Invoke();
            yield break;
        }

        item.DOScale(0, _scaleReduceDuration).OnComplete(() =>
        {
            item.DOComplete(true);
            onAttracted?.Invoke();
        });
    }
}