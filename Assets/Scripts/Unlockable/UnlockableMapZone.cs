using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(MapZoneCamera))]
public class UnlockableMapZone : UnlockableObject
{
    [SerializeField] private GameObject _mapRoot;
    [SerializeField] private float _unlockDuration = 1f;
    [SerializeField] private NavMeshSurface _navMeshSurface;

    private MapZoneCamera _mapZoneCamera;

    private void Awake()
    {
        _mapZoneCamera = GetComponent<MapZoneCamera>();
    }

    public override GameObject Unlock(Transform parent, bool onLoad, string guid)
    {
        _mapRoot.SetActive(true);

        if (onLoad == false)
        {
            _mapRoot.transform.localScale = Vector3.zero;
            _mapRoot.transform.DOScale(1f, _unlockDuration).OnComplete(() => _navMeshSurface.BuildNavMesh());
            _mapZoneCamera.Show();
            return _mapRoot;
        }

        _navMeshSurface.BuildNavMesh();

        return _mapRoot;
    }
}
