using UnityEngine;
using System.Collections;

public class OnWaterPosition : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		if (Physics.Raycast (transform.position, -Vector3.up, out hit)) {
			float distanceToGround = hit.distance;
		}
		print (hit.distance);
	}
}
