using UnityEngine;

namespace Unlockable
{
    [RequireComponent(typeof(GUIDObject))]
    public class GUIDObjectUnlocker : UnlockableObject
    {
        [SerializeField] private UnlockableWithGUID _unlockableWithGuid;
        
        private GUIDObject _guidObject;

        private void Awake()
        {
            _guidObject = GetComponent<GUIDObject>();
        }

        public override GameObject Unlock(Transform parent, bool onLoad) => 
            _unlockableWithGuid.Unlock(parent, onLoad, _guidObject.GUID);
    }
}