using UnityEngine;
using System.Collections;

public class AstronautController : MonoBehaviour {

	public Transform astronaut;
	public Transform astronautPositionOne;
	public Transform astronautPositionTwo;
	public Transform astronautPositionThree;
	public Transform wrenchArm;
	public Transform wrenchFollow;
	public Transform wrenchPositionOne;
	public Transform wrenchPositionTwo;

	public float speed;


	public bool astronautPositionOneTravel;
	public bool astronautPositionTwoTravel;
	public bool astronautRotate;
	public bool astronautAtPositionOne;
	public bool astronautAtPositionTwo;

	public bool wrenchRotateOne;
	public bool wrenchRotateTwo;
	public bool startTravel;
	public bool firstTime;
	


	// Use this for initialization
	void Start () {
		speed = 0.1F;

	}
	
	// Update is called once per frame
	void Update () {

		if (astronautPositionTwoTravel) {
			astronaut.position = Vector3.MoveTowards (astronaut.position, astronautPositionTwo.position, 0.25f * Time.deltaTime);
			astronaut.rotation = Quaternion.RotateTowards (astronaut.rotation, astronautPositionTwo.rotation, 25 * Time.deltaTime);
		}

		if (astronautPositionOneTravel) {
			if (startTravel == false) {
			astronaut.rotation = Quaternion.RotateTowards (astronaut.rotation, astronautPositionThree.rotation, 20 * Time.deltaTime);
				StartCoroutine(Delay(7));
			}
			else {
				astronaut.position = Vector3.MoveTowards (astronaut.position, astronautPositionOne.position, 0.25f * Time.deltaTime);
				astronaut.rotation = Quaternion.RotateTowards (astronaut.rotation, astronautPositionOne.rotation, 20 * Time.deltaTime);
			}



		}

	

		if (astronaut.position == astronautPositionOne.position || astronautAtPositionOne) {
			astronautPositionOneTravel = false;
			astronautAtPositionOne = true;
			startTravel = false;

			StartCoroutine(WaitTravelTwo(9));
			
			if (wrenchArm.eulerAngles.z > 8) {
				wrenchRotateTwo = true;
				wrenchRotateOne = false;
			}

			if (wrenchArm.eulerAngles.z < 3) {
				wrenchRotateTwo = false;
				wrenchRotateOne = true;
			}

			if (wrenchRotateOne) {
				wrenchArm.Rotate (Vector3.forward * Time.deltaTime * 5);
			}

		
			if (wrenchRotateTwo) {
				wrenchArm.Rotate (Vector3.back * Time.deltaTime * 5);
			}


		}

		if (astronaut.position == astronautPositionTwo.position || astronautAtPositionTwo) {
			astronautPositionTwoTravel = false;
			astronautAtPositionTwo = true;
			startTravel = false;

			StartCoroutine(WaitTravelOne(9));

			if (wrenchArm.eulerAngles.z > 179) {
				wrenchRotateTwo = true;
				wrenchRotateOne = false;
			}

			if (wrenchArm.eulerAngles.z < 185) {
				wrenchRotateTwo = false;
				wrenchRotateOne = true;
			}
			
			if (wrenchRotateOne) {
				wrenchArm.Rotate (Vector3.forward * Time.deltaTime * 5);
			}
			
			
			if (wrenchRotateTwo) {
				wrenchArm.Rotate (Vector3.back * Time.deltaTime * 5);
			}
			
			
		}

	}

	IEnumerator WaitTravelTwo(float waitTime) {
		yield return new WaitForSeconds (waitTime);
		astronautPositionTwoTravel = true;
		astronautAtPositionOne = false;
	}

	IEnumerator WaitTravelOne(float waitTime) {
		yield return new WaitForSeconds (waitTime);
		astronautPositionOneTravel = true;
		astronautAtPositionTwo = false;
	}

	IEnumerator Delay(float waitTime) {
		yield return new WaitForSeconds (waitTime);
		startTravel = true;

	}
}
