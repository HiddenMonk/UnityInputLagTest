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

	void Awake()
	{
		if(text == null) text = GetComponent<Text>();
		defaultColor = text.color;
	}

	void Update()
	{
		//We add 1 since when we see the text on the screen, we would be on the next frame.
		text.text = (Time.frameCount + 1).ToString();
		
		if(changeColors)
		{
			text.color = colors[currentColorIndex];

			currentColorIndex++;
			if(currentColorIndex >= colors.Length) currentColorIndex = 0;
		}else{
			text.color = defaultColor;
		}
	}
}