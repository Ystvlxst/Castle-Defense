using UnityEngine;

[RequireComponent(typeof(StackPresenter))]
public class StartAmmoLoader : MonoBehaviour
{
    [SerializeField] private int _amount;
    [SerializeField] private Stackable _template;

    private StackPresenter _stackPresenter;

    private void Start()
    {
        _stackPresenter = GetComponent<StackPresenter>();

        for (int i = 0; i < _amount; i++)
        {
            Stackable ammo = Instantiate(_template);
            _stackPresenter.AddToStack(ammo);
        }
    }
}
