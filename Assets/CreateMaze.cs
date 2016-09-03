using UnityEngine;
using System.Collections;

public class CreateMaze : MonoBehaviour {
	void Start(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			print("here");
		}
	}

	void RestartGame(){
		print ("here");
		for (int j = 0; j < 20; j++) {
			GameObject mazeWall = GameObject.CreatePrimitive (PrimitiveType.Cube);
			print (mazeWall);
			mazeWall.transform.parent = transform;
			mazeWall.name = "wall" + j;
			mazeWall.transform.position = new Vector3 (Random.Range (-20.0f, 20.0f), 0.5f, Random.Range (-20.0f, 20.0f));
			mazeWall.transform.localScale = new Vector3 (Random.Range (0.0f, 1.0f), 1.0f, Random.Range (0.0f, 1.0f));
		}
	}

	void Destroy(){
		/*	var objects = GameObject.FindObjectOfType (mazeWall);
		foreach(GameObject o in objects){
			Destroy(o);
		}*/
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			Destroy ();
			RestartGame ();
		}

	}
}
