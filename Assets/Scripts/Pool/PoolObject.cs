using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class PoolObject : MonoBehaviour 
{
	private GameObject _curGameObject = null;
	private Transform _curTransform = null;

	void Start()
	{
		_curGameObject = gameObject;
		_curTransform = transform;
	}

	public virtual void OnObjectReuse() 
	{

	}

	public void Deactivate()
	{
		_curGameObject.SetActive (false);

		_curTransform.position = Vector3.zero;
		_curTransform.rotation = Quaternion.identity;
	}

	protected void Destroy() 
	{
		gameObject.SetActive (false);
	}
}
