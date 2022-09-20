using System.Collections;
using UnityEngine;

public class DroneShooter : MonoBehaviour
{
    [SerializeField] private float _cooldown;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform[] _shootPoints;
    [SerializeField] private float _radius;
    [SerializeField] private DroneMissile _droneMissileTemplate;
    
    private float _maxDistance = 20;

    private void Start()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            foreach (Transform shootPoint in _shootPoints)
            {
                yield return new WaitForSeconds(_cooldown);

                RaycastHit[] results = new RaycastHit[1];

                if (Physics.SphereCastNonAlloc(transform.position, _radius, Vector3.down, results, _maxDistance, _layerMask) <= 0) 
                    continue;
                
                DroneMissile missile = Instantiate(_droneMissileTemplate, shootPoint.position, shootPoint.rotation);
                missile.SetTarget(results[0].transform);
            }
        }
    }
}