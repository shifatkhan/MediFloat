using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the mechanism of a singular partical based on
/// user input (rise and fall).
/// 
/// @author Shifat Khan
/// @version 1.0.0
/// </summary>
public class ParticleController : MonoBehaviour
{
    #region Fields
    public MoveState CurrentState = MoveState.UP;

    private float _currentSpeed = 0;
    public float CurrentSpeed => _currentSpeed;

    [Tooltip("This is the maximum speed when moving UP.")]
    [SerializeField] private float _maxSpeedUp = 10;
    [Tooltip("This is the maximum speed when moving DOWN.")]
    [SerializeField] private float _maxSpeedDown = 10;
    [Tooltip("How fast will object reach a maximum speed.")]
    [SerializeField] private float _acceleration = 10;
    [Tooltip("How fast will object reach a speed of 0.")]
    [SerializeField] private float _deceleration = 10;

    private InputManager _inputManager;
    #endregion

    #region Monobehaviour functions
    private void Start()
    {
        _inputManager = InputManager.Instance;
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
        }
        else
        {
            MoveDown();
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
    #endregion
}
