using DG.Tweening;
using UnityEngine;

public class DOTweenSettings : MonoBehaviour
{
    private void Awake() => 
        DOTween.SetTweensCapacity(500, 50);
}