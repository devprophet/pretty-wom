using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVPacker : MonoBehaviour {

	public static Vector2[] GetCubeUVs(int xIndex, int yIndex) {
		var stepY = 1f / (float)Mathf.Round(Manager.MainTexture.height / Manager.TileHeight);
		var stepX = 1f / (float)Mathf.Round(Manager.MainTexture.width / Manager.TileWidth);

		return new Vector2[]{
			new Vector2( (xIndex * stepX) + 0,  (yIndex * stepY) + 0 ),
			new Vector2( (xIndex * stepX) + 0,  (yIndex * stepY) + stepY ),
			new Vector2( (xIndex * stepX) + stepX,  (yIndex * stepY) + stepY ),
			new Vector2( (xIndex * stepX) + stepX,  (yIndex * stepY) + 0 ),
		};
	}

}
