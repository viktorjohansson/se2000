using UnityEngine;
using System.Collections;

public class Float : MonoBehaviour {
	public float FloatStrenght;
	public float RandomRotationStrenght;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.GetComponent<Rigidbody>().AddForce(Vector3.up *FloatStrenght);
		transform.Rotate(RandomRotationStrenght,RandomRotationStrenght,RandomRotationStrenght);
	}
}

