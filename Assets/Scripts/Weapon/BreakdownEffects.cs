using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BreakdownStatus))]
public class BreakdownEffects : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> _effects;
    
    private BreakdownStatus _breakdownStatus;

    private void Awake() => 
        _breakdownStatus = GetComponent<BreakdownStatus>();

    private void OnEnable()
    {
        _breakdownStatus.Broke += OnBroke;
        _breakdownStatus.Repaired += OnRepaired;
    }
    
    private void OnDisable()
    {
        _breakdownStatus.Broke -= OnBroke;
        _breakdownStatus.Repaired -= OnRepaired;
    }

    private void OnRepaired()
    {
        foreach (ParticleSystem effect in _effects) 
            effect.Stop();
    }

    private void OnBroke()
    {
        foreach (ParticleSystem effect in _effects) 
            effect.Play();
    }
}