using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _spawn;
    [SerializeField] private Transform _weaponTransform;
    [SerializeField] private Bullet _template;
    [SerializeField] private TargetSelector _targetSelector;
    [SerializeField] private StackPresenter _stackPresenter;
    [SerializeField] private float _cooldown;
    [SerializeField] private int _shotsPerAmmo;
    [SerializeField] private ParticleSystem _shotEffect;

    private float _cooldownFactor = 1;
    private int _ammo;

    public Transform Spawn => _spawn;
    public Transform WeaponTransform => _weaponTransform;
    public Bullet Template => _template;
    public TargetSelector TargetSelector => _targetSelector;
    public StackPresenter StackPresenter => _stackPresenter;
    public float Cooldown => _cooldown;
    public int ShotsPerArmo => _shotsPerAmmo;
    public ParticleSystem ShotEffect => _shotEffect;
    public float CooldownFactor => _cooldownFactor;


    public void OnModificationUpdate(float value) =>
        _cooldownFactor = value;

    public bool CanShoot() =>
    _ammo > 0 && TargetSelector.HasTarget;

    public bool CanRefillAmmo() =>
    StackPresenter.Empty == false && _ammo == 0;

    public void RefillAmmo()
    {
        Stackable stackable = StackPresenter.Data.Last();
        StackPresenter.RemoveFromStack(stackable);
        Destroy(stackable.gameObject);
        _ammo = ShotsPerArmo;
    }

    public void MinusAmmo() =>
        _ammo--;
}