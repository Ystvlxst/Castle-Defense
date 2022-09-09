using BabyStack.Model;
using UnityEngine;

public class PlayerSpeedModificationPresenter : ModificationPresenter<PlayerSpeedRateModification, float>
{
    [SerializeField] private CharacterMovement _movement;

    protected override void Enabled()
    {
        AddListener(_movement);
    }
}