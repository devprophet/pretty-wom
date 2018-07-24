using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class Chunk : MonoBehaviour {

	public const int CHUNK_MAX_CUBE = 10 * 10 * 10;

	private List<Vector3> Vertices 	= new List<Vector3>();
	private List<int> Triangles 	= new List<int>();
	private List<Vector2> UVs 		= new List<Vector2>();

	public Dictionary<Vector3, Cube> cubes = new Dictionary<Vector3, Cube>();

	public bool Add(Cube cube) {
		if (cubes.Count + 1 < CHUNK_MAX_CUBE) {
			cubes.Add(cube.Position, cube);
			Calculate();
			return true;
		} else {
			return false;
		}
	}

	public void Remove(Vector3 Position) {
		cubes.Remove(Position);
		Calculate();
	}

	public void Calculate() {
		Vertices.Clear();
		Triangles.Clear();
		UVs.Clear();

		var keys = cubes.Keys.ToArray();

		for (int i = 0; i < keys.Length; i++) {

			var up 		= !cubes.ContainsKey(keys[i] + new Vector3(0,  1, 0));
			var down 	= !cubes.ContainsKey(keys[i] + new Vector3(0, -1, 0));
			var right 	= !cubes.ContainsKey(keys[i] + new Vector3( 1, 0, 0));
			var left 	= !cubes.ContainsKey(keys[i] + new Vector3(-1, 0, 0));
			var front 	= !cubes.ContainsKey(keys[i] + new Vector3(0, 0,  1));
			var back 	= !cubes.ContainsKey(keys[i] + new Vector3(0, 0, -1));

			cubes[keys[i]].SetFaces(new bool[]{ back, right, front, left, up, down });
			cubes[keys[i]].Calculate(Vertices.Count);

			Vertices.AddRange(cubes[keys[i]].Vertices);
			Triangles.AddRange(cubes[keys[i]].Triangles);
			UVs.AddRange(cubes[keys[i]].UVs);
		}

		Mesh m = new Mesh();

		m.SetVertices(Vertices);
		m.SetTriangles(Triangles, 0);
		m.SetUVs(0, UVs);

		m.RecalculateBounds();
		m.RecalculateNormals();
		m.RecalculateTangents();
		
		GetComponent<MeshFilter>().mesh = m;
		GetComponent<MeshCollider>().sharedMesh = m;

	}

	private List<int> RecalculateTriangles(List<int> triangles, int with) {
		List<int> recalculatedTriangles = new List<int>();
		for(int i = 0; i < triangles.Count; i++) {
			recalculatedTriangles.Add(triangles[i] + with);
		}
		return recalculatedTriangles;
	}

}
