using DG.Tweening;
using System.Collections;
using UnityEngine;

public class StackableProducerUnlocker : MonoBehaviour
{
    [SerializeField] private Stackable _stackable;
    [SerializeField] private UnlockRule _unlockRule;

    [SerializeField] private bool _needShow = true;

    public StackableType Type => _stackable.Type;
    public bool Unlocked => _unlockRule.CanUnlock;

    private void Awake()
    {
        if (Unlocked)
            return;

        _unlockRule.AddUpdateListener(OnUpdate);
        gameObject.SetActive(false);
        transform.localScale = Vector3.zero;
    }

    private void OnDestroy()
    {
        if (Unlocked == false)
            _unlockRule.RemoveUpdateListener(OnUpdate);
    }

    private void OnUpdate()
    {
        if (_unlockRule.CanUnlock == false)
            return;

        _unlockRule.RemoveUpdateListener(OnUpdate);
        gameObject.SetActive(true);
    }
}
