﻿using UnityEngine;

/*
Based on: http://wiki.unity3d.com/index.php?title=Singleton
*/

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T instance;
	public static T Instance
	{
		get
		{

			if (instance == null)
			{
				instance = (T) FindObjectOfType(typeof(T));

				if ( FindObjectsOfType(typeof(T)).Length > 1 )
				{

					return instance;
				}

				if (instance == null)
				{
					GameObject singleton = new GameObject();
					instance = singleton.AddComponent<T>();
					singleton.name = "(singleton) "+ typeof(T).ToString();

					DontDestroyOnLoad(singleton);

				} 
			}

			return instance;

		}
	}

}
