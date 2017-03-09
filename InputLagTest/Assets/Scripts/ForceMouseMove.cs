using System;
using UnityEngine;
using UnityEngine.UI;

public class ForceMouseMove : MonoBehaviour
{
	public Text textSet;
	public Text textMP;
	public Text text;

	bool isForward = true;
	Vector3 pos;
	Vector2 forward = new Vector2(50, 0);
	bool absolute = false;
	//It seems using absolute doesnt cause the GetAxisRaw to update unless if we also force a left click or something?

	void Start()
	{
		pos = Input.mousePosition;
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			MoveMouse();
		}
		
		DetectMouse();
	}

	void MoveMouse()
	{
		if(isForward)
		{
			ForceSetMouse.MoveMouse((int)forward.x, (int)forward.y, absolute);
			textSet.text = "Frame " + Time.frameCount + " SetMouseForwards";
			isForward = false;
		}
		else
		{
			ForceSetMouse.MoveMouse((int)-forward.x, (int)-forward.y, absolute);
			textSet.text = "Frame " + Time.frameCount + " SetMouseBackwards";
			isForward = true;
		}
	}

	void DetectMouse()
	{
		if(Input.mousePosition != pos)
		{
			textMP.text = "Frame " + Time.frameCount + " Moved Position \n";
			pos = Input.mousePosition;
		}
		if(Input.GetAxisRaw("Mouse X") != 0)
		{
			text.text = "Frame " + Time.frameCount + " Moved Axis " + Input.GetAxisRaw("Mouse X");
		}
		//if(Input.GetKeyDown(KeyCode.Mouse0)) Debug.Log(Time.frameCount + " Pressed");
	}
}