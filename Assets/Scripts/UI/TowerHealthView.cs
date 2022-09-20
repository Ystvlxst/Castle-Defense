using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class TowerHealthView : MonoBehaviour
{
    private const string _low = "Low";

    [SerializeField] private Tower _tower;
    [SerializeField] private Animator _animator;
    [SerializeField] private Image _image;

    private Slider _slider;
    private Color _startFillColor;

    private void OnEnable()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = _tower.Health;
        _slider.value = _slider.maxValue;
        _startFillColor = _image.color;

        _tower.Damaged += CheckHealth;
    }

    private void OnDisable()
    {
        _tower.Damaged -= CheckHealth;
    }

    private void CheckHealth(int damage)
    {
        _slider.value = _tower.CurrentHealth;

        if (_tower.CurrentHealth <= _tower.LowestHealth)
        {
            _animator.SetTrigger(_low);
            _image.color = Color.red;
        }
        else
        {
            _animator.ResetTrigger(_low);
            _image.color = _startFillColor;
        }
    }
}
