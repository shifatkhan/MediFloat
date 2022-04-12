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

    [Header("Audio")]
    [SerializeField]
    private AudioSource _audioSource;
    [Range(0,1)]
    [SerializeField]
    private float _breatheInVolume = 1f;
    [SerializeField]
    private float _breathInPitch = 1f;
    [SerializeField]
    private float _holdVolume = 0f;
    [SerializeField]
    private float _holdPitch = 0.5f;
    [SerializeField]
    private float _breatheOutVolume = 1f;
    [SerializeField]
    private float _breathOutPitch = 1f;
    [SerializeField]
    private float _audioTransitionTime = 0.33f;

    private Tween _moveToTop;
    private Tween _moveToBot;
    private Tween _moveToInitialPos;

    private GuidingBoxMovement _guidingBox;
    private InputManager _inputManager;
    private GameManager _gameManager;


    private Vector3 _initialPos;
    private void Start()
    {
        _guidingBox = GuidingBoxMovement.Instance;
        _inputManager = InputManager.Instance;
        _gameManager = GameManager.Instance;

        _initialPos = transform.position;

        _gameManager.GameStartEvent.AddListener(() => AudioTransition(_breatheOutVolume, _breathOutPitch));
        _gameManager.GamePauseEvent.AddListener(() => AudioTransition(0, _holdPitch));
    }

    private void Update()
    {
        if (!_inputManager.InputEnabled) return;

        if (_inputManager.MouseButton0)
        {
            MoveToTop();
        }
        else
        {
            MoveToBot();
        }
    }

    private void OnDestroy()
    {
        _moveToTop.Kill();
        _moveToBot.Kill();
        _moveToInitialPos.Kill();
    }

    /// <summary>
    /// Moves the feather towards the top of the screen.
    /// </summary>
    public void MoveToTop()
    {
        if (CurrentState == MoveState.UP)
            return;

        _moveToBot?.Pause();
        _moveToInitialPos?.Pause();

        CurrentState = MoveState.UP;
        _moveToTop = transform
            .DOMoveY(_gameManager.BoxTopPosition.y, _guidingBox.MoveTimeToTop / SpeedMultiplier)
            .SetUpdate(UpdateType.Fixed)
            .SetEase(_guidingBox.AnimationCurve)
            .SetAutoKill(false)
            .SetRecyclable(true)
            .OnComplete(() => { if (_gameManager.GameState != GameState.PAUSED) AudioTransition(_holdVolume, _holdPitch); });

        AudioTransition(_breatheInVolume, _breathInPitch);
    }

    /// <summary>
    /// Moves the feather towards the bottom of the screen.
    /// </summary>
    public void MoveToBot()
    {
        if (CurrentState == MoveState.DOWN)
            return;

        _moveToTop?.Pause();
        _moveToInitialPos?.Pause();

        CurrentState = MoveState.DOWN;
        _moveToBot = transform
            .DOMoveY(_gameManager.BoxBotPosition.y, _guidingBox.MoveTimeToBot / SpeedMultiplier)
            .SetUpdate(UpdateType.Fixed)
            .SetEase(_guidingBox.AnimationCurve)
            .SetRecyclable(true);

        AudioTransition(_breatheOutVolume, _breathOutPitch);
    }

    public void MoveToInitialPos()
    {
        _moveToTop?.Pause();
        _moveToBot?.Pause();

        CurrentState = MoveState.DOWN;
        _moveToInitialPos = transform
            .DOMoveY(_initialPos.y, _guidingBox.MoveTimeToTop / SpeedMultiplier)
            .SetUpdate(UpdateType.Fixed)
            .SetEase(_guidingBox.AnimationCurve)
            .SetRecyclable(true);
    }

    private void AudioTransition(float volume, float pitch)
    {
        _audioSource.DOFade(volume, _audioTransitionTime);
        _audioSource.DOPitch(pitch, _audioTransitionTime);
    }
}
