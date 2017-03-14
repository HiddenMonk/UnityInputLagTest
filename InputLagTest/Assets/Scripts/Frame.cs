using System;
using UnityEngine;
using UnityEngine.UI;

public class Frame : MonoBehaviour
{
	public Text text;
	public bool changeColors = true;
	public Color[] colors;
	Color defaultColor;

	int currentColorIndex;

	public static Color currentFrameColor {get; private set;}
	public static int currentFrameColorIndex {get; private set;}
	public static Color[] frameColors {get; private set;}

	void Awake()
	{
		if(text == null) text = GetComponent<Text>();
		defaultColor = text.color;
		frameColors = colors;
	}

	void Update()
	{
		//We add 1 since when we see the text on the screen, we would be on the next frame.
		text.text = (Time.frameCount + 1).ToString();
		
		if(changeColors)
		{
			currentFrameColor = colors[currentColorIndex];
			currentFrameColorIndex = currentColorIndex;
			text.color = currentFrameColor;

			currentColorIndex++;
			if(currentColorIndex >= colors.Length) currentColorIndex = 0;
		}else{
			text.color = defaultColor;
		}
	}
}