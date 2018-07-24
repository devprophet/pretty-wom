using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube {

	public static Dictionary<string, int> CubeTypes = new Dictionary<string, int>(){
		{"Grass", 0},
		{"Dirt", 1},
	};

	public Vector3 Position {get; private set;}
	public float Size 		{get; private set;}
	public bool[] Faces 	{get; private set;}
	public string type		{get; private set;}

	public List<Vector3> Vertices 	{get; private set;}
	public List<int> Triangles 		{get; private set;}
	public List<Vector2> UVs 		{get; private set;}

	public bool Calculated {get; private set;}

	public Cube(string Type, Vector3 Position, bool[] Faces, float Size = 1f) {
		
		this.Vertices 	= new List<Vector3>();
		this.Triangles 	= new List<int>();
		this.UVs 		= new List<Vector2>();

		this.Position = Position;
		this.Size 	= Size;
		this.Faces 	= Faces;
		this.Calculated = false;
		this.type = Type;
		
	}

	public void SetFaces(bool[] Faces) {
		this.Faces = Faces;
	}

	public void SetType(string type) {
		if(CubeTypes.ContainsKey(type)) {
			this.type = type;
		} else {
			Debug.LogWarningFormat("The cube type \"{0}\" not exist!", type);
		}
	}

	public void Calculate(int index = 0) {

		Vertices.Clear();
		Triangles.Clear();
		UVs.Clear();

		var s = Size / 2f;

		Vector3[] p = new Vector3[] {
			new Vector3(-s, -s, -s) + Position, // 0 0
			new Vector3(-s,  s, -s) + Position, // 1 1
			new Vector3( s,  s, -s) + Position, // 2 2
			new Vector3( s, -s, -s) + Position, // 3 3

			new Vector3(-s, -s,  s) + Position, // 4 0
			new Vector3(-s,  s,  s) + Position, // 5 1
			new Vector3( s,  s,  s) + Position, // 6 2
			new Vector3( s, -s,  s) + Position, // 7 3
		};

		for(int i = 0; i < Faces.Length; i++) {
			if(Faces[i]) {

				Triangles.AddRange(AddTriangles(index + Vertices.Count));
				UVs.AddRange(UVPacker.GetCubeUVs(i, CubeTypes[type]));

				switch(i) {
					case 0: Vertices.AddRange(new Vector3[]{ p[0], p[1], p[2], p[3]}); break;
					case 1: Vertices.AddRange(new Vector3[]{ p[3], p[2], p[6], p[7]}); break;
					case 2: Vertices.AddRange(new Vector3[]{ p[7], p[6], p[5], p[4]}); break;
					case 3: Vertices.AddRange(new Vector3[]{ p[4], p[5], p[1], p[0]}); break;
					case 4: Vertices.AddRange(new Vector3[]{ p[1], p[5], p[6], p[2]}); break;
					case 5: Vertices.AddRange(new Vector3[]{ p[4], p[0], p[3], p[7]}); break;
					default: break;
				}

			}
		}

		this.Calculated = true;
	}

	public int[] AddTriangles(int index) {
		return new int[]{ 0 + index, 1 + index, 2 + index, 0 + index, 2 + index, 3 + index, };
	}

}
