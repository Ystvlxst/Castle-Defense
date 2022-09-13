using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float _towerHealthFactor;
    [SerializeField] private Transform _spawn;
    [SerializeField] private Transform _weaponTransform;
    [SerializeField] private TargetSelector _targetSelector;
    [SerializeField] private StackPresenter _stackPresenter;
    [SerializeField] private int _shotsPerAmmo;
    [SerializeField] private ParticleSystem _shotEffect;
    [SerializeField] private float _shootDistance;

    private Tower _tower;
    private float _upgradeFactor = 1;
    private int _ammo;

    protected Transform Spawn => _spawn;
    protected Transform WeaponTransform => _weaponTransform;
    protected TargetSelector TargetSelector => _targetSelector;
    private StackPresenter StackPresenter => _stackPresenter;
    protected ParticleSystem ShotEffect => _shotEffect;
    protected float UpgradeFactor => _upgradeFactor;
    protected float ShotsPerAmmo => _shotsPerAmmo;
    public float ShootDistance => _shootDistance;

    private void Awake() =>
        _tower = FindObjectOfType<Tower>();

    public void OnModificationUpdate(float value) =>
        _upgradeFactor = value;

    protected bool CanShoot() =>
        _ammo > 0 && TargetSelector.HasTarget(_shootDistance) && _tower.CurrentHealth >= _tower.Health - _towerHealthFactor;

    protected bool CanRefillAmmo() =>
        StackPresenter.Empty == false && _ammo == 0;

    protected void RefillAmmo(int shotsPerAmmo)
    {
        Stackable stackable = StackPresenter.Data.Last();
        StackPresenter.RemoveFromStack(stackable);
        Destroy(stackable.gameObject);
        _ammo += shotsPerAmmo;
    }

    protected void MinusAmmo() =>
        _ammo--;
}