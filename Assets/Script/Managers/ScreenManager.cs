using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Calculates and manages all information about screen size.
/// 
/// @author Shifat Khan
/// @version 1.0.0
/// </summary>
public class ScreenManager : Singleton<ScreenManager>
{
    private float _screenWidth;
    public float ScreenWidth => _screenWidth;

    private float _screenHeight;
    public float ScreenHeight => _screenHeight;

    private Vector3 _topLeftPoint;
    public Vector3 TopLeftPoint => _topLeftPoint;

    private Vector3 _topRightPoint;
    public Vector3 TopRightPoint => _topRightPoint;

    private Vector3 _botLeftPoint;
    public Vector3 BotLeftPoint => _botLeftPoint;

    private Vector3 _botRightPoint;
    public Vector3 BotRightPoint => _botRightPoint;

    protected override void Awake()
    {
        base.Awake();
        CalculateScreenSizes();
    }

    public void CalculateScreenSizes()
    {
        _screenHeight = Camera.main.orthographicSize * 2;
        _screenWidth = _screenHeight * Screen.width / Screen.height; // basically height * screen aspect ratio

        Camera camera = Camera.main;
        _topLeftPoint = camera.ViewportToWorldPoint(new Vector3(0f, 1f, 0f));
        _topRightPoint = camera.ViewportToWorldPoint(new Vector3(1f, 1f, 0f));
        _botLeftPoint = camera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
        _botRightPoint = camera.ViewportToWorldPoint(new Vector3(1f, 0f, 0f));
    }
}
