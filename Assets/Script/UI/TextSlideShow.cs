using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;
using UnityEngine.Events;

/// <summary>
/// TODO: Add unity events list to each message in case we want to add anims.
/// </summary>
public class TextSlideShow : MonoBehaviour
{
    [Serializable]
    public class Slide
    {
        [TextArea(3, 10)]
        public string text;
        public float duration = 7f;
        public UnityEvent OnSlideStart;
        public UnityEvent OnSlideEnd;
    }

    [SerializeField]
    private bool _animateOnStart = true;
    [SerializeField]
    private TextMeshProUGUI _TMP_text;
    [SerializeField]
    private CanvasGroup _canvasGroup;
    [SerializeField]
    private List<Slide> _slides;
    [SerializeField]
    private float _transitionDuration = 10f;

    private int _currIndex = 0;
    private Sequence _sequence;

    private GameState state = GameState.NOT_STARTED;

    private void Start()
    {
        if (_animateOnStart)
            StartChange();
    }

    private void OnDestroy()
    {
        DOTween.Kill(_canvasGroup);
    }

    public void StartChange()
    {
        if (_slides.Count == 0 || state == GameState.STARTED)
            return;

        state = GameState.STARTED;

        _currIndex = 0;

        _TMP_text.text = _slides[_currIndex].text;
        GoToNext();
    }

    private void GoToNext()
    {
        _sequence?.Kill();
        _sequence = DOTween.Sequence();

        var onStart = _slides[_currIndex].OnSlideStart;
        var onEnd = _slides[_currIndex].OnSlideEnd;

        _sequence.Append(_canvasGroup.DOFade(1, _transitionDuration).OnStart(() => onStart.Invoke()));
        _sequence.Append(_canvasGroup.DOFade(0, _transitionDuration)
            .OnComplete(() => {
                onEnd.Invoke();
                _currIndex++;
                _TMP_text.text = _slides[_currIndex].text;
            })
            .SetDelay(_slides[_currIndex].duration));

        if (_currIndex == _slides.Count - 1)
        {
            _currIndex = 0;
            state = GameState.NOT_STARTED;
        }
        else
        {
            _sequence.OnComplete(GoToNext);
        }
    }

    public void StopSlideShow()
    {
        _sequence?.Kill();
        _sequence = DOTween.Sequence();
        _sequence.Append(_canvasGroup.DOFade(0, _transitionDuration));
        _currIndex = 0;
        state = GameState.NOT_STARTED;
    }
}
