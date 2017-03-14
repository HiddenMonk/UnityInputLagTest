using System;
using UnityEngine;

public class CopyFrameColor : MonoBehaviour
{
	public Material myMaterial;
	public bool cursorCopy;
	Texture2D[] colorTextures;

	void Start()
	{
		if(Frame.frameColors != null)
		{
			colorTextures = new Texture2D[Frame.frameColors.Length];

			for(int i = 0; i < colorTextures.Length; i++)
			{
				Texture2D texture = new Texture2D(1, 1);
				texture.SetPixel(1, 1, Frame.frameColors[i]);
				colorTextures[i] = texture;
			}
		}
	}

	void LateUpdate()
	{
		myMaterial.color = Frame.currentFrameColor;

		if(cursorCopy && colorTextures != null && colorTextures.Length > Frame.currentFrameColorIndex)
		{
			Cursor.SetCursor(colorTextures[Frame.currentFrameColorIndex], Vector2.zero, CursorMode.Auto);
		}
	}
}
