using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gives a monobehavior the Singleton behavior.
/// Can also make it no destroy. 
/// 
/// @author Shifat Khan
/// @version 1.0.0
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	public static T Instance;
	[SerializeField] protected bool _dontDestroyOnLoad = true;

	protected virtual void Awake()
	{
		// Singleton.
		if (Instance != null && Instance != this)
			Destroy(gameObject);

		Instance = FindObjectOfType<T>();

        // DontDestroyOnLoad.
        if (_dontDestroyOnLoad)
        {
			// DontDestroyOnLoad does not work if this is a child.
			if (transform.parent != null)
				transform.parent = null;

			DontDestroyOnLoad(gameObject);
		}
	}
}
