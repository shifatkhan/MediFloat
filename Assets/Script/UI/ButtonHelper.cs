using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ButtonHelper : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text;

    public bool IsDisabled;
    public UnityEvent OnClickEvent;

    public void SetText(string newText)
    {
        _text.text = newText;
    }

    public void OnClick()
    {
        if (IsDisabled)
        {
            return;
        }

        OnClickEvent.Invoke();
    }

    private void OnDestroy()
    {
        OnClickEvent.RemoveAllListeners();
    }
}
