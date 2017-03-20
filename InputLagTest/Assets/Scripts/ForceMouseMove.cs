using System;
using UnityEngine;
using UnityEngine.UI;

public class ForceMouseMove : MonoBehaviour
{
	public Text textSet;
	public Text textMP;
	public Text text;
	public Text textPress;
	public InputField moveMouseAmount;

	public bool autoMove;
	public float seconds = 1f;

	public int mouseSetAmount = 1;

	float currentTime;

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
		if(autoMove)
		{
			AutoMove();
		}
		else if(Input.GetKeyDown(KeyCode.Space))
		{
			MoveMouseLoop();
		}

		DetectMouse();
	}

	public void EnableAutoMove(bool value)
	{
		autoMove = value;
	}

	void AutoMove()
	{
		currentTime += Time.deltaTime;
		if(currentTime > seconds)
		{
			MoveMouseLoop();
			currentTime = 0;
		}
	}

	void MoveMouseLoop()
	{
		for(int i = 0; i < mouseSetAmount; i++)
		{
			MoveMouse();
		}
		PressMouseLeft();
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

	void PressMouseLeft()
	{
		if(isForward)
		{
			ForceSetMouse.PressMouseLeft((int)forward.x, (int)forward.y, absolute);
		}else{
			ForceSetMouse.PressMouseLeft((int)-forward.x, (int)-forward.y, absolute);
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
		if(Input.GetKeyDown(KeyCode.Mouse0))
		{
			textPress.text = "Frame " + Time.frameCount + " Pressed Left Mouse";
		}
	}

	public void SetMoveMouseAmount(string value)
	{
		int amount = 0;
		if(int.TryParse(value, out amount))
		{
			if(amount <= 0) amount = 1;
			else if(amount % 2 == 0) amount += 1;
			mouseSetAmount = amount;
			moveMouseAmount.text = amount.ToString();
		}
	}
}