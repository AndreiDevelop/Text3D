using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Text3DInputController : MonoBehaviour
{
    public delegate void Text3DInputHandler();
    public event Text3DInputHandler OnSaved;

    [SerializeField] Text3DController _text3DController = null;
    [SerializeField] Button _enter = null;
    [SerializeField] TextMeshProUGUI _observableText = null;

   // [SerializeField] private GameObject _panelTextInput;

    private void Start()
    {
        _enter.onClick.AddListener(ChangeText);
    }

    private void ChangeText()
    {
        _text3DController.Update3DText(_observableText.text);

        FinishText3DInput();

        if (OnSaved != null)
            OnSaved();
    }

    public void StartText3DInput()
    {
       // _panelTextInput.SetActive(true);
    }

    public void FinishText3DInput()
    {
      //  _panelTextInput.SetActive(false);
    }
}
