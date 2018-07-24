using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour {

	public List<Chunk> Chunks = new List<Chunk>();

	public void AddCube(Vector3 Position, string type) {
		if ( Chunks.Count == 0) {
			AddChunk();
		}
		
		if (!Chunks[Chunks.Count - 1].Add(new Cube(type, Position, new bool[]{ true, true, true, true, true, true }))) {
			AddChunk();
			AddCube(Position, type);
		}
	}

	public void Remove(Vector3 Position) {
		
		Chunks[Chunks.Count - 1].Remove(Position);
	}

	public void AddChunk () {
		GameObject Chunk_GameObject = new GameObject("Chunk");
		var mr = Chunk_GameObject.AddComponent<MeshRenderer>();
		mr.material = Resources.Load<Material>("ChunkMat");
		mr.material.mainTexture = Manager.MainTexture;
		Chunk_GameObject.AddComponent<MeshFilter>();
		Chunk_GameObject.AddComponent<MeshCollider>();
		var chunk = Chunk_GameObject.AddComponent<Chunk>();
		Chunks.Add(chunk);
	}

}
