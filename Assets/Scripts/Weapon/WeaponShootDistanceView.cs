using System;
using UnityEngine;

public class WeaponShootDistanceView : MonoBehaviour
{
    private static readonly int Radius = Shader.PropertyToID("_Radius");
    
    [SerializeField] private Weapon _weapon;
    [SerializeField] private MoneyHolderTrigger _trigger;
    [SerializeField] private Projector _projector;
    [SerializeField] private float _animationSpeed;

    private MoneyHolder _inTrigger;
    private float _currentRadius;
    private Material _material => _projector.material;

    private void OnEnable()
    {
        _trigger.Enter += TriggerEnter;
        _trigger.Exit += TriggerExit;
    }

    private void Start()
    {
        SetFieldActive(false);
    }

    private void Update()
    {
        float targetRadius = _inTrigger == null ? 0 : _weapon.ShootDistance;
        _currentRadius = Mathf.Lerp(_currentRadius,targetRadius, _animationSpeed * Time.deltaTime);
        _material.SetFloat(Radius, _currentRadius);
    }

    private void OnDisable()
    {
        _trigger.Enter += TriggerEnter;
        _trigger.Exit += TriggerExit;
    }

    private void TriggerEnter(MoneyHolder moneyHolder)
    {
        if (_inTrigger != null) 
            return;

        _inTrigger = moneyHolder;
        ShowDistanceField();
    }

    private void ShowDistanceField()
    {
        _currentRadius = 0;
        SetFieldActive(true);
    }

    private void TriggerExit(MoneyHolder moneyHolder)
    {
        if (_inTrigger == moneyHolder) 
            _inTrigger = null;
        
        //SetFieldActive(false);
    }

    private void SetFieldActive(bool active)
    {
        _projector.enabled = active;
    }
}