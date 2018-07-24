using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	
	[Tooltip("Si")]
	public bool canUse = false;
	public float rotationSpeed = 80f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.Alpha0)) {
			canUse = !canUse;
			if (canUse) {
				Cursor.lockState = CursorLockMode.Locked;
			} else {
				Cursor.lockState = CursorLockMode.None;
			}
		}
		
		if(!canUse)
			return;
		
		transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime);

	}
}
