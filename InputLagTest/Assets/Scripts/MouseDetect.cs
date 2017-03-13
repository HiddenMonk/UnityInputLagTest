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
	public Text firstDetectedFrameText;
	public InputField fontSizeInput;
	public InputField maxQueueInput;
	public Button togglePauseButton;
	public Button clearButton;

	Queue<string> framesDetectedQueue = new Queue<string>();
	public int maxQueue = 20;

	bool isPaused;

	string textDivider = " - ";

	void Awake()
	{
		ClearText();

		fontSizeInput.onEndEdit.AddListener(delegate{SetFontSize(fontSizeInput);});
		maxQueueInput.onEndEdit.AddListener(delegate{SetMaxQueue(fontSizeInput);});
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
		string detectedFrame = Time.frameCount.ToString();
		framesDetectedQueue.Enqueue(detectedFrame);

		if(firstDetectedFrameText.text == string.Empty)
		{
			firstDetectedFrameText.text = "First Detected Frame = " + detectedFrame;
		}

		framesDetectedQueue.Dequeue();
	}

	void DisplayText()
	{
		StringBuilder builder = new StringBuilder();
		int line = 1;
		foreach(string frameText in framesDetectedQueue.Reverse())
		{
			builder.Append(line);
			builder.Append(textDivider);
			builder.AppendLine(frameText);
			line++;
		}

		text.text = builder.ToString();
	}

	void SetFontSize(InputField input)
	{
		text.fontSize = Convert.ToInt32(fontSizeInput.text);
	}

	void SetMaxQueue(InputField input)
	{
		maxQueue = Convert.ToInt32(maxQueueInput.text);
		ClearText();
	}

	void ClearText()
	{
		text.text = string.Empty;
		firstDetectedFrameText.text = string.Empty;
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