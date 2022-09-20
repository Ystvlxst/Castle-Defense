using TMPro;
using UnityEngine;

[RequireComponent(typeof(StackPresenter))]
public class StackCountView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    
    private StackPresenter _stackPresenter;

    private void Awake() => 
        _stackPresenter = GetComponent<StackPresenter>();

    private void OnEnable()
    {
        _stackPresenter.Removed += OnCountChanged;
        _stackPresenter.Added += OnCountChanged;
    }

    private void OnDisable()
    {
        _stackPresenter.Removed += OnCountChanged;
        _stackPresenter.Added += OnCountChanged;
    }

    private void OnCountChanged(Stackable _) => 
        _text.text = _stackPresenter.Count.ToString();
}