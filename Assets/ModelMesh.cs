using UnityEngine;
using System.Collections;

public class ModelMesh : MonoBehaviour {

	public float width = 0.5f;
	public float height = 0.5f;

	// Use this for initialization
	void Start () {
		Mesh mesh = new Mesh();
		gameObject.GetComponent<MeshFilter>().mesh = mesh;

		Vector3[] vertices = new Vector3[9];

		//front quad:
		vertices[0] = new Vector3(0.0f, 0.0f, 0.0f);
		vertices[1] = new Vector3(width, 0.0f, 0.0f);
		vertices[2] = new Vector3(0.0f, height, 0.0f);
		vertices[3] = new Vector3(width, height, 0.0f);

		//rear quad:
		vertices[4] = new Vector3(0.0f, 0.0f, width/2);
		vertices[5] = new Vector3(width, 0.0f, width/2);
		vertices[6] = new Vector3(0.0f, height, width/2);
		vertices[7] = new Vector3(width, height, width/2);

		//front 'nose' point:
		vertices[8] = new Vector3(width/2, height/2, width);

		mesh.vertices = vertices;


		int[] tri = new int[48];
		// Lower left triangle of front quad
		tri[0] = 0;
		tri[1] = 2;
		tri[2] = 1;
		// Upper right triangle of front quad
		tri[3] = 2;
		tri[4] = 3;
		tri[5] = 1;

		//Triangles of rear quad
		tri[6] = 4;
		tri[7] = 6;
		tri[8] = 5;
		tri[9] = 6;
		tri[10] = 7;
		tri[11] = 5;

		//triangles of top quad
		tri[12] = 3;
		tri[13] = 6;
		tri[14] = 7;
		tri[15] = 2;
		tri[16] = 6;
		tri[17] = 3;

		//triangles of bottom quad
		tri[18] = 1;
		tri[19] = 4;
		tri[20] = 5;
		tri[21] = 0;
		tri[22] = 4;
		tri[23] = 1;

		//triangles of left quad
		tri[24] = 0;
		tri[25] = 6;
		tri[26] = 2;
		tri[27] = 4;
		tri[28] = 6;
		tri[29] = 0;

		//triangles of right quad
		tri[30] = 5;
		tri[31] = 3;
		tri[32] = 7;
		tri[33] = 1;
		tri[34] = 3;
		tri[35] = 5;

		//front nose pyramid
		//top
		tri[36] = 6;
		tri[37] = 8;
		tri[38] = 7;
		
		//bottom
		tri[39] = 4;
		tri[40] = 8;
		tri[41] = 5;

		//left
		tri[42] = 6;
		tri[43] = 8;
		tri[44] = 4;

		//right
		tri[45] = 7;
		tri[46] = 8;
		tri[47] = 5;



		mesh.triangles = tri;

		//Color:
		Material material = new Material(Shader.Find("Standard"));
		material.color = Color.red;
		GetComponent<Renderer>().material = material;
	}
}
