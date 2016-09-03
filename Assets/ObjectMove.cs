using UnityEngine;
using System.Collections;

public class ObjectMove : MonoBehaviour {

public float rate = 3f;

	void OnTriggerEnter(Collider other)
	{
		print("Collision detected with trigger object " + other.name);
	}

	void OnTriggerStay(Collider other)
	{
		print("Still colliding with trigger object " + other.name);
	}

	void OnTriggerExit(Collider other)
	{
		print(gameObject.name + " and trigger object " + other.name + " are no longer colliding");
	}

	// Update is called once per frame
    void Update() {

        if (Input.GetKey(KeyCode.A)){
            transform.position += Vector3.left * rate * Time.deltaTime;
        }
        
        if (Input.GetKey(KeyCode.D)){
            transform.position += Vector3.right * rate * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.W)){
        	transform.position += Vector3.forward * rate * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S)){
			
        	transform.position -= Vector3.forward * rate * Time.deltaTime;
        }
    }
		
}