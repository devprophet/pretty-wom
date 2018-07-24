using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

	public static int TileHeight = 16;
	public static int TileWidth = 16;
	public static Texture2D texture;
	public Texture2D textureCubeTest;
	public string textureFolder;
	public string[] textures;
	

	public static Texture2D MainTexture;

	int textureX = 0;
	int textureY = 0;

	Texture2D TestTexture;

	void OnGUI()
	{
		if(MainTexture != null) {
			GUILayout.Box(MainTexture);
		}
	}

	void Start() {

		var _textures = new string[textures.Length];
		for(int i = 0; i < textures.Length; i++) {
			_textures[i] = textureFolder + "/" + textures[i];
		}

		texture = TexturesPacker.PackTexture(_textures);

		textureCubeTest = TexturesPacker.CreateCubeTexture(new int[]{2, 2, 2, 2, 4, 0}, texture);
		MainTexture = new Texture2D(TileWidth * 6, TileHeight * 2);

		MainTexture = TexturesPacker.MergeTexture(MainTexture, TexturesPacker.CreateCubeTexture(new int[]{2, 2, 2, 2, 4, 0}, texture), new Vector2(0, 0));
		MainTexture = TexturesPacker.MergeTexture(MainTexture, TexturesPacker.CreateCubeTexture(new int[]{0, 0, 0, 0, 0, 0}, texture), new Vector2(0, 1));
		var chunkManager = GameObject.FindObjectOfType<ChunkManager>();
		chunkManager.AddCube(Vector3.zero, "Grass");

	}

}
