using UnityEngine;
using System;
using System.Collections;
using System.Threading;

public class CreateMaze : MonoBehaviour {

	static int gridXMin = -20;
	static int gridZMin = -20;
	static int gridXMax = 20;
	static int gridZMax = 20;
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
		maze[xval,zval] = 0;


		//　Allocate the maze with recursive method
		allocate(xval, zval);

		return maze;
	}

	public void allocate(int x, int z)
	{
		// 4 random directions
		int[] directions = new int[]{1,2,3,4};

		//directions = generateRandomDirections();
		Shuffle(directions);

		// Examine each direction
		for (int i = 0; i < directions.Length; i++)
		{

			switch(directions[i]){
			case 1: // Up
				//　Whether 2 cells up is out or not
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
			case 2: // Right
				// Whether 2 cells to the right is out or not
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
			case 3: // Down
				// Whether 2 cells down is out or not
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
			case 4: // Left
				// Whether 2 cells to the left is out or not
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

	// Fisher Yates Shuffle
	public void Shuffle<T>(T[] array)
	{
		System.Random _random = new System.Random();
		for (int i = array.Length; i > 1; i--)
		{
			// Pick random element to swap.
			int j = _random.Next(i); // 0 <= j <= i-1
			// Swap.
			T tmp = array[j];
			array[j] = array[i - 1];
			array[i - 1] = tmp;
		}
		Thread.Sleep(1);
	}





	void Start(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			print("here");
		}
	}


	void putWall(int x, int z){
		GameObject mazeWall = GameObject.CreatePrimitive (PrimitiveType.Cube);
		mazeWall.transform.parent = transform;
		mazeWall.name = "wall" + x + "." + z;
		mazeWall.GetComponent<Renderer>().material.shader = Shader.Find("cubeColor");
		mazeWall.transform.position = new Vector3 ((float)x + gridXMin, 0.5f, (float)z + gridZMin);
		mazeWall.transform.localScale = new Vector3 (.25f, 1f, .25f);
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
			//Destroy ();
			int[,] maze = GenerateMaze (xSize, zSize);

			//RestartGame (maze);
		}
	}
}
