using Pooler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns particles on the bottom of the screen.
/// 
/// @author Shifat Khan
/// </summary>
public class ParticleSpawner : MonoBehaviour
{
    private GameManager _gameManager;
    private ObjectPooler _objectPooler;

    [SerializeField]
    private string _particlePoolID = "snow";

    [Header("Sizing")]
    [SerializeField]
    [Range(0, 1)]
    [Tooltip("1 = Spawned only on foreground, 0 = Spawned only on background.")]
    private float _fgToBgRatio = 0.5f;
    [SerializeField]
    private float _fgRange = 0.25f;
    [SerializeField]
    private Vector3 _fgScale = Vector3.one;
    [SerializeField]
    private Vector3 _bgScale = new Vector3(2f, 2f, 2f);

    [Header("Spacing")]
    [SerializeField]
    private float _minSpacing = 5;
    [SerializeField]
    private float _maxSpacing = 25;

    void Start()
    {
        _objectPooler = ObjectPooler.Instance;

        _gameManager = GameManager.Instance;
        _gameManager.GameStartedEvent.AddListener(PopulateScreen);
    }

    private void OnDestroy()
    {
        _gameManager.GameStartedEvent.RemoveListener(PopulateScreen);
    }

    private void PopulateScreen()
    {
        int poolSize = _objectPooler.PoolDictionary[_particlePoolID].Count;
        int foregroundCount = Mathf.FloorToInt(_fgToBgRatio * poolSize);

        Vector3 prevPos = Vector3.zero;
        for (int i = 0; i < poolSize; i++)
        {
            // Try to spawn at spaced position.
            // If we can't after 3 tries, we leave it.
            Vector3 newPos = Vector3.zero;
            bool ready = false;
            for (int j = 0; j < 3 && !ready; j++)
            {
                float randX = Random.Range(_gameManager.BotLeftPoint.x, _gameManager.BotRightPoint.x);
                float randY = Random.Range(_gameManager.BotLeftPoint.y, _gameManager.TopLeftPoint.y);
                newPos = new Vector3(randX, randY, 0);

                float dist = Vector3.Distance(prevPos, newPos);

                if (i != 0 && dist >= _minSpacing && dist <= _maxSpacing)
                    ready = true;
            }

            float zRange = _fgRange / 2;
            if (foregroundCount > 0)
            {
                // Spawn in foreground.
                float foregroundZ = Random.Range(-zRange, zRange);
                newPos.z = foregroundZ;
                _objectPooler.SpawnFromPool(_particlePoolID, newPos, _fgScale, Quaternion.identity);
                foregroundCount--;
            }
            else
            {
                // Spawn in background.
                float foregroundZ = Random.Range(zRange, 1);
                foregroundZ *= Random.Range(0, 2) * 2 - 1; // Randomly set negative or positive.
                Vector3 newScale = _bgScale * foregroundZ;
                newPos.z = foregroundZ;
                var objectSpawned = _objectPooler.SpawnFromPool(_particlePoolID, newPos, newScale, Quaternion.identity);
                objectSpawned.GetComponent<ParticleController>().InitialScale = newScale;
            }
        }
    }
}
