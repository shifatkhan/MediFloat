using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IPictureHelper
{
    public void Show();

    public void Show(Action onComplete = null);

    public void Show(float opacity, float animDuration, Action onComplete = null);

    public void Hide();

    public void Hide(Action onComplete = null);

    public void Hide(float animDuration, Action onComplete = null);
}
