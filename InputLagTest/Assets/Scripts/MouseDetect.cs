using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Events;
using System.Linq;

public class MouseDetect : MonoBehaviour
{
	public Text text;
	public InputField fontSizeInput;
	public Button togglePauseButton;
	public Button clearButton;

	Queue<string> framesDetectedQueue = new Queue<string>();
	int maxQueue = 20;

	bool isPaused;

	void Awake()
	{
		SetQueue();

		fontSizeInput.onEndEdit.AddListener(delegate{SetFontSize(fontSizeInput);});
		togglePauseButton.onClick.AddListener(TogglePauseDetection);
		clearButton.onClick.AddListener(ClearText);
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.F1)) TogglePauseDetection();
		if(Input.GetKeyDown(KeyCode.F2)) ClearText();

		if(isPaused) return;

		if(MouseTranslate.useButtonInsteadOfMouseMove)
		{
			if(Input.GetKeyDown(MouseTranslate.inputKey))
			{
				EnqueueFrame();
			}
		}
		else if(Input.GetAxisRaw("Mouse X") != 0 || Input.GetAxisRaw("Mouse Y") != 0)
		{
			EnqueueFrame();
		}

		DisplayText();
	}

	void EnqueueFrame()
	{
		framesDetectedQueue.Enqueue(Time.frameCount.ToString());
		framesDetectedQueue.Dequeue();
	}

	void DisplayText()
	{
		StringBuilder builder = new StringBuilder();
		foreach(string frameText in framesDetectedQueue.Reverse())
		{
			builder.AppendLine(frameText);
		}

		text.text = builder.ToString();
	}

	void SetFontSize(InputField input)
	{
		text.fontSize = Convert.ToInt32(fontSizeInput.text);
	}

	void ClearText()
	{
		text.text = string.Empty;
		framesDetectedQueue.Clear();
		SetQueue();
	}

	void TogglePauseDetection()
	{
		isPaused = !isPaused;
		if(isPaused) togglePauseButton.image.color = Color.gray;
		else togglePauseButton.image.color = Color.cyan;
	}

	void SetQueue()
	{
		for(int i = 0; i < maxQueue; i++)
		{
			framesDetectedQueue.Enqueue(string.Empty);
		}
	}
}