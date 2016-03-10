using UnityEngine;
using System.Collections;

public class LandingController : MonoBehaviour {

	private Vector3 OriginalPos;
	private Quaternion OriginalRot;

	public Transform camTransform;
	public Transform onWaterFront;
	public Transform onWater;
	public Transform atmosphere;
	public Transform horizont;
	public Transform noRotation;

	public bool waterTravel;
	public bool Shaking;
	public bool setRotationTime;
	public bool cameraDone;
	public bool reload;
	public bool audioPlayed;
	public bool firstPointDone;
	
	private float ShakeDecay;
	private float ShakeIntensity;
	public float highSpeedStep;
	public float lowSpeedStep;

	public int language;

	public GameObject audioObject;
	public GameObject backgroundObject;
	public GameObject program2Object;
	public GameObject playerObject;
	
	// Use this for initialization
	void Start () {
		language = 0;
		Camera.main.stereoSeparation = 0.1F;
		Camera.main.stereoConvergence = 10F;
		reload = false;

		audioObject = GameObject.Find ("audio");
		backgroundObject = GameObject.Find ("background");
		program2Object = GameObject.Find ("Program2");
		playerObject = GameObject.Find ("player");

		audioObject.GetComponent<AudioController>().playSound (2); //2 = landing, kolla AudioController
	}
	
	// Update is called once per frame
	void Update () {

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


		highSpeedStep = 30 * Time.deltaTime;
		lowSpeedStep = 3 * Time.deltaTime;
    
		if (transform.position.y > (onWater.position.y + 60)) {
			transform.position = Vector3.MoveTowards (transform.position, onWater.position, highSpeedStep);
			DoShake (highSpeedStep);
		}
    
		if (transform.position.y > onWaterFront.position.y && transform.position.y < onWater.position.y + 65)  {
			camTransform.localRotation = Quaternion.RotateTowards(camTransform.localRotation, noRotation.rotation, Time.deltaTime);
			camTransform.localPosition = Vector3.MoveTowards(camTransform.localPosition, noRotation.position, Time.deltaTime);
			ParaCamera();
			transform.position = Vector3.MoveTowards (transform.position, onWaterFront.position, lowSpeedStep);
		}

		if (transform.position.y < onWater.position.y + 50 && transform.position.y > onWater.position.y + 45) {
			backgroundObject.GetComponent<BackgroundController>().startParachute(language); //Fixa på rätt ställe
		}

		if (transform.position.y < onWater.position.y + 1 && transform.position.y > onWater.position.y + 0.5F) {
			if (reload == false) {
				audioObject.GetComponent<AudioController>().playSound (7); 
				backgroundObject.GetComponent<BackgroundController>().hasLanded(language);
				StartCoroutine(LoadScene());
				reload = true;
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

	void ParaCamera() {
    
		if (setRotationTime == false) {
			setRotationTime = true;
		}
    	
		if (!firstPointDone) {
			var firstTargetRotation = Quaternion.LookRotation (atmosphere.transform.position - transform.localPosition);
			transform.localRotation = Quaternion.RotateTowards (transform.rotation, firstTargetRotation, Time.deltaTime * 300);
			if (firstTargetRotation == transform.localRotation) {
				firstPointDone = true;
			}
		} else {
			var secondTargetRotation = Quaternion.LookRotation(horizont.transform.position - transform.localPosition);
			transform.localRotation = Quaternion.RotateTowards (transform.rotation, secondTargetRotation, Time.deltaTime * 10);
			if (secondTargetRotation == transform.localRotation) {
				setRotationTime = false;
				cameraDone = true;
			}
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
	

	IEnumerator LoadScene() {
		yield return new WaitForSeconds (5F);
		audioObject.GetComponent<AudioController>().playSound (8);
		yield return new WaitForSeconds (10F);
		yield return new WaitForSeconds (5F);
		playerObject.GetComponent<Fading>().BeginFade(1);
		yield return new WaitForSeconds (1F);
		Destroy(program2Object);
		Destroy(audioObject);
		Application.LoadLevel ("earthScene");
	}
	
}