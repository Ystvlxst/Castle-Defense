using UnityEngine;

internal class NullInput : MonoBehaviour, IInputSource
{
    public Vector3 Destination { get; set; }
    
    private void Update() => 
        Destination = transform.position;
}