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

    private float _currentSpeed = 0;
    public float CurrentSpeed => _currentSpeed;

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
    [Tooltip("Time it takes to expand and shrink the particle.")]
    [SerializeField] private float _transitionTime = 1f;
    [Tooltip("Scale when partical is moving up.")]
    [SerializeField] private Vector3 _stretchedScale;
    private Vector3 _initialScale;
    public Vector3 InitialScale {
        get => _initialScale;
        set
        {
            transform.localScale = value;
            _initialScale = value;
            _stretchedScale.x = _initialScale.x * _scaleRatio.x;
            _stretchedScale.y = _initialScale.y * _scaleRatio.y;
            _stretchedScale.z = _initialScale.z * _scaleRatio.z;
        }
    }
    private Vector3 _scaleRatio;
    private float _timeElapsed = 0;

    private InputManager _inputManager;
    #endregion

    #region Monobehaviour functions
    private void Start()
    {
        _inputManager = InputManager.Instance;
        _scaleRatio.x = _stretchedScale.x / _initialScale.x;
        _scaleRatio.y = _stretchedScale.y / _initialScale.y;
        _scaleRatio.z = _stretchedScale.z / _initialScale.z;
    }

    private void Update()
    {
        if(_inputManager.MouseButton0)
        {
            CurrentState = MoveState.UP;
        }
        else
        {
            CurrentState = MoveState.DOWN;
        }
    }

    private void FixedUpdate()
    {
        if (CurrentState == MoveState.UP && _currentSpeed < _maxSpeedUp)
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
    }
    #endregion

    #region Movement functions
    private void MoveUp()
    {
        _currentSpeed += _acceleration * Time.fixedDeltaTime;
    }

    private void MoveDown()
    {
        if (_currentSpeed > _deceleration * Time.fixedDeltaTime)
        {
            _currentSpeed -= _deceleration * Time.fixedDeltaTime;
        }
        else if(_currentSpeed > -_maxSpeedDown)
        {
            _currentSpeed -= _deceleration * Time.fixedDeltaTime;
        }
    }

    private void MoveUpAnimation()
    {
        _timeElapsed += Time.deltaTime / _transitionTime;
        _timeElapsed = Mathf.Clamp01(_timeElapsed);
        transform.localScale = Vector3.Lerp(_initialScale, _stretchedScale, _timeElapsed);
    }

    private void MoveDownAnimation()
    {
        _timeElapsed -= Time.deltaTime / _transitionTime;
        _timeElapsed = Mathf.Clamp01(_timeElapsed);
        transform.localScale = Vector3.Lerp(_stretchedScale, _initialScale, _timeElapsed);
    }

    public void OnObjectSpawned()
    {
        _initialScale = transform.localScale;
    }
    #endregion
}
