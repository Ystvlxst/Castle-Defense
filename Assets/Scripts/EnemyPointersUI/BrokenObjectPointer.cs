using UnityEngine;

namespace Source.UI.EnemyPointersUI
{
    public class BrokenObjectPointer : MonoBehaviour
    {
        [SerializeField] private BreakdownStatus _breakdownStatus;
        [SerializeField] private CanvasPointerBehaviour _pointerBehaviourTemplate;
        [SerializeField] private Transform _pointerPosition;
        
        private Camera _camera;
        private CanvasPointerBehaviour _pointerBehaviour;
        private Player _player;

        public void Init(Player player) => 
            _player = player;

        private void Awake() => 
            _camera = Camera.main;

        private void OnEnable()
        {
            _breakdownStatus.Broke += OnBroke;
            _breakdownStatus.Repaired += OnRepaired;
        }
        
        public void LateUpdate() => 
            UpdatePointer();

        private void OnDisable()
        {
            _breakdownStatus.Broke -= OnBroke;
            _breakdownStatus.Repaired -= OnRepaired;
        }

        private void OnBroke()
        {
            _pointerBehaviour = Instantiate(_pointerBehaviourTemplate);
            _pointerBehaviour.Construct(_camera);
        }

        private void OnRepaired() => 
            Destroy(_pointerBehaviour.gameObject);

        private void UpdatePointer()
        {
            if(_pointerBehaviour != null)
                _pointerBehaviour.Point(_player.transform.position, _pointerPosition.position);
        }
    }
}