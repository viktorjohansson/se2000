using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Vector3 OriginalPos;
	private Quaternion OriginalRot;

	public Transform onGround;
	public Transform atmosphere;
	public Transform camTransform;
	public Transform windExplosion;

	public float speed;
	public float rotateSpeed;
	private float startTime;

	public bool atmoSphereTravel;
	public bool Shaking;
	public bool audioPlayed;
	public bool startShake;
	public bool startDone;

	private float ShakeDecay;
	private float ShakeIntensity;

	public int language;
  
	public GameObject standby;
	public GameObject journeyAudios;
	public GameObject backgroundSound;
	public GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("player");
		standby = GameObject.Find ("standby");
		journeyAudios = GameObject.Find ("audio");
		backgroundSound = GameObject.Find ("background");
		Shaking = false;  
		atmoSphereTravel = false;
		startShake = false;
		startDone = false;
		speed = 0.5F;
		rotateSpeed = 30F;
		Screen.SetResolution (5760, 2160, false);
		Camera.main.stereoSeparation = 0.1F;
		Camera.main.stereoConvergence = 3F;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.RightArrow) && camTransform.eulerAngles.y < 30 || Input.GetKey (KeyCode.RightArrow) && camTransform.eulerAngles.y > 320) {
			camTransform.Rotate (Vector3.up * rotateSpeed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.LeftArrow) && camTransform.eulerAngles.y > 330 || Input.GetKey (KeyCode.LeftArrow) && camTransform.eulerAngles.y < 40) {
			camTransform.Rotate (-Vector3.up * rotateSpeed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.UpArrow) && camTransform.eulerAngles.x > 330 || Input.GetKey (KeyCode.UpArrow) && camTransform.eulerAngles.x < 40) {
			camTransform.Rotate (Vector3.left * rotateSpeed * Time.deltaTime);
		}

		if (!Input.anyKey) {
			camTransform.rotation = Quaternion.RotateTowards(camTransform.rotation,transform.rotation, Time.deltaTime * 20);
		}

		float distCovered = Vector3.Distance (transform.position, onGround.position);
		float travelSpeed = (distCovered + 1) * Time.deltaTime;

		if (Input.GetKeyDown ("x") && startDone == false) {
			standby.GetComponent<AudioSource>().Stop();
			journeyAudios.GetComponent<AudioController>().playSound (0); //0 = takeOff, kolla AudioController
			startDone = true;
			StartCoroutine(WaitForTakeOff());
			backgroundSound.GetComponent<BackgroundController>().start();
		} 

		if (Input.GetKeyDown ("b")) {
			Application.LoadLevel ("earthScene");
		}
		
		if (Input.GetKeyDown ("n")) {
			Application.LoadLevel ("startScene");
		}
		
		if (Input.GetKeyDown ("m")) {
			Application.LoadLevel ("landingScene");
		}

		if (Input.GetKeyDown ("c")) {
			language = 1;
		}

		if (Input.GetKeyDown ("v")) {
			language = 0;
		}



		if (startShake) {
			DoShake(5 * Time.deltaTime);
		}

		if (atmoSphereTravel) {
			DoShake(travelSpeed * 2 + 5);
			transform.position = Vector3.MoveTowards (transform.position, atmosphere.position, travelSpeed * speed);
      
			if (atmosphere.position.y - transform.position.y < 1800) {
				transform.Rotate (Vector3.left * 17F * Time.deltaTime);
			}

			if (atmosphere.position.y - transform.position.y < 50) {
				float fadeTime = player.GetComponent<Fading>().BeginFade(1);
				StartCoroutine(LoadScene(fadeTime));
			}
		}
	

		if(ShakeIntensity > 0) {
			camTransform.localPosition = OriginalPos + Random.insideUnitSphere * ShakeIntensity;
			camTransform.localRotation = new Quaternion(OriginalRot.x + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f,
			                                    OriginalRot.y + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f,
			                                    OriginalRot.z + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f,
			                                    OriginalRot.w + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f);
			
			ShakeIntensity -= ShakeDecay;
   		 } else if (Shaking) {
			Shaking = false;    
		}
	}

	public void DoShake(float speed) {
		OriginalPos = camTransform.localPosition;
		OriginalRot = camTransform.localRotation;

		if (0.015f * speed < 0.02f) {
			ShakeIntensity = 0.015f * speed;
		} else {
			ShakeIntensity = 0.02f;
		}
		ShakeDecay = 0.1f;
		Shaking = true;
	}

	IEnumerator LoadScene(float waitTime) {
		yield return new WaitForSeconds (waitTime);
		Application.LoadLevel ("startScene");
	}

	IEnumerator WaitForTakeOff() {
		yield return new WaitForSeconds (12);
		windExplosion.position = camTransform.position;
		yield return new WaitForSeconds (2);
		startShake = true;
		yield return new WaitForSeconds (1.7F);
		startShake = false;
		atmoSphereTravel = true;
	}
}
