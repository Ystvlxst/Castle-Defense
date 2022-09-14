using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class StackHolder : StackView
{
    [SerializeField] private float _offsetY;
    [SerializeField] private float _sortMoveDuration = 2f;

    private Vector3 _lastAddedScale;
    private int _lastChildCount;

    protected override Vector3 CalculateAddEndPosition(Transform container, Transform stackable)
    {
        var stackableLocalScale = stackable.localScale;
        var endPosition = new Vector3(0, stackableLocalScale.y / 2, 0);

        _lastChildCount = container.childCount;
        _lastAddedScale = new Vector3(0, stackable.localScale.y / 2f, 0);
        

        return Vector3.zero;
    }

    protected override void Sort(List<Transform> unsortedTransforms)
    {
        var sortedList = unsortedTransforms.OrderBy(transform => transform.localPosition.y);
        var position = Vector3.zero + _lastAddedScale;

        foreach (var item in sortedList)
        {
            position.y += item.localScale.y / 2;

            item.transform.DOComplete(true);
            item.transform.DOLocalMove(position, _sortMoveDuration);

            position.y += item.localScale.y / 2 + _offsetY;
        }
    }

    private Transform FindTopStackable(Transform container)
    {
        Transform topStackable = container.GetChild(0);

        foreach (Transform stackable in container.GetComponentsInChildren<Transform>())
            if (topStackable.position.y < stackable.position.y)
                topStackable = stackable;

        return topStackable;
    }
}
