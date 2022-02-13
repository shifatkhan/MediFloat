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
    private GameManager _gameManager;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _gameManager.GameStartedEvent.AddListener(ResizeOnStart);
    }

    private void ResizeOnStart()
    {
        ResizeToFullscreen();
        _gameManager.RecalculateScreenEvent.AddListener(ResizeToFullscreen);
    }

    private void OnDestroy()
    {
        _gameManager.GameStartedEvent.RemoveListener(ResizeOnStart);
    }

    public void ResizeToFullscreen()
    {
        Sprite s = _spriteRenderer.sprite;
        float unitWidth = s.textureRect.width / s.pixelsPerUnit;
        float unitHeight = s.textureRect.height / s.pixelsPerUnit;

        _spriteRenderer.transform.localScale = 
            new Vector3(_gameManager.ScreenWidth / unitWidth, _gameManager.ScreenHeight / unitHeight, 0f);
    }
}
