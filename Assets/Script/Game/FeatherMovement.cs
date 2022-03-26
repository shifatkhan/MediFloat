using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Handles player input to make the feather go up and down.
/// 
/// TODO: Code here is a duplicate of what is found in <see cref="GuidingBoxMovement"/>. Maybe make them both inherit from 1 class.
/// 
/// @author Shifat Khan
/// @version 1.0.1
/// </summary>
public class FeatherMovement : MonoBehaviour
{
    public MoveState CurrentState = MoveState.IDLE;

    public float SpeedMultiplier = 1f;

    private Tween _moveToTop;
    private Tween _moveToBot;

    private GuidingBoxMovement _guidingBox;
    private InputManager _inputManager;
    private GameManager _gameManager;

    private void Start()
    {
        _guidingBox = GuidingBoxMovement.Instance;
        _inputManager = InputManager.Instance;
        _gameManager = GameManager.Instance;
    }

    private void Update()
    {
        if (_inputManager.MouseButton0)
        {
            MoveToTop();
        }
        else
        {
            MoveToBot();
        }
    }

    /// <summary>
    /// Moves the feather towards the top of the screen.
    /// </summary>
    public void MoveToTop()
    {
        if (CurrentState == MoveState.UP)
            return;

        _moveToBot?.Pause();

        CurrentState = MoveState.UP;
        _moveToTop = transform
            .DOMoveY(_gameManager.BoxTopPosition.y, _guidingBox.MoveTimeToTop / SpeedMultiplier)
            .SetUpdate(UpdateType.Fixed)
            .SetEase(_guidingBox.AnimationCurve)
            .SetAutoKill(false)
            .SetRecyclable(true);
    }

    /// <summary>
    /// Moves the feather towards the bottom of the screen.
    /// </summary>
    public void MoveToBot()
    {
        if (CurrentState == MoveState.DOWN)
            return;

        _moveToTop?.Pause();

        CurrentState = MoveState.DOWN;
        _moveToBot = transform
            .DOMoveY(_gameManager.BoxBotPosition.y, _guidingBox.MoveTimeToBot / SpeedMultiplier)
            .SetUpdate(UpdateType.Fixed)
            .SetEase(_guidingBox.AnimationCurve)
            .SetRecyclable(true);
    }
}
