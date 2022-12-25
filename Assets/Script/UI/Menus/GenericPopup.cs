using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GenericPopup : Singleton<GenericPopup>
{
    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private CanvasGroupHelper _canvasGroupHelper;

    [Header("Buttons")]
    [SerializeField]
    private ButtonHelper _buttonPrefab;
    [SerializeField]
    private Transform _buttonsContainer;

    public class GenericPopupButtonParam
    {
        public string ButtonText;
        public UnityEvent OnClick;

        public GenericPopupButtonParam(string buttonText)
        {
            ButtonText = buttonText;
            OnClick = new UnityEvent();
        }
    }

    public void Show(string text, List<GenericPopupButtonParam> buttonsParam)
    {
        _text.text = text;
        _canvasGroupHelper.Show();

        foreach (var param in buttonsParam)
        {
            ButtonHelper btnGO = Instantiate(_buttonPrefab, _buttonsContainer);
            btnGO.SetText(param.ButtonText);
            btnGO.OnClickEvent.AddListener(() => param.OnClick.Invoke());
        }
    }

    [NaughtyAttributes.Button("Trigger Hide")]
    public void Hide()
    {
        _canvasGroupHelper.Hide();

        foreach (Transform item in _buttonsContainer)
        {
            Destroy(item.gameObject);
        }
    }
}
