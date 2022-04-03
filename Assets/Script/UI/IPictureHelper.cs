using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IPictureHelper
{
    public void Show(UnityEvent onComplete = null);

    public void Show(float opacity, float animDuration, UnityEvent onComplete = null);

    public void Hide(UnityEvent onComplete = null);

    public void Hide(float animDuration, UnityEvent onComplete = null);
}
