using UnityEngine;

namespace Source.UI.EnemyPointersUI
{
    public interface IPointerBehaviour
    {
        void Construct(Camera camera);
        void Point(Vector3 playerPosition, Vector3 targetPosition);
    }
}