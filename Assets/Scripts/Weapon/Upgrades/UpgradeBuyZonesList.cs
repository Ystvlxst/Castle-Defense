using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeBuyZonesList : MonoBehaviour
{
    private List<BuyZonePresenter> _buyZones;

    private void Start()
    {
        _buyZones = GetComponentsInChildren<BuyZonePresenter>().ToList();

        DisableLocked();
        ActivateFirstLocked();
    }

    private void DisableLocked()
    {
        foreach (BuyZonePresenter buyZonePresenter in _buyZones)
            buyZonePresenter.gameObject.SetActive(buyZonePresenter.IsUnlocked);
    }

    private void ActivateFirstLocked()
    {
        foreach (var buyZonePresenter in _buyZones.Where(buyZonePresenter => buyZonePresenter.IsUnlocked == false))
        {
            buyZonePresenter.gameObject.SetActive(true);
            buyZonePresenter.Unlocked += OnUnlocked;

            return;
        }
    }

    private void OnUnlocked(BuyZonePresenter buyZonePresenter)
    {
        buyZonePresenter.Unlocked -= OnUnlocked;
        ActivateFirstLocked();
    }
}