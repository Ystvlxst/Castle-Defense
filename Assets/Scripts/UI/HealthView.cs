using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthView : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private CanvasGroup _healthCanvasGroup;

    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = _health.Amount;
    }

    private void OnEnable() => 
        _health.HealthChanged += OnHealthChanged;
    
    private void OnDisable() => 
        _health.HealthChanged += OnHealthChanged;

    private void OnHealthChanged(float amount)
    {
        _slider.value = amount;
        _healthCanvasGroup.DOComplete();
        _healthCanvasGroup.alpha = 1;
        Invoke(nameof(HideHealth), 1f);
    }

    private void HideHealth() => 
        _healthCanvasGroup.DOFade(0, 1f);
}
