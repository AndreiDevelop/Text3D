using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text3DCarriage : MonoBehaviour 
{
	public enum OffsetType
	{
		Width,
		Height,
		Depth
	}

    private const float SPACE_OFFSET = 0.15f;
    private const float STANDART_OFFSET = 5f;


	private Vector3 _currentPosition;
	public Vector3 CurrentPosition
	{
		get 
		{
			return _currentPosition;
		}
	}

	private Vector3 _prevPosition;
	public Vector3 PrevPosition
	{
		get 
		{
			return _prevPosition;
		}
	}

    private Vector3 _etalonPosition;

    private void Awake()
    {
        _etalonPosition = transform.position;
        ResetPosition();
    }

    #if UNITY_EDITOR
        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 5);
        }
    #endif

    public void ResetPosition()
    {
        _prevPosition = _etalonPosition;
        _currentPosition = _etalonPosition;
    }

    public void SetNewPosition(Vector3 newPosition)
	{
		_prevPosition = _currentPosition;
		_currentPosition = newPosition;
	}

	public void SetNewPosition(float objectSize, OffsetType type)
	{
		float offset = CalculateOffsetDependOfObjectSize (objectSize);
        Offset(offset, type);
	}

    public void MakeSpace()
    {
        Offset(SPACE_OFFSET, OffsetType.Width);
    }

    private void Offset(float offset, OffsetType type)
    {
        Vector3 offsetMask = GetOffsetMask(offset, type);

        _prevPosition = _currentPosition;
        _currentPosition = _prevPosition + offsetMask;
    }

	private Vector3 GetOffsetMask(float offset, OffsetType type)
	{
		Vector3 offsetMask = Vector3.zero;

		if (type == OffsetType.Width)
			offsetMask = new Vector3 (offset, 0f, 0f);
		else if(type == OffsetType.Height)
			offsetMask = new Vector3 (0f, offset,  0f);
		else if(type == OffsetType.Depth)
			offsetMask = new Vector3 (0f, 0f, offset);

		return offsetMask;
	}

	private float CalculateOffsetDependOfObjectSize(float objectSize)
	{
		//return (objectSize / 2f) + STANDART_OFFSET;
		return objectSize + STANDART_OFFSET;
	}
}
