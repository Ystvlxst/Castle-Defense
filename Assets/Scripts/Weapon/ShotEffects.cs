using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Gunpoint))]
public class ShotEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem _shootEffect;
    [SerializeField] private List<Animation> _animations;

    private Gunpoint _gunpoint;

    private void Awake() => 
        _gunpoint = GetComponent<Gunpoint>();

    private void OnEnable() => 
        _gunpoint.Shot += OnShot;

    private void OnDisable() => 
        _gunpoint.Shot -= OnShot;

    private void OnShot()
    {
        _shootEffect.Play();

        foreach(Animation animation in _animations)
        {
            if (animation != null) 
                animation.Play();
        }
    }
}