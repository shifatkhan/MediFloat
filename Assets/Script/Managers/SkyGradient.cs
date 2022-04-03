using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkyGradient : MonoBehaviour
{
    [Serializable]
    public class ColorCombo
    {
        public Color TopColor = Color.white;
        public Color BotColor = Color.white;
    }

    [SerializeField]
    private bool _animateOnStart = true;
    [SerializeField]
    private SpriteRenderer _topSprite;
    [SerializeField]
    private SpriteRenderer _botSprite;

    [Header("Colors")]
    [SerializeField]
    private List<ColorCombo> _skyColors;
    [SerializeField]
    private float _transitionDuration = 10f;
    [SerializeField]
    private float _delay = 5f;

    private int _currIndex = 0;

    private void Start()
    {
        if (_animateOnStart)
            StartColorChange();
    }

    private void OnDestroy()
    {
        DOTween.Kill(_topSprite);
        DOTween.Kill(_botSprite);
    }

    public void StartColorChange()
    {
        if (_skyColors.Count == 0)
            return;

        _topSprite.DOColor(_skyColors[_currIndex].TopColor, _transitionDuration)
            .SetDelay(_delay)
            .OnComplete(GoToNextColor);

        _botSprite.DOColor(_skyColors[_currIndex].BotColor, _transitionDuration)
            .SetDelay(_delay);
    }

    private void GoToNextColor()
    {
        if (_currIndex == _skyColors.Count - 1)
            _currIndex = 0;
        else
            _currIndex++;

        _topSprite.DOColor(_skyColors[_currIndex].TopColor, _transitionDuration)
            .SetDelay(_delay)
            .OnComplete(GoToNextColor);

        _botSprite.DOColor(_skyColors[_currIndex].BotColor, _transitionDuration)
            .SetDelay(_delay);
    }
}
