using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PoolManager  {

	private static Dictionary<string , LinkedList<GameObject>> poolsDictionary;
	private static Transform deactivatedObjectsParent;

	public static void init ( Transform pooledObjectsContainer)
	{
	//	Debug.Log ("init");
		deactivatedObjectsParent = pooledObjectsContainer;
		poolsDictionary = new Dictionary<string, LinkedList<GameObject>> ();
	}

	public static GameObject getGameObjectFromPool(GameObject prefab, Vector3 v)
	{
		if (!poolsDictionary.ContainsKey (prefab.name)) {
			poolsDictionary [prefab.name] = new LinkedList<GameObject> ();
			Debug.Log ("pool create");
		}

		GameObject result;

		if (poolsDictionary [prefab.name].Count > 0) {
			result = poolsDictionary [prefab.name].First.Value;
			poolsDictionary [prefab.name].RemoveFirst ();
			result.transform.position = v;
			result.SetActive (true);
			//Debug.Log ("object from pool");
			return result;
		}

		result = Object.Instantiate (prefab, v, Quaternion.identity);
		result.transform.parent = deactivatedObjectsParent;
		result.name = prefab.name;
		return result;
	}
	public static GameObject getGameObjectFromPool(GameObject prefab, Vector3 v, NotPlayableEntity.Entities entity)
	{
		var name = entity.ToString();
		if (!poolsDictionary.ContainsKey (name)) {
			poolsDictionary [name] = new LinkedList<GameObject> ();
			//Debug.Log ("pool create");
		}

		GameObject result;

		if (poolsDictionary [name].Count > 0) {
			result = poolsDictionary [name].First.Value;
			poolsDictionary [name].RemoveFirst ();
			result.transform.position = v;
			result.SetActive (true);
			//Debug.Log ("object from pool");
			return result;
		}

		result = Object.Instantiate (prefab, v, Quaternion.identity);
		result.transform.parent = deactivatedObjectsParent;
		result.name = name;
		return result;
	}

	public static void putGameObjectToPool (GameObject target)
	{
		if (target) {
			if (poolsDictionary [target.name] != null) {
				poolsDictionary [target.name].AddFirst (target);
				target.transform.parent = deactivatedObjectsParent;
				target.SetActive (false);
				//Debug.Log ("put to pool");
			} /*else
				Debug.Log ("dictionary fail");*/
		}/*else
			Debug.Log ("target fail");*/
		
	}
	public static void putGameObjectToPool (GameObject target, NotPlayableEntity.Entities entity)
	{
		var name = entity.ToString();
		if (target) {
			if (poolsDictionary [name] != null) {
				poolsDictionary [name].AddFirst (target);
				target.transform.parent = deactivatedObjectsParent;
				target.SetActive (false);
				//Debug.Log ("put to pool");
			} /*else
				Debug.Log ("dictionary fail");*/
		}/*else
			Debug.Log ("target fail");*/
		
	}

}
