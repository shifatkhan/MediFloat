using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEnabler : MonoBehaviour
{
    [SerializeField]
    private List<RuntimePlatform> _platforms;

    private void Start()
    {
        if (!_platforms.Contains(Application.platform))
            gameObject.SetActive(false);
    }
}
