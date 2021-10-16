using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Continuously moves the gameobject up and down .
/// 
/// @author Shifat Khan
/// @version 1.0.0
/// </summary>
public class GuidingBoxMovement : Singleton<GuidingBoxMovement>
{
    public MoveState CurrentState = MoveState.UP;

    [Header("Animation config")]
    public float MoveTimeTop;
    public float MoveTimeBot;
    public float DelayTop;
    public float DelayBot;
    [SerializeField] private AnimationCurve _animationCurve;

    [Header("Positions config")]
    [SerializeField] private float margin;
    [SerializeField] private SpriteMask _spriteMask;

    private float _screenMaxHeight;
    private float _screenMinHeight;

    private ScreenManager _screenManager;

    private IEnumerator Start()
    {
        // Wait a second to remove freezes.
        yield return new WaitForSeconds(1);

        _screenManager = ScreenManager.Instance;
        _spriteMask = GetComponent<SpriteMask>();

        CalculateMovePositions();
        MoveToTop();
    }

    /// <summary>
    /// Calculate the top and bottom coordinate to which we will
    /// move the guiding box to.
    /// </summary>
    private void CalculateMovePositions()
    {
        _screenMaxHeight = _screenManager.TopLeftPoint.y - (_spriteMask.bounds.size.y / 2) - (margin / 2);
        _screenMinHeight = _screenManager.BotLeftPoint.y + (_spriteMask.bounds.size.y / 2) + (margin / 2);

    }

    public void MoveToTop()
    {
        CurrentState = MoveState.UP;
        transform
            .DOMoveY(_screenMaxHeight, MoveTimeTop)
            .SetDelay(DelayTop)
            .SetEase(_animationCurve)
            .OnComplete(MoveToBot);
    }

    public void MoveToBot()
    {
        CurrentState = MoveState.HOLD;
        transform
            .DOMoveY(_screenMinHeight, MoveTimeBot)
            .SetDelay(DelayBot)
            .OnStart(() => { CurrentState = MoveState.DOWN; })
            .SetEase(_animationCurve)
            .OnComplete(MoveToTop);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(_spriteMask.bounds.size.x + margin, _spriteMask.bounds.size.y + margin, 0f));
    }
}

public enum MoveState
{
    UP,
    DOWN,
    HOLD
}
