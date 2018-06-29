using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text3DSpawn : MonoBehaviour 
{
	public GameObject SpawnObject(GameObject objectToSpawn, Vector3 position)
	{
        //GameObject bufGameObject = PoolManager.Instance.ReuseObject(objectToSpawn, position, Quaternion.identity).gameObject;

        GameObject bufGameObject = Instantiate(objectToSpawn, position, Quaternion.identity).gameObject;

        return bufGameObject;
	}
}
