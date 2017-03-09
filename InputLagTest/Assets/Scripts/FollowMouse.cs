using System;
using UnityEngine;
using System.Collections.Generic;

public class FollowMouse : MonoBehaviour
{
	public static Vector3 defaultPosition;

	List<GameObject> poles = new List<GameObject>();

	float sensitivity = .1f;

	void Awake()
	{
		defaultPosition = transform.position;
		Options.SetCursorToCenter();
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.F1)) ResetCube();
		if(Input.GetKeyDown(KeyCode.F2)) ClearPoles();

		if(Input.GetKeyDown(KeyCode.Mouse0))
		{
			GameObject pole = GameObject.CreatePrimitive(PrimitiveType.Capsule);
			Vector3 position = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
			position.z = transform.position.z;
			pole.transform.position = position;
			pole.transform.localScale = new Vector3(.1f, 10f, .1f);
			poles.Add(pole);
		}

		transform.Translate(Vector3.right * (Input.GetAxisRaw("Mouse X") * sensitivity));
	}

	public void ResetCube()
	{
		Options.SetCursorToCenter();
		transform.position = defaultPosition;
	}

	public void ClearPoles()
	{
		for(int i = 0; i < poles.Count; i++)
		{
			GameObject.Destroy(poles[i]);
		}
		poles = new List<GameObject>();
	}
}