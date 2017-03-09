using System;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
	void Awake()
	{
		GameObject.DontDestroyOnLoad(gameObject);
	}
}