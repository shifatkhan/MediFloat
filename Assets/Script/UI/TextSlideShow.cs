using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

/// <summary>
/// TODO: Add unity events list to each message in case we want to add anims.
/// </summary>
public class TextSlideShow : MonoBehaviour
{
    [SerializeField]
    private bool _animateOnStart = true;
    [SerializeField]
    private TextMeshProUGUI _TMP_text;
    [SerializeField]
    private CanvasGroup _canvasGroup;
    [TextArea(3, 10)]
    [SerializeField]
    private List<string> _textSlides;
    [SerializeField]
    private float _transitionDuration = 10f;
    [SerializeField]
    private float _delay = 5f;

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
        if (_textSlides.Count == 0 || state == GameState.STARTED)
            return;

        state = GameState.STARTED;

        _currIndex = 0;

        _TMP_text.text = _textSlides[_currIndex];
        GoToNext();
    }

    private void GoToNext()
    {
        _sequence?.Kill();
        _sequence = DOTween.Sequence();

        _sequence.Append(_canvasGroup.DOFade(1, _transitionDuration));
        _sequence.Append(_canvasGroup.DOFade(0, _transitionDuration)
            .OnComplete(() => {
                _currIndex++;
                _TMP_text.text = _textSlides[_currIndex]; 
            })
            .SetDelay(_delay));

        if (_currIndex == _textSlides.Count - 1)
        {
            _currIndex = 0;
            state = GameState.NOT_STARTED;
        }
        else
        {
            _sequence.OnComplete(GoToNext);
        }
    }
}
