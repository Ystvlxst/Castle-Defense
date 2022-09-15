using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserBeamAnimation : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _scaleFactor;

    private LineRenderer _lineRenderer;
    private Vector2 _offset;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        _offset += Vector2.right * _speed * Time.deltaTime;
        _lineRenderer.sharedMaterial.mainTextureOffset = _offset;
        float xScale = Vector3.Distance(_lineRenderer.GetPosition(1), Vector3.zero);
        _lineRenderer.sharedMaterial.mainTextureScale = new Vector2(xScale * _scaleFactor, 1);
    }
}