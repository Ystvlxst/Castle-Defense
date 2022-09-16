using UnityEngine;

namespace Source.UI.EnemyPointersUI
{
    public class PointerBehaviour : MonoBehaviour, IPointerBehaviour
    {
        [SerializeField] private RectTransform _boundaries;
        
        private Pointer _onScreenPointer;
        private BlendRotation _pointerBlendRotation;
        private BlendPointer _pointerBlendMovement;

        public void Construct(Camera camera)
        {
            IPointerRotation pointerDirectRotation = new PointerDirectRotation(transform);
            IPointerRotation fixedPointerRotation = new FixedPointerRotation(transform, Quaternion.Euler(0, 0, -90));
            IPointerMovement onBoundariesPointer = new OnBoundariesPointer(_boundaries, transform);
            IPointerMovement pointerMovement = new OnScreenPointer(transform);
            _pointerBlendRotation = new BlendRotation(fixedPointerRotation, pointerDirectRotation, transform);
            _pointerBlendMovement = new BlendPointer(pointerMovement, onBoundariesPointer, transform);
            _onScreenPointer = new Pointer(_pointerBlendMovement, _pointerBlendRotation, camera);
        }

        public void Point(Vector3 playerPosition, Vector3 targetPosition)
        {
            var blend = (Vector3.Distance(_onScreenPointer.GetViewportPosition(playerPosition),
                _onScreenPointer.GetViewportPosition(targetPosition)) - 0.4f) * 5f;

            blend = Mathf.Clamp(blend, 0, 1);

            _pointerBlendRotation.SetBlendValue(blend);
            _pointerBlendMovement.SetBlendValue(blend);
            _onScreenPointer.TryPoint(playerPosition, targetPosition);
        }
    }
}