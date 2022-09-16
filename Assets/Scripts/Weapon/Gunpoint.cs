using System;
using UnityEngine;

public class Gunpoint : MonoBehaviour
{
    public event Action Shot;
    
    public void Shoot(Bullet bulletTemplate, float force, float torqueForce)
    {
        Bullet bullet = Instantiate(bulletTemplate, transform.position, transform.rotation);
        bullet.Rigidbody.velocity = transform.forward * force;
        bullet.Rigidbody.AddTorque(transform.forward * torqueForce);
        Shot?.Invoke();
    }
}