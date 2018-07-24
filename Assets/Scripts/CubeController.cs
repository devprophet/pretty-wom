using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour {

	ChunkManager chunkManager;

	// Use this for initialization
	void Start () {
		chunkManager = GameObject.FindObjectOfType<ChunkManager>();
	}
	
	// Update is called once per frame
	void Update () {

		var mouseRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
		RaycastHit hit;
		if (Physics.Raycast(mouseRay, out hit)) {

			var x = Mathf.RoundToInt(hit.point.x - (hit.normal.x * 0.5f));
			var y = Mathf.RoundToInt(hit.point.y - (hit.normal.y * 0.5f));
			var z = Mathf.RoundToInt(hit.point.z - (hit.normal.z * 0.5f));

			var pointRounded = new Vector3(x, y, z);

			Debug.DrawLine(mouseRay.origin, hit.point, Color.yellow);
			Debug.DrawLine(mouseRay.origin, pointRounded, Color.blue);

			Debug.DrawLine(pointRounded, pointRounded + new Vector3(-1, 0, 0), Color.red);
			Debug.DrawLine(pointRounded, pointRounded + new Vector3( 1, 0, 0), Color.red);
			Debug.DrawLine(pointRounded, pointRounded + new Vector3(0, -1, 0), Color.red);
			Debug.DrawLine(pointRounded, pointRounded + new Vector3(0,  1, 0), Color.red);
			Debug.DrawLine(pointRounded, pointRounded + new Vector3(0, 0, -1), Color.red);
			Debug.DrawLine(pointRounded, pointRounded + new Vector3(0, 0,  1), Color.red);

			if (Input.GetMouseButtonDown(0)) {
				chunkManager.AddCube(pointRounded + hit.normal, "Dirt");
			}

			if (Input.GetMouseButtonDown(1)) {
				chunkManager.Remove(pointRounded);
			}

		}
	}
}
