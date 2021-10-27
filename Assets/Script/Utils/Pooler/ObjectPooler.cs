using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pooler
{
    /// <summary>
    /// An object pooling system. We use a dictionary so you can have
    /// a pool of multiple different types of game object prefabs.
    /// 
    /// @author Shifat Khan
    /// @version 2.0.0
    /// </summary>
    public class ObjectPooler : Singleton<ObjectPooler>
    {
        #region Fields
        /// <summary>
        /// A container class that holds a list of variant objects
        /// for a specific type of prefab.
        /// </summary>
        [System.Serializable]
        public class Pool
        {
            public string Tag;
            public List<GameObject> Variants;
            public int Size;
        }

        public List<Pool> Pools;
        public Dictionary<string, Queue<GameObject>> PoolDictionary;
        #endregion

        #region Monobehaviour functions
        private void Start()
        {
            // Set up the pools.
            PoolDictionary = new Dictionary<string, Queue<GameObject>>();

            int prefabIndex = -1;

            foreach (var pool in Pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                for (int i = 0; i < pool.Size; i++)
                {
                    prefabIndex = Random.Range(0, pool.Variants.Count);

                    GameObject obj = Instantiate(pool.Variants[prefabIndex], transform);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                PoolDictionary.Add(pool.Tag, objectPool);
            }
        }
        #endregion

        #region Pooling functions
        /// <summary>
        /// Spawns a game object from the pool queue.
        /// </summary>
        /// <param name="tag">The type of pool you want to spawn from.</param>
        /// <param name="position">Position to spawn at.</param>
        /// <param name="scale">Scale to spawn at.</param>
        /// <param name="rotation">Rotation to spawn at.</param>
        /// <param name="autoEnqueue">Do we automatically re-enqueue the object?</param>
        /// <returns>Spawned gameobject.</returns>
        public GameObject SpawnFromPool(string tag, Vector3 position, Vector3 scale, Quaternion rotation, bool autoEnqueue = true)
        {
            if (!PoolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning($"Pool with tag {tag} does not exist.");
                return null;
            }

            // If pool is empty, create more.
            if (PoolDictionary[tag].Count == 0)
            {
                var objectsFromPool = Pools.Find(x => x.Tag == tag).Variants;
                var prefabIndex = Random.Range(0, objectsFromPool.Count);
                GameObject obj = Instantiate(objectsFromPool[prefabIndex], transform);
                obj.SetActive(false);
                PoolDictionary[tag].Enqueue(obj);
            }

            GameObject objectToSpawn = PoolDictionary[tag].Dequeue();

            objectToSpawn.SetActive(false);
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.localScale = scale;
            objectToSpawn.transform.rotation = rotation;

            IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();
            if (pooledObj != null)
                pooledObj.OnObjectSpawned();

            if (autoEnqueue)
                PoolDictionary[tag].Enqueue(objectToSpawn); // Do we want to enqueue only if the object was manually destroyed.

            return objectToSpawn;
        }

        /// <summary>
        /// Gets a game object from the pool queue.
        /// </summary>
        /// <param name="tag">The type of pool you want to get from.</param>
        /// <returns>Gameobject from pool.</returns>
        public GameObject GetFromPool(string tag)
        {
            if (!PoolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning($"Pool with tag {tag} does not exist.");
                return null;
            }

            GameObject objectToSpawn = PoolDictionary[tag].Dequeue();
            PoolDictionary[tag].Enqueue(objectToSpawn);

            return objectToSpawn;
        }
        #endregion
    }
}
