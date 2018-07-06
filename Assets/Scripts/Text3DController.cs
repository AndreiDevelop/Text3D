using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

public class Text3DController : MonoBehaviour 
{
	[SerializeField]private Text3DSpawn _spawnText = null;
	[SerializeField]private Text3DCarriage _carriage = null;

    [SerializeField] SymbolsManager _symbolsManager = null;

    [SerializeField] Transform _textHolder;

    public List<char> _observableSymbolsList = new List<char>();

    public void Update3DText(string text)
    {
        _symbolsManager.RemoveAllSymbols();
        _carriage.ResetPosition();

        _observableSymbolsList = text.ToList<char>();

        foreach(char bufObservableSymbol in _observableSymbolsList)
        {
            //put space
            if (bufObservableSymbol.Equals(SymbolsManager.SPACE_CHAR))
            {
                PutNewSymbol(_symbolsManager.SpaceObject);
            }

            //find match symbols in symbol manager list
            foreach (KeyValuePair<char, Text3DObject> bufSymbol in _symbolsManager.AllSymbols)
            {
                //put match symbol
                if (bufObservableSymbol.Equals(bufSymbol.Key))
                {
                    PutNewSymbol(bufSymbol.Value.gameObject);
                }
            }
        }

        _symbolsManager.AlignUsedSymbols(_textHolder.transform);
    }

    private void PutNewSymbol(GameObject symbol)
    {
        StartCoroutine(SpawnWithDelay(symbol));
    }

    private IEnumerator SpawnWithDelay(GameObject symbol)
    {
        Text3DObject bufTextObject = _spawnText.SpawnObject(symbol, transform).GetComponent<Text3DObject>();

        yield return new WaitForEndOfFrame();
        bufTextObject.transform.localPosition = _carriage.CurrentPosition;

        bufTextObject.Activate();
        _carriage.SetNewPosition(bufTextObject.GetWidth(), Text3DCarriage.OffsetType.Width);
        _symbolsManager.AddUsedSymbol(bufTextObject);

    }

    private void RemoveSymbol()
    {
        _symbolsManager.RemoveLastUsedSymbol();
    }
}
