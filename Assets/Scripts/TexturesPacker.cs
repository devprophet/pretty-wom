using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class TexturesPacker {

	public static Texture2D LoadTexture(byte[] data) {
		var tex = new Texture2D(Manager.TileWidth, Manager.TileHeight);
		tex.LoadImage(data);
		tex.filterMode = FilterMode.Point;
		return tex;
	}

	public static Texture2D PackTexture (string[] TexturesPath) {
		int finalSize = TexturesPath.Length * Manager.TileHeight;
		int index = 0;

		var packedTexture = new Texture2D(finalSize, Manager.TileHeight);
		packedTexture.filterMode = FilterMode.Point;

		foreach(var path in TexturesPath) {
			
			if (!File.Exists(path)) {
				Debug.Log("File not exist at this path: " + path);
				continue;
			}

			byte[] data = File.ReadAllBytes(path);
			var texture = LoadTexture(data);

			if (index == 4) {
				texture = MergeTexture(texture, new Color(0, 1f, 0, 1f));
			}

			for(int i = 0; i < Manager.TileWidth; i++) {
				for (int j = 0; j < Manager.TileHeight; j++) {
					packedTexture.SetPixel(i + (Manager.TileWidth * index), j, texture.GetPixel(i, j));
				}
			}
			index++;
		}
		packedTexture.Apply();
		return packedTexture;
	}

	public static Texture2D CreateCubeTexture(int[] sides, Texture2D PackedTexture) {
		var texture = new Texture2D(6 * Manager.TileWidth, Manager.TileHeight);
		var textures = new Texture2D[6];

		for(int i = 0; i < 6; i++) {
			textures[i] = GetTextureTile(PackedTexture, new Vector2(sides[i], 0));
			texture = MergeTexture(texture, textures[i], new Vector2(i, 0));
		}

		texture.Apply();
		return texture;

	}

	public static Texture2D GetTextureTile(Texture2D PackedTexture, Vector2 index) {
		int startY = Mathf.RoundToInt(index.y * Manager.TileHeight);
		int startX = Mathf.RoundToInt(index.x * Manager.TileWidth);

		var texture = new Texture2D(Manager.TileWidth, Manager.TileHeight);
		texture.filterMode = FilterMode.Point;

		for (int i = startX; i < startX + Manager.TileWidth; i++) {
			for (int j = startY; j < startY + Manager.TileHeight; j++) {
				texture.SetPixel(i - startX, j - startY, PackedTexture.GetPixel(i, j));
			}
		}
		texture.Apply();
		return texture;

	}

	public static Texture2D MergeTexture(Texture2D a, Texture2D b, Vector2 at) {
		var newTexture = new Texture2D(a.width, a.height);
		newTexture.filterMode = FilterMode.Point;

		var startX = Mathf.RoundToInt(at.x * b.width);
		var startY = Mathf.RoundToInt(at.y * b.height);

		for (int i = 0; i < a.width; i++) {
			for (int j = 0; j < a.height; j++) {
				newTexture.SetPixel(i, j, a.GetPixel(i, j));
			}
		}

		for (int i = startX; i < startX + b.width; i++) {
			for (int j = startY; j < startY + b.height; j++) {
				if(i < newTexture.width && j < newTexture.height && i >= 0 && j >= 0) {
					newTexture.SetPixel(i, j, b.GetPixel(i - startX, j - startY));
				}
			}
		}

		newTexture.Apply();
		return newTexture;
	}

	public static Texture2D MergeTexture(Texture2D a, Color b) {
		var newTexture = new Texture2D(a.width, a.height);
		newTexture.filterMode = FilterMode.Point;

		for(int i = 0; i < newTexture.width; i++) {
			for(int j = 0; j < newTexture.height; j++) {
				var color = a.GetPixel(i, j);
				color.r = color.r * b.r;
				color.g = color.g * b.g;
				color.b = color.b * b.b;
				newTexture.SetPixel(i, j, color);
			}
		}

		newTexture.Apply();
		return newTexture;
	}

}
