using UnityEngine;

public class WeaponJoint : MonoBehaviour
{
    [SerializeField] private bool _x;
    [SerializeField] private bool _y;
    [SerializeField] private float _maxDegreesDelta = 40f;
    [SerializeField] private float _maxMagnitudeDelta = 40f;
    
    private Vector3 _rotation;
    private float _minDelta = 0.001f;

    public void Rotate(Vector3 targetDirection)
    {
        GetTargetRotation(TargetDirection(targetDirection));
    }

    public bool LooksAt(Vector3 targetDirection)
    {
        targetDirection = TargetDirection(targetDirection);
        return Vector3.SqrMagnitude(targetDirection.normalized - _rotation.normalized) < _minDelta;
    }

    private Vector3 TargetDirection(Vector3 targetDirection)
    {
        if (!_y) 
            targetDirection = Vector3.ProjectOnPlane(targetDirection, transform.right);

        if (!_x) 
            targetDirection = Vector3.ProjectOnPlane(targetDirection, transform.up);

        return targetDirection;
    }

    private void GetTargetRotation(Vector3 targetDirection)
    {
        _rotation = Vector3.RotateTowards(transform.forward, targetDirection,
            _maxDegreesDelta * Mathf.Deg2Rad * Time.deltaTime, _maxMagnitudeDelta);


        transform.rotation = Quaternion.LookRotation(_rotation);
    }
}