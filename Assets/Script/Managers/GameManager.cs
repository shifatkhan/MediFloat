using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Calculates and manages all information about screen size
/// and gameplay.
/// 
/// @author Shifat Khan
/// @version 1.0.1
/// </summary>
public class GameManager : Singleton<GameManager>
{
    #region Fields - position
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
    [HideInInspector] public Vector3 BoxTopPosition;
    [HideInInspector] public Vector3 BoxBotPosition;
    #endregion

    #region Fields - other
    private GameState _gameState = GameState.NOT_STARTED;
    public GameState GameState => _gameState;

    [HideInInspector] public UnityEvent GameStartedEvent;
    [HideInInspector] public UnityEvent GamePausedEvent;
    [HideInInspector] public UnityEvent ScreenChangeEvent;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        RecalculateScreenSizes();
    }

    public void StartGame()
    {
        GameStartedEvent.Invoke();

        _gameState = GameState.STARTED;
    }

    public void PauseGame()
    {
        GamePausedEvent.Invoke();

        _gameState = GameState.PAUSED;
    }

    public void RecalculateScreenSizes()
    {
        _screenHeight = Camera.main.orthographicSize * 2;
        _screenWidth = _screenHeight * Screen.width / Screen.height; // basically height * screen aspect ratio

        Camera camera = Camera.main;
        _topLeftPoint = camera.ViewportToWorldPoint(new Vector3(0f, 1f, 0f));
        _topRightPoint = camera.ViewportToWorldPoint(new Vector3(1f, 1f, 0f));
        _botLeftPoint = camera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
        _botRightPoint = camera.ViewportToWorldPoint(new Vector3(1f, 0f, 0f));

        ScreenChangeEvent.Invoke();
    }
}
