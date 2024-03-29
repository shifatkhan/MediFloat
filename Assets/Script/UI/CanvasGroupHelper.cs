using DG.Tweening;
using System;
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
    [SerializeField]
    private bool _useGameObjectActiveState = false;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnDestroy()
    {
        DOTween.Kill(_canvasGroup);
    }

    public void Show() => Show(_showOpacity, _animationDuration, null);

    public void Show(Action onComplete = null) => Show(_showOpacity, _animationDuration, onComplete);

    public void Show(float opacity, float animDuration, Action onComplete = null)
    {
        if (_canvasGroup == null)
            return;

        if (_useGameObjectActiveState)
            _canvasGroup.gameObject.SetActive(true);

        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.DOFade(_showOpacity, animDuration)
            .OnComplete(() => onComplete?.Invoke());
    }

    public void Hide() => Hide(_animationDuration, null);

    public void Hide(Action onComplete = null) => Hide(_animationDuration, onComplete);

    public void Hide(float animDuration, Action onComplete = null)
    {
        if (_canvasGroup == null)
            return;

        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.DOFade(0, animDuration)
            .OnComplete(() => 
            { 
                onComplete?.Invoke();
                if (_useGameObjectActiveState)
                    _canvasGroup.gameObject.SetActive(false);
            });
    }
}
