using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropableItem : MonoBehaviour
{
    [SerializeField] private float _horizontalForce = 50;
    [SerializeField] private Collider _collider;

    private Rigidbody _rigidbody;
    private Coroutine _disable;

    protected virtual void Awake() => 
        _rigidbody = GetComponent<Rigidbody>();

    public void Push()
    {
        var random = Random.insideUnitCircle;
        var shift = new Vector3(random.x, 0, random.y);
        Push(Vector3.up + shift * _horizontalForce);
    }

    public void Push(Vector3 direction)
    {
        _rigidbody.AddForce(direction, ForceMode.VelocityChange);
        _disable = StartCoroutine(DisableBodyWhenReady());
    }

    public void DisableGravity()
    {
        if (_disable != null)
            StopCoroutine(_disable);
        
        _rigidbody.isKinematic = true;
        _rigidbody.useGravity = false;
        _collider.enabled = false;
        Destroy(_rigidbody);
        Destroy(_collider);
    }

    private IEnumerator DisableBodyWhenReady()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => _rigidbody.velocity == Vector3.zero);

        _rigidbody.isKinematic = true;
        _disable = null;
    }
}