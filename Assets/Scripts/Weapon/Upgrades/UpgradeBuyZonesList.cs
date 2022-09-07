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
    }

    private void DisableLocked()
    {
        for (var i = 1; i < _buyZones.Count; i++) 
            _buyZones[i].gameObject.SetActive(_buyZones[i].IsUnlocked);
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