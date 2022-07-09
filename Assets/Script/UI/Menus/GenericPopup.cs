using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GenericPopup : Singleton<GenericPopup>
{
    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private CanvasGroupHelper _canvasGroupHelper;

    public void Show(string text)
    {
        _text.text = text;
        _canvasGroupHelper.Show();
    }
}
