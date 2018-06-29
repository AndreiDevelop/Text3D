using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(PoolObject))]
public class Text3DObject : MonoBehaviour 
{
    private PoolObject _poolObject;
    public PoolObject PoolObject
    {
        get
        {
            return _poolObject;
        }
    }

    private MeshRenderer _meshRenderer;
	private Material _materil;
	private Transform _transform;

    void Awake()
	{
        _poolObject = GetComponent<PoolObject>();
		_meshRenderer = GetComponent<MeshRenderer> ();
		_materil = _meshRenderer.material;

        _meshRenderer.enabled = false;
	}

    public void Activate()
    {
        _meshRenderer.enabled = true;
    }

	public float GetWidth()
	{
		return _meshRenderer.bounds.size.x;
	}

	public float GetHeight()
	{
		return _meshRenderer.bounds.size.y;
	}

	public float GetDepth()
	{
		return _meshRenderer.bounds.size.z;
	}
}
