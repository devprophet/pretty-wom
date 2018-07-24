using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

	public Transform cam;
	public float rotationSpeed = 80f;
	public float walkSpeed = 5f;
	public bool canUse = false;
	private float yAxis = 0f;
	
	// TODO: dwdwdwd
	void Update () {

		if (Input.GetKeyDown(KeyCode.Alpha0)) {
			canUse = !canUse;
		}

		if(!canUse) {
			if (Cursor.lockState == CursorLockMode.Locked) {
				Cursor.lockState = CursorLockMode.None;
			}
			return;
		}

		if (Cursor.lockState != CursorLockMode.Locked) {
			Cursor.lockState = CursorLockMode.Locked;
		}

		if (Input.GetKey(KeyCode.Space)) {
			yAxis = Mathf.Lerp(yAxis, 1f, walkSpeed * Time.deltaTime);
		} else {
			yAxis = Mathf.Lerp(yAxis, 0f, walkSpeed * Time.deltaTime);
		}

		cam.transform.Rotate(-Vector3.right * Input.GetAxis("Mouse Y") * rotationSpeed);
		transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * rotationSpeed);

		var direction = new Vector3(Input.GetAxis("Horizontal"), yAxis, Input.GetAxis("Vertical"));
		transform.Translate(direction * walkSpeed * Time.deltaTime);
	}
}
