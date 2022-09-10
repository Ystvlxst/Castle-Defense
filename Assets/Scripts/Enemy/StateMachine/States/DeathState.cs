using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class DeathState : State
{
    [SerializeField] private Collider[] _ragdollColliders;
    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private LootDrop _lootDrop;
    [SerializeField] private Rigidbody[] _ragdollRigidbodyes;

    private void Awake()
    {
        if (_ragdollRigidbodyes.Length == 0)
            return;

        foreach (Rigidbody rigidbody in _ragdollRigidbodyes)
        {
            rigidbody.isKinematic = true;
            rigidbody.detectCollisions = false;
        }
    }

    private void OnEnable()
    {
        StartCoroutine(Death());
    }

#if UNITY_EDITOR
    [ContextMenu(nameof(SetupComponents))]
    public void SetupComponents()
    {
        _ragdollColliders = GetComponentsInChildren<Collider>()
            .Where(colllider => colllider != GetComponent<Collider>()).ToArray();

        _ragdollRigidbodyes = GetComponentsInChildren<Rigidbody>()
            .Where(colllider => colllider != GetComponent<Rigidbody>()).ToArray();
    }
#endif

    private IEnumerator Death()
    {
        _animator.enabled = false;
        _navMeshAgent.speed = 0;

        foreach (Rigidbody rigidbody in _ragdollRigidbodyes)
        {
            rigidbody.isKinematic = false;
            rigidbody.detectCollisions = true;
        }

        _lootDrop.DropLoot();

        yield return new WaitForSeconds(6);

        foreach (Collider collider in _ragdollColliders)
            collider.isTrigger = true;

        Destroy(gameObject, 2f);
    }
}