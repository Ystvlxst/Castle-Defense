using System.Collections;
using UnityEngine;

public class LootDrop : MonoBehaviour
{
    [SerializeField] private DropableItem _droppableItem;
    [SerializeField] private Vector3 _spawnOffset = new Vector3(0.38f, 2.2f, 0.0f);
    [SerializeField] private Vector2 _amountRange;
    [SerializeField] private float _spawnDelayBetweenItems = 0.1f;

    private float _force = 6f;
    private Vector3 _spawnPosition => transform.position + _spawnOffset  + Random.insideUnitSphere * 0.5f;

    public void DropLoot()
    {
        StartCoroutine(DropLootOverTime());
    }

    private IEnumerator DropLootOverTime()
    {
        int count = Random.Range((int)_amountRange.x, (int)_amountRange.y + 1);

        for (int i = 0; i < count; i++)
        {
            DropableItem spawnedDollar = Instantiate(_droppableItem, _spawnPosition, Random.rotation);
            spawnedDollar.Push(GetRandomDirection() * _force);

            if(_spawnDelayBetweenItems != 0)
                yield return new WaitForSeconds(_spawnDelayBetweenItems);
        }
    }

    private Vector3 GetRandomDirection()
    {
        var direction = Random.insideUnitSphere;
        direction.y = Mathf.Abs(direction.y);
        direction.y += 10f;
        
        return direction.normalized;
    }
}