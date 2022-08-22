using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tesselation : MonoBehaviour
{
	struct Shape
	{
		// {0,0,0}, {6,0,0}, {3, 3, 0}, {9,3,0}
		public List<Vector3> geometry;
		// {0, 1, 2, 1, 4, 3}
		public List<int> topology;
	};

	string makeStringVector3(List<Vector3> list)
	{
		string r = "[";
		foreach (Vector3 v in list)
			r += v.ToString("F3") + " ";
		r += "]";
		return r;
	}

	string makeStringInt(List<int> list)
	{
		string r = "[";
		foreach (int i in list)
			r += i.ToString() + " ";
		r += "]";
		return r;
	}

	void Tessellate(Shape input)
	{
		for (int t = 0; t < input.topology.Count; t += 12)
		{
			Vector3 A = input.geometry[input.topology[t + 0]];
			Vector3 B = input.geometry[input.topology[t + 1]];
			Vector3 C = input.geometry[input.topology[t + 2]];
			Vector3 o = ((A + B) / 2.0f).normalized;
			Vector3 p = ((B + C) / 2.0f).normalized;
			Vector3 q = ((C + A) / 2.0f).normalized;
			int ia = input.topology[t + 0];
			int ib = input.topology[t + 1];
			int ic = input.topology[t + 2];
			int io = FindVertex(input.geometry, o);
			int ip = FindVertex(input.geometry, p);
			int iq = FindVertex(input.geometry, q);

			if (io == -1)
			{
				input.geometry.Add(o);
				io = input.geometry.Count - 1;
			}
			if (ip == -1)
			{
				input.geometry.Add(p);
				ip = input.geometry.Count - 1;
			}
			if (iq == -1)
			{
				input.geometry.Add(q);
				iq = input.geometry.Count - 1;
			}

			List<int> newT = new List<int>();
			for (int i = 0; i < t; i++)
				newT.Add(input.topology[i]);
			newT.Add(ia); newT.Add(io); newT.Add(iq);
			newT.Add(io); newT.Add(ib); newT.Add(ip);
			newT.Add(iq); newT.Add(ip); newT.Add(ic);
			newT.Add(io); newT.Add(ip); newT.Add(iq);
			for (int i = t + 3; i < input.topology.Count; i++)
				newT.Add(input.topology[i]);
			//Debug.Log(makeStringInt(input.topology));
			//Debug.Log(makeStringInt(newT));
			//Debug.Log(makeStringVector3(input.geometry));
			input.topology.Clear(); // Reemplazo la topología con la versión teselada.
			for (int i = 0; i < newT.Count; i++)
				input.topology.Add(newT[i]);
		}
	}

	int FindVertex(List<Vector3> list, Vector3 look)
	{
		return list.IndexOf(look);
	}

	// Start is called before the first frame update
	void Start()
	{
		Mesh myMesh = new Mesh();
		Shape i = new Shape();
		i.geometry = new List<Vector3>(){
	 		new Vector3(0, 0, 1),
			new Vector3(1, 0, 0),
			new Vector3(0, 0, -1),
			new Vector3(-1, 0, 0),
			new Vector3(0, 1, 0),
			new Vector3(0, -1, 0)
		};

		i.topology = new List<int>(){   0, 1, 4,
										 1, 2, 4,
								 		2, 3, 4,
										 3, 0, 4,

										 0, 5, 1,
								 		1, 5, 2,
								 		2, 5, 3,
								 		3, 5, 0
								};
		Tessellate(i);
		Tessellate(i);
		Tessellate(i);



		myMesh.vertices = i.geometry.ToArray();
		myMesh.triangles = i.topology.ToArray();
		myMesh.RecalculateNormals();
		MeshRenderer mr = gameObject.AddComponent<MeshRenderer>();
		mr.material = new Material(Shader.Find("Diffuse"));
		MeshFilter mf = gameObject.AddComponent<MeshFilter>();
		mf.mesh = myMesh;
	}

	// Update is called once per frame
	void Update()
	{

	}
}