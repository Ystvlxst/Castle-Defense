using UnityEngine;

internal class DroneMovement : MonoBehaviour, IMovement
{
    [SerializeField] private float _speed;

    public void Move(Vector3 rawDirection)
    {
        transform.Translate(rawDirection * Time.deltaTime * _speed);
    }

    public void Stop()
    {
        
    }
}