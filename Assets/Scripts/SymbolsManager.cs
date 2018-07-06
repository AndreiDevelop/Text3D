using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Collections;
using System;

public class SymbolsManager : MonoBehaviour
{
    public const char SPACE_CHAR = ' ';
    public const string SPECIAL_SYMBOL_SPACE = "Space";
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

    private GameObject _spaceObject;
    public GameObject SpaceObject
    {
        get
        {
            return _spaceObject;
        }
    }

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
        //yield return new WaitUntil(() => PoolManager.Instance != null);

        foreach (Transform bufTransform in transform)
        {  
            yield return new WaitForFixedUpdate();
            if (!bufTransform.name.Equals(SPECIAL_SYMBOL_SPACE))
            {
                char firstSymbolInPrefabName = bufTransform.name.ToCharArray()[0];
                InitializeSymbol(firstSymbolInPrefabName, bufTransform.gameObject);
            }
            else
            {
                _spaceObject = bufTransform.gameObject;
            }
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

    private Vector3 CalculateCentroid()
    {
        Vector3 centroid = Vector3.zero;

        foreach (Text3DObject bufSymbol in _usedSymbols)
            centroid += bufSymbol.transform.position;

        Vector3 bufCentroid = new Vector3(
            (float)(centroid.x / _usedSymbols.Count),
            (float)(centroid.y / _usedSymbols.Count),
            (float)(centroid.z / _usedSymbols.Count));

        return bufCentroid;
    }

    private IEnumerator CreateObjectOnCentreWithDelay(Transform parentTransform)
    {
        yield return new WaitUntil(() => _usedSymbols != null);
        
        //spawn empty object in the centre of all symbols position
        Transform cubeTransform = new GameObject().transform;//GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        cubeTransform.position = CalculateCentroid();

        //parent all symbols to spawn object
        yield return new WaitForEndOfFrame();
        SetUsedSymbolsParent(cubeTransform);

        //parent spawn object to preset object
        yield return new WaitForEndOfFrame();
        cubeTransform.SetParent(parentTransform);

        //reset position spawn object to zero
        yield return new WaitForEndOfFrame();
        cubeTransform.localPosition = Vector3.zero;
    }

    public void SetUsedSymbolsParent(Transform parentTransform)
    {
        foreach (Text3DObject bufSymbol in _usedSymbols)
            bufSymbol.transform.SetParent(parentTransform);
    }

    public void AlignUsedSymbols(Transform alignTransfrom)
    {
        StartCoroutine(CreateObjectOnCentreWithDelay(alignTransfrom));
    }
}

