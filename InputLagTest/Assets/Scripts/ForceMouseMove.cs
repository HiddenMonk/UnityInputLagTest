using System;
using UnityEngine;
using UnityEngine.UI;

public class ForceMouseMove : MonoBehaviour
{
	public Button button;
	public Text textSet;
	public Text text;

	int setEveryAmountOfFrames = 50;
	int current;
	bool isForward = true;
	Vector3 pos;
	Vector2 forward = new Vector2(50, 0);
	Vector2 backward = new Vector2(-50, 0);
	bool absolute = false;
	//It seems using absolute doesnt cause the GetAxisRaw to update unless if we also force a left click or something?

	bool isEnabled;

	void Start()
	{
		button.onClick.AddListener(Toggle);
		Toggle(true);

		pos = Input.mousePosition;
	}

	void Toggle() {Toggle(!isEnabled);}
	void Toggle(bool enable)
	{
		isEnabled = enable;

		if(isEnabled) button.image.color = Color.cyan;
		else button.image.color = Color.gray;
	}

	void OnApplicationFocus(bool isFocused)
	{
		Toggle(isFocused);
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.F1)) Toggle();
		if(!isEnabled) return;

		if(isForward) current++;
		else current--;

		if(current == setEveryAmountOfFrames)
		{
			ForceSetMouse.MoveMouse((int)forward.x, (int)forward.y, absolute);
			textSet.text = "Frame " + Time.frameCount + " SetMouseForwards";
			isForward = false;
		}
		else if(current == 0)
		{
			ForceSetMouse.MoveMouse((int)backward.x, (int)backward.y, absolute);
			textSet.text = "Frame " + Time.frameCount + " SetMouseBackwards";
			isForward = true;
		}

		string texts = string.Empty;
		if(Input.mousePosition != pos)
		{
			texts = "Frame " + Time.frameCount + " Moved Position \n";
			pos = Input.mousePosition;
		}
		if(Input.GetAxisRaw("Mouse X") != 0)
		{
			texts += "Frame " + Time.frameCount + " Moved Axis " + Input.GetAxisRaw("Mouse X");
		}
		//if(Input.GetKeyDown(KeyCode.Mouse0)) Debug.Log(Time.frameCount + " Pressed");

		if(texts != string.Empty) text.text = texts;
	}
}