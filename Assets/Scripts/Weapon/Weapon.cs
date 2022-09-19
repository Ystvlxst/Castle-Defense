using System.Collections.Generic;
using System.Linq;
using BabyStack.Model;
using UnityEngine;

public class Weapon : MonoBehaviour, IModificationListener<float>
{
    [SerializeField] private List<Gunpoint> _gunpoints;
    [SerializeField] private TargetSelector _targetSelector;
    [SerializeField] private StackPresenter _stackPresenter;
    [SerializeField] private int _shotsPerAmmo;
    [SerializeField] private float _shootDistance;
    [SerializeField] private BreakdownStatus _breakdown;
    [SerializeField] private List<WeaponJoint> _weaponJoints;

    private float _upgradeFactor = 1;
    private int _ammo;
    private Gunpoint _currentGunpoint;

    protected TargetSelector TargetSelector => _targetSelector;
    private StackPresenter StackPresenter => _stackPresenter;
    protected float UpgradeFactor => _upgradeFactor;
    protected float ShotsPerAmmo => _shotsPerAmmo;

    public float ShootDistance => _shootDistance + (_upgradeFactor - 1) * _shootDistance * 2f;
    public BreakdownStatus Breakdown => _breakdown;
    public int Ammo => _stackPresenter.Count;

    private void Awake() =>
        _currentGunpoint = _gunpoints.First(point => point.gameObject.activeSelf);

    public void AddAmmo(int shotsPerAmmo) => 
        _ammo += shotsPerAmmo;

    public void OnModificationUpdate(float value) =>
        _upgradeFactor = value;

    public bool CanShoot() =>
        _ammo > 0 && TargetSelector.HasTarget(ShootDistance) && !Breakdown.Broken;

    protected bool CanRefillAmmo() =>
        StackPresenter.Empty == false && _ammo == 0;

    protected void RefillAmmo(int shotsPerAmmo)
    {
        Stackable stackable = StackPresenter.Data.Last();
        StackPresenter.RemoveFromStack(stackable);
        Destroy(stackable.gameObject);
        AddAmmo(shotsPerAmmo);
    }

    protected Gunpoint SelectGunpoint()
    {
        Gunpoint nextGunpoint =
            _gunpoints.FirstOrDefault(point => point.isActiveAndEnabled && point != _currentGunpoint);

        if (nextGunpoint == null)
            return _currentGunpoint;

        _gunpoints.Remove(_currentGunpoint);
        _gunpoints.Add(_currentGunpoint);
        _currentGunpoint = nextGunpoint;

        return _currentGunpoint;
    }

    protected void SpendAmmo() =>
        _ammo--;

    protected void Aim(Vector3 targetDirection)
    {
        foreach (WeaponJoint joint in _weaponJoints)
            joint.Rotate(targetDirection);
    }

    protected bool Aimed(Vector3 targetDirection) =>
        _weaponJoints.All(joint => joint.LooksAt(targetDirection));
}