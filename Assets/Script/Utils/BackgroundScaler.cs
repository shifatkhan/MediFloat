using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scales a gameobject to fill the whole screen.
/// 
/// @author Shifat Khan
/// @version 1.0.0
/// </summary>
public class BackgroundScaler : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private ScreenManager _screenManager;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _screenManager = ScreenManager.Instance;

        ResizeToFullscreen();
    }

    public void ResizeToFullscreen()
    {
        Sprite s = _spriteRenderer.sprite;
        float unitWidth = s.textureRect.width / s.pixelsPerUnit;
        float unitHeight = s.textureRect.height / s.pixelsPerUnit;

        _spriteRenderer.transform.localScale = 
            new Vector3(_screenManager.ScreenWidth / unitWidth, _screenManager.ScreenHeight / unitHeight, 0f);
    }
}
