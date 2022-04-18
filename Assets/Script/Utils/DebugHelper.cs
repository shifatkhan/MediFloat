using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A wrapper for the Debug log.
/// </summary>
public class DebugHelper : MonoBehaviour
{
    #region Debug
    /// <summary>
    /// Logs a message onto a file and in unity.
    /// </summary>
    /// <param name="classname">The classname to log.</param>
    /// <param name="message">The string to log.</param>
    public static void Log(string classname, string message)
    {
        Debug.Log($"{classname}: {message}");
    }

    /// <summary>
    /// Logs a warning onto a file and in unity.
    /// </summary>
    /// <param name="classname">The classname to log.</param>
    /// <param name="message">The string to log.</param>
    public static void LogWarning(string classname, string message)
    {
        Debug.LogWarning($"{classname}: {message}");
    }

    /// <summary>
    /// Logs an error onto a file and in unity.
    /// </summary>
    /// <param name="classname">The classname to log.</param>
    /// <param name="message">The string to log.</param>
    public static void LogError(string classname, string message)
    {
        Debug.LogError($"{classname}: {message}");
    }
    #endregion
}
