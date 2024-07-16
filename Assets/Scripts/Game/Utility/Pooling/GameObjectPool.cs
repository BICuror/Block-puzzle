using UnityEngine;
using System.Collections.Generic;

namespace Utility
{
    public abstract class GameObjectPool<T>: MonoBehaviour where T: PoolObject
    {   
        [SerializeField] private Transform _poolObjectParent; 
        [SerializeField] private int _initialPoolSize;
        private T _objectPrefab;
        private List<T> _pool;
        private int _pointer;
    
        public void InstantiatePool(T prefab)
        {
            _objectPrefab = prefab;

            _pool = new List<T>();

            for (int i = 0; i < _initialPoolSize; i++)
            {   
                T newPooledObject = CreatePooledObject();

                newPooledObject.gameObject.SetActive(false);   
            }
        } 
        
        public T GetPooledObject()
        {
            MovePointer();

            if (HasFreeElement(out T element))
            {
                element.InitPoolObject();

                return element;
            }
            else 
            {
                return CreatePooledObject();
            }
        }   
        
        private T CreatePooledObject()
        {
            T newPooledObject = Instantiate(_objectPrefab, Vector3.zero, Quaternion.identity, _poolObjectParent);

            newPooledObject.SetPoolObjectOwnerTransform(_poolObjectParent);

            _pool.Add(newPooledObject);    
        
            return _pool[_pool.Count - 1];
        }

        private void MovePointer()
        {
            _pointer += 1;

            if (_pointer >= _pool.Count) _pointer = 0;
        }

        private bool HasFreeElement(out T element)
        {
            for (int i = 0; i < _pool.Count; i++)
            {
                int currentPointer = _pointer + i;

                if (currentPointer >= _pool.Count) currentPointer -= _pool.Count;

                if (_pool[currentPointer].IsBeingUsed == false)
                {
                    element = _pool[currentPointer];
                
                    return true;
                }
            }

            element = null;

            return false;
        }
    }
}
