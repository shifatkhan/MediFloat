using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pooler;

/// <summary>
/// Controls the mechanism of a singular partical based on
/// user input (rise and fall).
/// 
/// @author Shifat Khan
/// @version 1.0.0
/// </summary>
public class ParticleController : MonoBehaviour, IPooledObject
{
    #region Fields
    public MoveState CurrentState = MoveState.UP;

    [Header("Render config")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private int _bgLayer = -2;
    [SerializeField] private int _midLayer = -1;
    [SerializeField] private int _fgLayer = 1;
    [SerializeField] private Vector3 _normalScale = Vector3.one;
    [SerializeField] private float _opacityThreshold = 0.15f;
    [SerializeField] private float _minOpacity = 0.50f;

    [Header("Movement config")]
    [Tooltip("This is the maximum speed when moving UP.")]
    [SerializeField] private float _maxSpeedUp = 10f;
    [Tooltip("This is the maximum speed when moving DOWN.")]
    [SerializeField] private float _maxSpeedDown = 10f;
    [Tooltip("How fast will object reach a maximum speed.")]
    [SerializeField] private float _acceleration = 10f;
    [Tooltip("How fast will object reach a speed of 0.")]
    [SerializeField] private float _deceleration = 10f;

    [Header("Animation config")]
    [Tooltip("Time it takes to expand and shrink the particle moving up.")]
    [SerializeField] private float _transitionTimeUp = 0.33f;
    [Tooltip("Time it takes to expand and shrink the particle moving down.")]
    [SerializeField] private float _transitionTimeDown = 0.1f;
    [Tooltip("Scale when partical is moving up.")]
    [SerializeField] private Vector3 _stretchedScaleRatio;

    private Vector3 _initialScale;
    public Vector3 InitialScale {
        get => _initialScale;
        set
        {
            transform.localScale = value;
            _initialScale = value;

            if (_initialScale.x < _normalScale.x)
            {
                _spriteRenderer.sortingOrder = _bgLayer;
            }
            else if (_initialScale.x > _normalScale.x)
            {
                _spriteRenderer.sortingOrder = _fgLayer;
            }
            else
            {
                _spriteRenderer.sortingOrder = _midLayer;
            }

            if (_initialScale.x > _normalScale.x + _opacityThreshold)
            {
                Color prevColor = _spriteRenderer.color;
                prevColor.a = _normalScale.x - (_initialScale.x - _normalScale.x);
                prevColor.a = Mathf.Clamp(prevColor.a, _minOpacity, 1f);
                _spriteRenderer.color = prevColor;
            }
            else if(_initialScale.x < _normalScale.x - _opacityThreshold)
            {
                Color prevColor = _spriteRenderer.color;
                prevColor.a = _normalScale.x - _initialScale.x;
                prevColor.a = Mathf.Clamp(prevColor.a, _minOpacity, 1f);
                _spriteRenderer.color = prevColor;
            }
        }
    }

    private float _currentSpeed = 0;
    public float CurrentSpeed => _currentSpeed;

    private float _timeElapsed = 0;
    private bool _stateChanged = false;

    private InputManager _inputManager;
    #endregion

    #region Monobehaviour functions
    private void Awake()
    {
        InitialScale = transform.localScale;
    }

    private void Start()
    {
        _inputManager = InputManager.Instance;
    }

    private void Update()
    {
        if(_inputManager.MouseButton0)
        {
            if (CurrentState == MoveState.DOWN)
                _stateChanged = true;

            CurrentState = MoveState.UP;
        }
        else
        {
            if (CurrentState == MoveState.UP)
                _stateChanged = true;

            CurrentState = MoveState.DOWN;
        }
    }

    private void FixedUpdate()
    {
        if (CurrentState == MoveState.UP)
        {
            MoveUp();
            MoveUpAnimation();
        }
        else
        {
            MoveDown();
            MoveDownAnimation();
        }
        
        var newPosition = transform.position;
        newPosition.y = transform.position.y + _currentSpeed * Time.fixedDeltaTime;
        transform.position = newPosition;

        _currentSpeed = Mathf.Clamp(_currentSpeed, _maxSpeedDown, _maxSpeedUp);
    }
    #endregion

    #region Movement functions
    private void MoveUp()
    {
        _currentSpeed += _acceleration * Time.fixedDeltaTime;
    }

    private void MoveDown()
    {
        _currentSpeed -= _deceleration * Time.fixedDeltaTime;
        //if (_currentSpeed > _deceleration * Time.fixedDeltaTime)
        //{
        //    _currentSpeed -= _deceleration * Time.fixedDeltaTime;
        //}
        //else if(_currentSpeed > -_maxSpeedDown)
        //{
        //    _currentSpeed -= _deceleration * Time.fixedDeltaTime;
        //}
    }

    private void MoveUpAnimation()
    {
        if (_stateChanged)
        {
            _timeElapsed = 0;
            _stateChanged = false;
        }

        _timeElapsed += Time.deltaTime / _transitionTimeUp;
        _timeElapsed = Mathf.Clamp01(_timeElapsed);
        transform.localScale = Vector3.Lerp(_initialScale, Vector3.Scale(_initialScale, _stretchedScaleRatio), _timeElapsed);
    }

    private void MoveDownAnimation()
    {
        if (_stateChanged)
        {
            _timeElapsed = 0;
            _stateChanged = false;
        }

        _timeElapsed += Time.deltaTime / _transitionTimeDown;
        _timeElapsed = Mathf.Clamp01(_timeElapsed);
        transform.localScale = Vector3.Lerp(Vector3.Scale(_initialScale, _stretchedScaleRatio), _initialScale, _timeElapsed);
    }

    public void OnObjectSpawned()
    {
        _initialScale = transform.localScale;
    }
    #endregion
}
