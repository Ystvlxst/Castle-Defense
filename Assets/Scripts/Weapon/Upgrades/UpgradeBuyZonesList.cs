using BabyStack.Model;
using UnityEngine;

public class UpgradeBuyZonesList : MonoBehaviour
{
    [SerializeField] private BuyZonePresenter _buyZoneTemplate;
    [SerializeField] private UpgradeUnlockable<float> _upgradeUnlockable;
    
    private BuyZonePresenter _buyZonePresenter;

    private void Start()
    {
        TryLoadNextBuyZone();
    }

    private void TryLoadNextBuyZone()
    {
        if (!HasNextModification(out ModificationData<float> modification)) 
            return;
        
        _buyZonePresenter = Instantiate(_buyZoneTemplate, transform);
        _buyZonePresenter.Init(new BuyZone(modification.Price), _upgradeUnlockable);
        _buyZonePresenter.Unlocked += OnBuyZoneUnlocked;
    }

    private void OnBuyZoneUnlocked(BuyZonePresenter buyZonePresenter)
    {
        _buyZonePresenter.Unlocked -= OnBuyZoneUnlocked;
        Destroy(_buyZonePresenter.gameObject);
        TryLoadNextBuyZone();
    }

    private bool HasNextModification(out ModificationData<float> modification)
    {
        modification = default;

        Modification<float> unlockableModification = _upgradeUnlockable.Modification;

        if (!unlockableModification.TryGetNextModification(out ModificationData<float> newModification))
            return false;
        
        modification = newModification;
        return true;
    }
}