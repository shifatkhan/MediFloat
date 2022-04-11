using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ImageHelper : MonoBehaviour, IPictureHelper
{
    private Image _image;
    private Color _showColor;
    private Color _hideColor;

    [Range(0, 1)]
    [SerializeField]
    private float _showOpacity = 1f;
    [SerializeField]
    private float _animationDuration = 0.33f;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Start()
    {
        if (_image == null) return;

        _showColor = _image.color;
        _showColor.a = _showOpacity;

        _hideColor = _image.color;
        _hideColor.a = 0f;
    }

    private void OnDestroy()
    {
        DOTween.Kill(_image);
    }

    public void Show() => Show(_showOpacity, _animationDuration, null);

    public void Show(Action onComplete = null) => Show(_showOpacity, _animationDuration, onComplete);

    public void Show(float opacity, float animDuration, Action onComplete = null)
    {
        _showColor.a = opacity;

        _image.DOColor(_showColor, animDuration)
            .OnComplete(() => onComplete?.Invoke());
    }

    public void Hide() => Hide(_animationDuration, null);

    public void Hide(Action onComplete = null) => Hide(_animationDuration, onComplete);

    public void Hide(float animDuration, Action onComplete = null)
    {
        _image.DOColor(_hideColor, animDuration)
            .OnComplete(() => onComplete?.Invoke());
    }
}
