using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An interface for objects that can be pooled.
/// 
/// @author Shifat Khan
/// @version 1.0.0
/// </summary>
namespace Pooler
{
    /// <summary>
    /// Interface for a pooled object. When this object will be pulled,
    /// we can add a default behaviour to it.
    /// </summary>
    public interface IPooledObject
    {
        /// <summary>
        /// Specifies what to do when it has been pooled.
        /// </summary>
        void OnObjectSpawned();
    }
}
