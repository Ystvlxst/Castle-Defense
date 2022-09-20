using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class TowerHealthView : MonoBehaviour
{
    private const string _low = "Low";
    private const string _full = "Full";

    [SerializeField] private Tower _tower;
    [SerializeField] private Animator _animator;
    [SerializeField] private Image _image;

    private Slider _slider;
    private Color _startFillColor;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = _tower.Health;
        _slider.value = _slider.maxValue;
        _startFillColor = _image.color;
    }

    private void Update()
    {
        _slider.value = _tower.CurrentHealth;

        if (_tower.IsLow)
        {
            _animator.SetTrigger(_low);
            _image.color = Color.red;
        }

        if(_tower.IsFull)
        {
            _animator.SetTrigger(_full);
            _image.color = _startFillColor;
        }
    }
}
