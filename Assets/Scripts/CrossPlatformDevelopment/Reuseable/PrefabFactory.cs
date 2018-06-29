using System.Collections.Generic;
using UnityEngine;

namespace CrossPlatformDevelopment.Reuseable
{
    public abstract class PrefabFactory : MonoBehaviour
    {
        [SerializeField] protected List<GameObject> objectPool;

        [SerializeField] protected GameObject prefab;
        protected abstract void SpawnPrefab();
        protected abstract void AddToPool(GameObject go);
        protected abstract void RemoveFromPool(GameObject go);
    }
}