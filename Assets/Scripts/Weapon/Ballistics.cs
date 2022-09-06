using System.Collections;
using System.Linq;
using UnityEngine;

public class Ballistics : MonoBehaviour
{
    [SerializeField] private Transform _spawn;
    [SerializeField] private float _angle;
    [SerializeField] private Bullet _template;
    [SerializeField] private TargetSelector _targetSelector;
    [SerializeField] private StackPresenter _stackPresenter;
    [SerializeField] private float _cooldown;

    private readonly float _g = Physics.gravity.y;

    private void Start() => 
        StartCoroutine(Shoot());

    private IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitUntil(() => _stackPresenter.Empty == false);
            Shot();
            yield return new WaitForSeconds(_cooldown);
        }
    }

    private void Shot()
    {
        _spawn.localEulerAngles = new Vector3(-_angle, 0f, 0f);

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

        Stackable stackable = _stackPresenter.Data.Last();
        _stackPresenter.RemoveFromStack(stackable);
        Destroy(stackable.gameObject);
    }
}
