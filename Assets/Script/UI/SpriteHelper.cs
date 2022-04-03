using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class SpriteHelper : MonoBehaviour, IPictureHelper
{
    private SpriteRenderer _spriteRenderer;
    private Color _showColor;
    private Color _hideColor;

    [Range(0, 1)]
    [SerializeField]
    private float _showOpacity = 1f;
    [SerializeField]
    private float _animationDuration = 0.33f;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (_spriteRenderer == null) return;

        _showColor = _spriteRenderer.color;
        _showColor.a = _showOpacity;

        _hideColor = _spriteRenderer.color;
        _hideColor.a = 0f;
    }

    private void OnDestroy()
    {
        DOTween.Kill(_spriteRenderer);
    }

    public void Show() => Show(_showOpacity, _animationDuration, null);

    public void Show(UnityEvent onComplete = null) => Show(_showOpacity, _animationDuration, onComplete);

    public void Show(float opacity, float animDuration, UnityEvent onComplete = null)
    {
        _showColor.a = opacity;

        _spriteRenderer.DOColor(_showColor, animDuration)
            .OnComplete(() => onComplete?.Invoke());
    }

    public void Hide() => Hide(_animationDuration, null);

    public void Hide(UnityEvent onComplete = null) => Hide(_animationDuration, onComplete);

    public void Hide(float animDuration, UnityEvent onComplete = null)
    {
        _spriteRenderer.DOColor(_hideColor, animDuration)
            .OnComplete(() => onComplete?.Invoke());
    }
}
