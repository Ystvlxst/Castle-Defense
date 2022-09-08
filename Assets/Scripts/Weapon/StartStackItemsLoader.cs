using UnityEngine;

[RequireComponent(typeof(StackPresenter))]
public class StartStackItemsLoader : MonoBehaviour
{
    [SerializeField] private int _amount;
    [SerializeField] private Stackable _template;
    [SerializeField] private LoadTime _loadTime;

    private StackPresenter _stackPresenter;

    private void Awake()
    {
        if(_loadTime == LoadTime.OnAwake)
            Load();
    }
    
    private void Start()
    {
        if(_loadTime == LoadTime.OnStart)
            Load();
    }

    private void Load()
    {
        _stackPresenter = GetComponent<StackPresenter>();

        for (int i = 0; i < _amount; i++)
        {
            Stackable ammo = Instantiate(_template);
            _stackPresenter.AddToStack(ammo);
        }
    }
}

internal enum LoadTime
{
    OnAwake,
    OnStart
}
