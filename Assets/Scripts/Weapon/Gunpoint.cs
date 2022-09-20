using System;
using UnityEngine;

public class Gunpoint : MonoBehaviour
{
    public event Action Shot;
    
    public void Shoot(Bullet bulletTemplate, float force, float torqueForce)
    {
        Bullet bullet = Instantiate(bulletTemplate, transform.position, transform.rotation);
        bullet.Launch(transform.forward * force, transform.forward * torqueForce);
        Shot?.Invoke();
    }
}