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

    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;
	private Material _materil;
	private Transform _transform;

    private Vector3 ObjectRealSize = Vector3.zero;

    void Awake()
	{
        _meshFilter = GetComponent<MeshFilter>();
        _poolObject = GetComponent<PoolObject>();
		_meshRenderer = GetComponent<MeshRenderer> ();
		_materil = _meshRenderer.material;

        CalculateObjectSizeInRealWorld();
        //_meshRenderer.enabled = false;
    }

    private void CalculateObjectSizeInRealWorld()
    {
        Vector3 objSize = _meshFilter.sharedMesh.bounds.size;
        ObjectRealSize = Matrix4x4.Scale(transform.localScale) * objSize;
    }

    public void Activate()
    {
        _meshRenderer.enabled = true;
    }

	public float GetWidth()
	{
        return ObjectRealSize.x;//_meshRenderer.bounds.size.x;

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
