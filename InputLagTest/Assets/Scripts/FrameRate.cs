using System;
using UnityEngine;
using UnityEngine.UI;

public class FrameRate : MonoBehaviour
{
	public Text text;
	public float updateRate = .2f;

	float deltaTime;
	float updateInterval;
	float fps;

	void Awake()
	{
		if(text == null) text = GetComponent<Text>();
	}

	void Update()
	{
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

		updateInterval -= Time.deltaTime;
		if(updateInterval < 0)
		{
			//float msec = deltaTime * 1000.0f;
			fps = 1.0f / deltaTime;
			updateInterval = updateRate;

			text.text = string.Format("{0:0}", fps);
		}
	}
}