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
		int x2down = x - 2;
		int x1down = x - 1;
		int z2down = z - 2;
		int z1down = z - 1;

		int x2up = x + 2;
		int x1up = x + 1;
		int z2up = z + 2;
		int z1up = z + 1;
		int i;
		for (i = 0; i < directions.Length; i++)
		{
			//faster than original if/elseif
			switch(directions[i]){
			//Up
			case 1:
				if (x2down <= 0)
					break;
				if (maze[x2down,z] != 0)
				{
					maze[x2down,z] = 0;
					putWall (x2down, z);
					maze[x1down,z] = 0;
					putWall (x1down, z);
					allocate(x2down, z);
				}
				break;
			//Down
			case 2: 
				if (x2up >= xSize - 1)
					break;
				if (maze[x2up,z] != 0)
				{
					maze[x2up,z] = 0;
					putWall (x2up, z);
					maze[x1up,z] = 0;
					putWall (x1up, z);
					allocate(x2up, z);
				}
				break;
			// Right
			case 3: 
				if (z2up >= zSize - 1)
					break;
				if (maze[x,z2up] != 0)
				{
					maze[x,z2up] = 0;
					putWall (x, z2up);
					maze[x,z1up] = 0;
					putWall (x, z1up);
					allocate(x, z2up);
				}
				break;
			//Left
			case 4:
				if (z2down <= 0)
					break;
				if (maze[x,z2down] != 0)
				{
					maze[x,z2down] = 0;
					putWall (x, z2down);
					maze[x,z1down] = 0;
					putWall (x, z1down);
					allocate(x, z2down);
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
