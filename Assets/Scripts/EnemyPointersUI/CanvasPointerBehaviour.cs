using System.Linq;
using UnityEngine;

namespace Source.UI.EnemyPointersUI
{
    internal class CanvasPointerBehaviour : MonoBehaviour, IPointerBehaviour
    {
        private IPointerBehaviour _pointerBehaviour;

        public void Construct(Camera camera)
        {
            _pointerBehaviour = GetComponentsInChildren<IPointerBehaviour>()
                .FirstOrDefault(behaviour => !ReferenceEquals(behaviour, this));

            _pointerBehaviour.Construct(camera);
        }

        public void Point(Vector3 playerPosition, Vector3 targetPosition) =>
            _pointerBehaviour.Point(playerPosition, targetPosition);
    }
}