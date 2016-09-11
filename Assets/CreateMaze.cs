using UnityEngine;
using System;
using System.Collections;
using System.Threading;

public class CreateMaze : MonoBehaviour {

	static int gridXMin = -20;
	static int gridZMin = -20;
	static int gridXMax = 21;
	static int gridZMax = 21;
	int xSize = -1 * gridXMin + gridXMax;
	int zSize = -1 * gridZMin + gridZMax;



	public int[,] maze { get; private set; }

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
		int zval = rand.Next(zSize);
		while (zval % 2 == 0)
		{
			zval = rand.Next(zSize);
		}



		//　Allocate the maze with recursive method
		allocate(xval, zval);

		return maze;
	}

	public void allocate(int x, int z)
	{
		//shuffled directions
		int[] directions = new int[]{1,2,3,4};
		Shuffle(directions);

		for (int i = 0; i < directions.Length; i++)
		{
			switch(directions[i]){
			//Up
			case 1:
				if (x - 2 <= 0)
					continue;
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
					continue;
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
					continue;
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
					continue;
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

	public void Shuffle<T>(T[] array)
	{
		System.Random rand = new System.Random ();
		int n = array.Length;
		while (n > 1) {
			n--;
			int k = rand.Next (n + 1);
			T value = array [k];
			array [k] = array [n];
			array [n] = value;
		}
		Thread.Sleep(1);
	}
		
	void Start(){
		if (Input.GetKeyDown (KeyCode.Space)) {
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
		if (x < 20 && x > 15) {
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
		if (x > 15 && x < 20) {
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
		mazeWall.GetComponent<Renderer> ().material.shader = Shader.Find (color);
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
