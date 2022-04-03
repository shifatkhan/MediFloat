using Pooler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns particles on the bottom of the screen.
/// 
/// @author Shifat Khan.
/// </summary>
public class ParticleSpawner : MonoBehaviour
{
    private GameManager _gameManager;
    private ObjectPooler _objectPooler;

    [SerializeField]
    private string _particlePoolID = "snow";
    [SerializeField]
    private float _expandX = 0f;
    [SerializeField]
    private float _expandY = 10f;

    [Header("Sizing")]
    [SerializeField]
    [Range(0, 1)]
    [Tooltip("1 = Spawned only on foreground, 0 = Spawned only on background.")]
    private float _fgBgSpawnRatio = 0.5f;
    [SerializeField]
    private Vector3 _maxFgScale = new Vector3(2f, 2f, 1f);
    [SerializeField]
    private Vector3 _maxBgScale = new Vector3(0.33f, 0.33f, 1f);

    [Header("Spacing")]
    [SerializeField]
    private float _minSpacing = 5;
    [SerializeField]
    private float _maxSpacing = 25;

    void Start()
    {
        _objectPooler = ObjectPooler.Instance;

        _gameManager = GameManager.Instance;
        _gameManager.GameReadyEvent.AddListener(PopulateScreen);
    }

    private void OnDestroy()
    {
        _gameManager.GameReadyEvent.RemoveListener(PopulateScreen);
    }

    private void PopulateScreen()
    {
        int poolSize = _objectPooler.PoolDictionary[_particlePoolID].Count;
        int foregroundCount = Mathf.FloorToInt(_fgBgSpawnRatio * poolSize);

        Vector3 prevPos = Vector3.zero;
        for (int i = 0; i < poolSize; i++)
        {
            // Try to spawn at spaced position.
            // If we can't after 3 tries, we leave it.
            Vector3 newPos = Vector3.zero;
            bool ready = false;
            for (int j = 0; j < 3 && !ready; j++)
            {
                float randX = Random.Range(_gameManager.BotLeftPoint.x - _expandX, _gameManager.BotRightPoint.x + _expandX);
                float randY = Random.Range(_gameManager.BotLeftPoint.y - _expandY, _gameManager.TopLeftPoint.y + _expandY);
                newPos = new Vector3(randX, randY, 0);

                float dist = Vector3.Distance(prevPos, newPos);

                if (i != 0 && dist >= _minSpacing && dist <= _maxSpacing)
                    ready = true;
            }

            if (foregroundCount > 0)
            {
                // Spawn in foreground.
                float newScaleX = Random.Range(1, _maxFgScale.x);
                float newScaleY = newScaleX;
                Vector3 newScale = new Vector3(newScaleX, newScaleY, 1f);
                _objectPooler.SpawnFromPool(_particlePoolID, newPos, newScale, Quaternion.identity);
                foregroundCount--;
            }
            else
            {
                // Spawn in background.
                float newScaleX = Random.Range(_maxBgScale.x, 1);
                float newScaleY = newScaleX;
                Vector3 newScale = new Vector3(newScaleX, newScaleY, 1f);
                var objectSpawned = _objectPooler.SpawnFromPool(_particlePoolID, newPos, newScale, Quaternion.identity);
                objectSpawned.GetComponent<ParticleController>().InitialScale = newScale;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_gameManager == null)
            return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, new Vector3((_gameManager.TopRightPoint.x * 2) + _expandX, (_gameManager.TopLeftPoint.y * 2) + _expandY, 0f));
    }
}
