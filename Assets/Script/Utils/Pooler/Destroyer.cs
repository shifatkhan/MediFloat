using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pooler
{
    /// <summary>
    /// This class will deactivate a game object 
    /// and put it back into the object pooler.
    /// 
    /// @author Shifat Khan
    /// @version 1.1.0
    /// </summary>
    public class Destroyer : MonoBehaviour
    {
        [Tooltip("Game Objects with this tag will be destroyed.")]
        [SerializeField] private List<string> tags;

        private ObjectPooler _objectPooler;

        private void Start()
        {
            _objectPooler = ObjectPooler.Instance;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (tags.Contains(collision.tag))
            {
                // Check if the object to destroy is part of the ObjectPooler.
                // If so, we add it back into the pool.
                // If not, we destroy it like normal.
                IPooledObject pooledObject = collision.GetComponent<IPooledObject>();
                if (pooledObject != null)
                {
                    collision.gameObject.SetActive(false);
                    _objectPooler.PoolDictionary[collision.tag].Enqueue(gameObject);
                }
                else
                {
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}
