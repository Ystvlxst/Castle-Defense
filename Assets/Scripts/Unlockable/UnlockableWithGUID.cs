using UnityEngine;

public abstract class UnlockableWithGUID : MonoBehaviour
{
    public abstract GameObject Unlock(Transform parent, bool onLoad, string guid);
}
