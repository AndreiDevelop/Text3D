using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour 
{
	private Dictionary<int,Queue<ObjectInstance>> poolDictionary = new Dictionary<int, Queue<ObjectInstance>> ();

	private static PoolManager _instance;

	public static PoolManager Instance 
	{
		get 
		{
			if (_instance == null) 
			{
				_instance = FindObjectOfType<PoolManager> ();
			}
			return _instance;
		}
	}

	public void CreatePool(GameObject prefab, int poolSize) 
	{
		int poolKey = prefab.GetInstanceID ();

		if (!poolDictionary.ContainsKey (poolKey)) 
		{
			poolDictionary.Add (poolKey, new Queue<ObjectInstance> ());

			GameObject poolHolder = new GameObject (prefab.name + " pool");
			poolHolder.transform.parent = transform;

			for (int i = 0; i < poolSize; i++) 
			{
				ObjectInstance newObject = new ObjectInstance(Instantiate (prefab) as GameObject);
				poolDictionary [poolKey].Enqueue (newObject);
				newObject.SetParent (poolHolder.transform);
			}
		}
	}

	public void CreatePool(GameObject prefab, int poolSize, Transform parentTransform) 
	{
		int poolKey = prefab.GetInstanceID ();

		if (!poolDictionary.ContainsKey (poolKey)) 
		{
			poolDictionary.Add (poolKey, new Queue<ObjectInstance> ());

			GameObject poolHolder = new GameObject (prefab.name + " pool");
			poolHolder.transform.parent = parentTransform;

			for (int i = 0; i < poolSize; i++) 
			{
				ObjectInstance newObject = new ObjectInstance(Instantiate (prefab) as GameObject);
				poolDictionary [poolKey].Enqueue (newObject);
				newObject.SetParent (poolHolder.transform);
			}
		}
	}

	public PoolObject ReuseObject(GameObject prefab, Vector3 position, Quaternion rotation) 
	{
        ObjectInstance objectToReuse = null;
        int poolKey = prefab.GetInstanceID ();

		if (poolDictionary.ContainsKey (poolKey)) 
		{
			objectToReuse = poolDictionary [poolKey].Dequeue ();
			poolDictionary [poolKey].Enqueue (objectToReuse);

			objectToReuse.Reuse (position, rotation);
		}

        return objectToReuse.poolObject;
	}

	public class ObjectInstance 
	{
        public PoolObject poolObject;

        private GameObject _gameObject;
		private Transform _objTransform;

		private bool hasPoolObjectComponent;
		
		public ObjectInstance(GameObject objectInstance) 
		{
			_gameObject = objectInstance;
			_objTransform = _gameObject.transform;
			_gameObject.SetActive(false);

			if (_gameObject.GetComponent<PoolObject>()) 
			{
				hasPoolObjectComponent = true;
				poolObject = _gameObject.GetComponent<PoolObject>();
			}
		}

		public void Reuse(Vector3 position, Quaternion rotation) 
		{
			_gameObject.SetActive (true);

			_objTransform.position = position;
			_objTransform.rotation = rotation;

			if (hasPoolObjectComponent) 
			{
				poolObject.OnObjectReuse ();
			}
		}
			
		public void SetParent(Transform parent) 
		{
			_objTransform.parent = parent;
		}
	}
}
