using UnityEngine;
using System.Collections;

public class ModelMesh : MonoBehaviour {

	public float width = 1f;
	public float height = 1f;

	// Use this for initialization
	void Start () {
		Mesh mesh = new Mesh();
		gameObject.GetComponent<MeshFilter>().mesh = mesh;

		Vector3[] vertices = new Vector3[4];
		vertices[0] = new Vector3(0.0f, 0.0f, 0.0f);
		vertices[1] = new Vector3(width, 0.0f, 0.0f);
		vertices[2] = new Vector3(0.0f, height, 0.0f);
		vertices[3] = new Vector3(width, height, 0.0f);
		mesh.vertices = vertices;

		int[] tri = new int[6];
		// Lower left triangle of a quad
		tri[0] = 0;
		tri[1] = 2;
		tri[2] = 1;
		// Upper right triangle of a quad
		tri[3] = 2;
		tri[4] = 3;
		tri[5] = 1;
		mesh.triangles = tri;

		//Color:
		Material material = new Material(Shader.Find("Standard"));
		material.color = Color.red;
		GetComponent<Renderer>().material = material;
	}
}
