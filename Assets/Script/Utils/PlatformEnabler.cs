using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sets gameobject as active for specified platforms.
/// </summary>
public class PlatformEnabler : MonoBehaviour
{
    [SerializeField]
    private List<RuntimePlatform> _platforms;

    private void Start()
    {
        if (_platforms.Contains(Application.platform))
            gameObject.SetActive(true);
    }
}
