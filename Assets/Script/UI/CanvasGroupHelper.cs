using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CanvasGroupHelper : MonoBehaviour, IPictureHelper
{
    private CanvasGroup _canvasGroup;

    [Range(0, 1)]
    [SerializeField]
    private float _showOpacity = 1f;
    [SerializeField]
    private float _animationDuration = 0.33f;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnDestroy()
    {
        DOTween.Kill(_canvasGroup);
    }

    public void Show() => Show(_showOpacity, _animationDuration, null);

    public void Show(UnityEvent onComplete = null) => Show(_showOpacity, _animationDuration, onComplete);

    public void Show(float opacity, float animDuration, UnityEvent onComplete = null)
    {
        _canvasGroup.DOFade(_showOpacity, animDuration)
            .OnComplete(() => onComplete?.Invoke());
    }

    public void Hide() => Hide(_animationDuration, null);

    public void Hide(UnityEvent onComplete = null) => Hide(_animationDuration, onComplete);

    public void Hide(float animDuration, UnityEvent onComplete = null)
    {
        _canvasGroup.DOFade(0, animDuration)
            .OnComplete(() => onComplete?.Invoke());
    }
}
