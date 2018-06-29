using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class Text3DController : MonoBehaviour 
{
	[SerializeField]private Text3DSpawn _spawnText = null;
	[SerializeField]private Text3DCarriage _carriage = null;

    [SerializeField] SymbolsManager _symbolsManager = null;

    [SerializeField] TextMeshProUGUI _observableText = null;

    [SerializeField] Transform _textHolder;

    public List<char> _observableSymbolsList = new List<char>();

    public void Update3DText()
    {
        _symbolsManager.RemoveAllSymbols();
        _carriage.ResetPosition();

        _observableSymbolsList = _observableText.text.ToList<char>();

        foreach(char bufObservableSymbol in _observableSymbolsList)
        {
            foreach (KeyValuePair<char, Text3DObject> bufSymbol in _symbolsManager.AllSymbols)
            {
                if (bufObservableSymbol.Equals(' '))
                {
                    _carriage.MakeSpace();
                }
                else if(bufObservableSymbol.Equals(bufSymbol.Key))
                {
                    PutNewSymbol(bufSymbol.Value.gameObject);
                }
            }
        }
    }

    private void PutNewSymbol(GameObject symbol)
	{
		Text3DObject bufTextObject = _spawnText.SpawnObject (symbol, _carriage.CurrentPosition).GetComponent<Text3DObject> ();
        bufTextObject.Activate();
        bufTextObject.transform.SetParent(_textHolder);

		_carriage.SetNewPosition (bufTextObject.GetWidth (), Text3DCarriage.OffsetType.Width);

        _symbolsManager.AddUsedSymbol(bufTextObject);
	}

    private void RemoveSymbol()
    {
        _symbolsManager.RemoveLastUsedSymbol();
    }
}
