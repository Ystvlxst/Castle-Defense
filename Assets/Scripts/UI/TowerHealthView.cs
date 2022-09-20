using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class TowerHealthView : MonoBehaviour
{
    private const string _low = "Low";

    [SerializeField] private Tower _player;
    [SerializeField] private Animator _animator;
    [SerializeField] private Image _image;

    private Slider _slider;
    private Color _startFillColor;

    private void Awake()
    {
        _slider = GetComponent<Slider>();

        _slider.maxValue = _player.Health;

        _startFillColor = _image.color;
    }

    private void Update()
    {
        _slider.value = _player.CurrentHealth;

        if (_player.CurrentHealth <= _player.LowestHealth)
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
