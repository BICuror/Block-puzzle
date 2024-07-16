using UnityEngine;

namespace Utility
{
    public abstract class PoolObject : MonoBehaviour
    {
        private Transform _ownerTransform;

        public bool IsBeingUsed => transform.parent != _ownerTransform;

        public void InitPoolObject()
        {
            transform.SetParent(null);
        }

        public void SetPoolObjectOwnerTransform(Transform ownerTransform)
        {
            _ownerTransform = ownerTransform;
        }

        public void ReturnObjectToPool()
        {
            transform.SetParent(_ownerTransform);

            ResetObject();
        }

        protected abstract void ResetObject(); 
    }
}

