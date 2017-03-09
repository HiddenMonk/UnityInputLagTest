using UnityEngine;

//This is placed on the cameras gameobject
public class MouseTranslate : MonoBehaviour
{
	public static float mouseSpeed;
	public static bool useButtonInsteadOfMouseMove;
	public static KeyCode inputKey = KeyCode.Mouse0;

	void Update()
	{
		if(useButtonInsteadOfMouseMove) //Could be keyboard or mouse buttons
		{
			if(Input.GetKeyDown(inputKey))
			{
				transform.Rotate(0, mouseSpeed, 0);
			}
		}else{
			transform.Rotate(0, Input.GetAxisRaw("Mouse X") * mouseSpeed, 0);
		}
	}
}