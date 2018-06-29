using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Collections;

public class SymbolsManager : MonoBehaviour
{
    //public int oneSymbolMaxCount = 10;

    //private int _allSymbolsMaxCount;
    //public int AllSymbolsMaxCount
    //{
    //    get
    //    {
    //        if (AllSymbols != null)
    //            _allSymbolsMaxCount = oneSymbolMaxCount * AllSymbols.Count;

    //        return _allSymbolsMaxCount;
    //    }
    //}

    private Dictionary<char, Text3DObject> _allSymbols = new Dictionary<char, Text3DObject>();
    public Dictionary<char, Text3DObject> AllSymbols
    {
        get
        {
            return _allSymbols;
        }
    }

    [SerializeField]
    private List<Text3DObject> _usedSymbols = new List<Text3DObject>();
    public List<Text3DObject> UsedSymbols
    {
        get
        {
            return _usedSymbols;
        }
    }

    private void Awake()
    {
        StartCoroutine(InitializeSymbolWithDelay());
    }

    private IEnumerator InitializeSymbolWithDelay()
    {
        yield return new WaitUntil(() => PoolManager.Instance != null);

        foreach (Transform bufTransform in transform)
        {  
            yield return new WaitForFixedUpdate();
            char firstSymbolInPrefabName = bufTransform.name.ToCharArray()[0];
            InitializeSymbol(firstSymbolInPrefabName, bufTransform.gameObject);
        }
    }

    private void InitializeSymbol(char symbolName, GameObject symbolPrefab)
    {
           _allSymbols.Add(symbolName, symbolPrefab.GetComponent<Text3DObject>());
           //PoolManager.Instance.CreatePool(symbolPrefab, oneSymbolMaxCount, symbolPrefab.transform);
    }

    public void AddUsedSymbol(Text3DObject symbolObj)
    {
        _usedSymbols.Add(symbolObj);
    }

    public void RemoveLastUsedSymbol()
    {
        int lastElementNumber = UsedSymbols.Count - 1;
        PoolObject bufObject = UsedSymbols[lastElementNumber].PoolObject;

        UsedSymbols.RemoveAt(lastElementNumber);

        Destroy(bufObject.gameObject);
        //bufObject.Deactivate();
    }

    public void RemoveAllSymbols()
    {
        foreach (Text3DObject butText3DObj in _usedSymbols)
            Destroy(butText3DObj.gameObject);

        _usedSymbols.Clear();
    }
}
