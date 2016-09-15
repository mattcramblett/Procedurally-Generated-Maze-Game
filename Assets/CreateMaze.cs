using UnityEngine;
using System;
using System.Collections;
using System.Threading;

public class CreateMaze : MonoBehaviour {

	//grid dimensions
	static int gridXMin = -20;
	static int gridZMin = -20;
	static int gridXMax = 21;
	static int gridZMax = 21;
	int xSize = -1 * gridXMin + gridXMax;
	int zSize = -1 * gridZMin + gridZMax;



	public int[,] maze;

	public int[,] GenerateMaze(int xSize,int zSize)
	{
		maze = new int[xSize,zSize];
		for (int x = 0; x < xSize; x++) {
			for (int z = 0; z < zSize; z++) {
				maze[x,z] = 1;
			}
		}

		System.Random rand = new System.Random();
		int xval = rand.Next(xSize);
		while (xval % 2 == 0)
		{
			xval = rand.Next(xSize);
		}
		int zval = rand.Next(xSize);
		while (zval % 2 == 0)
		{
			zval = rand.Next(xSize);
		}
			
		//maze
		allocate(xval, zval);

		return maze;
	}

	public void allocate(int x, int z)
	{
		//too fast without sleep? creates a bunch of long passages
		Thread.Sleep(1);
		//up, down, left, right
		int[] directions = new int[]{1,2,3,4};
		//mix for generation
		Shuffle(directions);
		int i;
		for (i = 0; i < directions.Length; i++)
		{
			//faster than original if/elseif
			switch(directions[i]){
			//Up
			case 1:
				if (x - 2 <= 0)
					break;
				if (maze[x - 2,z] != 0)
				{
					maze[x-2,z] = 0;
					putWall (x - 2, z);
					maze[x-1,z] = 0;
					putWall (x - 1, z);
					allocate(x - 2, z);
				}
				break;
			//Down
			case 2: 
				if (x + 2 >= xSize - 1)
					break;
				if (maze[x + 2,z] != 0)
				{
					maze[x + 2,z] = 0;
					putWall (x + 2, z);
					maze[x + 1,z] = 0;
					putWall (x + 1, z);
					allocate(x + 2, z);
				}
				break;
			// Right
			case 3: 
				if (z + 2 >= zSize - 1)
					break;
				if (maze[x,z + 2] != 0)
				{
					maze[x,z + 2] = 0;
					putWall (x, z + 2);
					maze[x,z + 1] = 0;
					putWall (x, z + 1);
					allocate(x, z + 2);
				}
				break;
			//Left
			case 4:
				if (z - 2 <= 0)
					break;
				if (maze[x,z - 2] != 0)
				{
					maze[x,z - 2] = 0;
					putWall (x, z - 2);
					maze[x,z - 1] = 0;
					putWall (x, z - 1);
					allocate(x, z - 2);
				}
				break;
			}
		}

	}

	//shuffle to choose random direction
	public void Shuffle(int[] array)
	{
		System.Random rand = new System.Random ();
		int n = 0;
		while (n < array.Length ) {
			int k = rand.Next (n + 1);
			int temp = array [k];
			array [k] = array [n];
			array [n] = temp;
			n++;
		}
	}
		
	bool answer(int x, int z){
		if (x == 20) {
			if (z == 2 || z == 1) {
				return true;
			}
			if (z <= 5) {
				return true;
			}
			if (z >= 21) {
				return true;
			}
		}
		if (x <= 20 && x >= 15) {
			if (z == 6) {
				return true;
			}
		}
		if (x == 14) {
			if (z <= 12 && z >= 6) {
				return true;
			}
		}
		if (x == 15) {
			if (z <= 20 && z >= 12) {
				return true;
			}
		}
		if (x > 15 && x <= 20) {
			if (z == 20) {
				return true;
			}
		}
		return false;
	}

	void wall(int x, int z, String color){
		GameObject mazeWall = GameObject.CreatePrimitive (PrimitiveType.Cube);
		mazeWall.transform.parent = transform;
		mazeWall.name = "wall" + x + "." + z;

		//Color:
		Material material = new Material(Shader.Find("Standard"));
		material.color = Color.black;
		mazeWall.GetComponent<Renderer>().material = material;

		mazeWall.transform.position = new Vector3 ((float)x + gridXMin, 0.5f, (float)z + gridZMin);
		mazeWall.transform.localScale = new Vector3 (.25f, 1f, .25f);
	}


	void putWall(int x, int z){
		if (answer(x,z)) {
		} else{
			wall (x, z, "cubeColor");
		}
	}


	void Destroy(){
		/*	GameObject[] objects = GameObject.FindObjectsOfType ();
		foreach(GameObject o in objects){
			Destroy(o);
		}*/
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			Destroy ();
			int[,] maze = GenerateMaze (xSize, zSize);

			//RestartGame (maze);
		}
	}
}
