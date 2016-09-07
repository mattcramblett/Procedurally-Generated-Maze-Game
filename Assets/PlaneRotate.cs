using UnityEngine;
using System.Collections;

public class PlaneRotate : MonoBehaviour {
	
	public float rate = 0.1f;
	private bool toggle = false;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha0)){
			toggle = !toggle;	
		}
		if(toggle){
			//transform.Rotate(0, Time.deltaTime * rate, 0);
			transform.Rotate(Vector3.up, rate);
		}		
	}
}
