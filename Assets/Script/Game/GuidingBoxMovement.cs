using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Continuously moves the gameobject up and down .
/// 
/// @author Shifat Khan
/// @version 1.0.2
/// </summary>
public class GuidingBoxMovement : Singleton<GuidingBoxMovement>
{
    #region Fields
    public MoveState CurrentState = MoveState.UP;

    [Header("Animation config")]
    public float MoveTimeToTop;
    public float MoveTimeToBot;
    public float DelayTop;
    public float DelayBot;
    [SerializeField] private AnimationCurve _animationCurve;
    public AnimationCurve AnimationCurve => _animationCurve;

    [Header("Positions config")]
    [SerializeField] private float margin;
    [SerializeField] private SpriteMask _spriteMask;

    private Tween _moveToTop;
    private Tween _moveToBot;

    private GameManager _gameManager;
    #endregion

    #region Monobehavior functions
    private IEnumerator Start()
    {
        // Wait a second to remove freezes.
        yield return new WaitForSeconds(1);

        _gameManager = GameManager.Instance;
        _gameManager.RecalculateScreenEvent.AddListener(RecalculateMovePositions);

        RecalculateMovePositions();
        MoveToTop();

        // Let the game know that everything is ready to go.
        _gameManager.SetGameReady();
    }
    #endregion

    #region Movement functions
    /// <summary>
    /// Calculate the top and bottom coordinate to which we will
    /// move the guiding box to.
    /// </summary>
    public void RecalculateMovePositions()
    {
        _gameManager.BoxTopPosition = new Vector3(
            0f,
            _gameManager.TopLeftPoint.y - (_spriteMask.bounds.size.y / 2) - (margin / 2),
            0f);

        _gameManager.BoxBotPosition = new Vector3(
            0f,
            _gameManager.BotLeftPoint.y + (_spriteMask.bounds.size.y / 2) + (margin / 2),
            0f);

        _moveToTop = transform
            .DOMoveY(_gameManager.BoxTopPosition.y, MoveTimeToTop)
            .SetUpdate(UpdateType.Fixed)
            .SetDelay(DelayTop)
            .OnStart(() => { CurrentState = MoveState.UP; })
            .SetEase(_animationCurve)
            .OnComplete(MoveToBot)
            .SetAutoKill(false);
        _moveToTop.Pause();

        _moveToBot = transform
            .DOMoveY(_gameManager.BoxBotPosition.y, MoveTimeToBot)
            .SetUpdate(UpdateType.Fixed)
            .SetDelay(DelayBot)
            .OnStart(() => { CurrentState = MoveState.DOWN; })
            .SetEase(_animationCurve)
            .OnComplete(MoveToTop)
            .SetAutoKill(false);
        _moveToBot.Pause();

        // Set the start position to be at the botttom.
        transform.position = _gameManager.BoxBotPosition;
    }

    /// <summary>
    /// Moves the box to the top of the screen.
    /// </summary>
    public void MoveToTop()
    {
        CurrentState = MoveState.UP;
        _moveToTop.Restart();
    }

    /// <summary>
    /// Moves the box to the bottom of the screen.
    /// </summary>
    public void MoveToBot()
    {
        CurrentState = MoveState.DOWN;
        _moveToBot.Restart();
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(_spriteMask.bounds.size.x + margin, _spriteMask.bounds.size.y + margin, 0f));
    }

    private void OnDestroy()
    {
        _moveToTop.Kill();
        _moveToBot.Kill();
    }
}
