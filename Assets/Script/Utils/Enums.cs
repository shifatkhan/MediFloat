/// <summary>
/// Enum representing the state of the game.
/// <para>
/// <see cref="GameState.NOT_STARTED"/> = before the game starts,
/// <see cref="GameState.STARTED"/> = game has started,
/// <see cref="GameState.PAUSED"/> = game is paused
/// </para>
/// </summary>
public enum GameState
{
    NOT_STARTED,
    STARTED,
    PAUSED
}

/// <summary>
/// Enum representing the state of the box
/// movement.
/// <para>
/// <see cref="MoveState.UP"/> = Moving upwards,
/// <see cref="MoveState.DOWN"/> = Moving downwards,
/// <see cref="MoveState.IDLE"/> = Not moving
/// </para>
/// </summary>
public enum MoveState
{
    UP,
    DOWN,
    IDLE
}
