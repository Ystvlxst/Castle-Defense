using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class TowerHealthView : MonoBehaviour
{
    private const string _low = "Low";

    [SerializeField] private Tower _player;
    [SerializeField] private Animator[] _animators;

    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();

        _slider.maxValue = _player.Health;
    }

    private void Update()
    {
        _slider.value = _player.CurrentHealth;

        if (_player.CurrentHealth <= _player.LowestHealth)
            SetTrigger();
        else
            ResetTrigger();
    }

    private void SetTrigger()
    {
        foreach(Animator animator in _animators)
            animator.SetTrigger(_low);
    }

    private void ResetTrigger()
    {
        foreach (Animator animator in _animators)
            animator.ResetTrigger(_low);
    }
}
