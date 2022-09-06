using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballistics : MonoBehaviour
{
    [SerializeField] private Transform _spawn;
    [SerializeField] private float _angle;
    [SerializeField] private Bullet _template;
    [SerializeField] private TargetSelector _targetSelector;

    private float _g = Physics.gravity.y;

    private void Update()
    {
        _spawn.localEulerAngles = new Vector3(-_angle, 0f, 0f);

        if (Input.GetMouseButtonDown(0))
        {
            Shot();
        }
       
    }

    private void Shot()
    {
        Vector3 fromTo = _targetSelector.SelectTarget() - transform.position;
        Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);

        transform.rotation = Quaternion.LookRotation(fromToXZ, Vector3.up);

        float x = fromToXZ.magnitude;
        float y = fromTo.y;

        float angleInRadians = _angle * Mathf.PI / 180;

        float v2 = (_g * x * x) / (2 * (y - Mathf.Tan(angleInRadians) * x) * Mathf.Pow(Mathf.Cos(angleInRadians), 2));
        float v = Mathf.Sqrt(Mathf.Abs(v2));

        Bullet bullet = Instantiate(_template, _spawn.position, Quaternion.identity);
        bullet.Rigidbody.velocity = _spawn.forward * v;
    }
}