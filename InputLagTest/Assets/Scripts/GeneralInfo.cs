using System;
using UnityEngine;
using UnityEngine.UI;

public class GeneralInfo : MonoBehaviour
{
	public Text text;

	void Awake()
	{
		if(text == null) text = GetComponent<Text>();

		Options.onApply += UpdateText;
	}

	void UpdateText()
	{
		text.text = string.Format("VSync Count = {0}\nRefresh Rate = {1}\nMax Queued Frames = {2}\nGraphics API = {3}", QualitySettings.vSyncCount, Screen.currentResolution.refreshRate, QualitySettings.maxQueuedFrames, SystemInfo.graphicsDeviceVersion);
	}
}