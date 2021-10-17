using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Handles player input to make the feather go up and down.
/// 
/// @author Shifat Khan
/// @version 1.0.0
/// </summary>
public class FeatherMovement : MonoBehaviour
{
    public MoveState CurrentState = MoveState.IDLE;

    [Header("Speed")]
    [Tooltip("Force to push the feather up.")]
    [SerializeField] private float _force;

    [Tooltip("Adjust the max velocity to your liking.")]
    [SerializeField] private float _maxVelocityMultiplier = 1f;

    private float _maxVelocityUp;
    private float _maxVelocityDown;

    [Header("Easing")]
    [Tooltip("Easing variables to make the feather switch up and down smoothly.")]
    [SerializeField] private float _easeStepperUp = 0.1f;
    [SerializeField] private float _easeStepperDown = 0.1f;
    private float _easingValue = 0f;


    private Rigidbody2D _rigidBody;

    private GuidingBoxMovement _guidingBox;
    private GameManager _gameManager;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private IEnumerator Start()
    {
        _gameManager = GameManager.Instance;
        _gameManager.GameStartedEvent.AddListener(Initialize);

        _guidingBox = GuidingBoxMovement.Instance;

        while (_gameManager.GameState != GameState.STARTED)
            yield return null;

        Initialize();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // Holding press.
            Debug.Log("HOLDING");
            _easingValue += _easeStepperUp;
            CurrentState = MoveState.UP;
        }
        else
        {
            _easingValue -= _easeStepperDown;
            CurrentState = CurrentState == MoveState.IDLE ? MoveState.IDLE : MoveState.DOWN;
        }

        _easingValue = Mathf.Clamp01(_easingValue);
    }

    private void FixedUpdate()
    {
        // Move the feather upwards.
        _rigidBody.AddForce(Vector2.up * _force * _easingValue);

        // Limit the velocity to the guiding box's speed.
        _rigidBody.velocity = Vector2.ClampMagnitude(
            _rigidBody.velocity,
            CurrentState == MoveState.UP ? _maxVelocityUp : _maxVelocityDown
            );
    }

    /// <summary>
    /// Initializes any variables when the game is ready.
    /// </summary>
    public void Initialize()
    {
        if (CurrentState != MoveState.IDLE)
            return;

        // Calculate max velocity with v = (x2 - x1) / (t2 - t1)
        var height = Vector2.Distance(_gameManager.BoxTopPosition, _gameManager.BoxBotPosition);

        _maxVelocityUp = (height / _guidingBox.MoveTimeToTop) * _maxVelocityMultiplier;
        _maxVelocityDown = (height / _guidingBox.MoveTimeToBot) * _maxVelocityMultiplier;
    }
}
