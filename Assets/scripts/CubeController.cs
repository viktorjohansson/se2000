using UnityEngine;
using System.Collections;

public class CubeController : MonoBehaviour {
	public Transform outerSpace;
	public Transform onGround;
	public Transform spaceStation;
	public Transform camTransform;
	public Transform noRotation;

	public Transform mercury;
	public Transform venus;
	public Transform earth;
	public Transform mars;
	public Transform jupiter;
	public Transform saturn;
	public Transform uranus;
	public Transform neptune;
	public Transform asteroid;
	public Transform planetAt;

	public Transform venusOverviewFirst;
	public Transform earthOverviewFirst;
	public Transform marsOverviewFirst;
	public Transform jupiterOverviewFirst;
	public Transform saturnOverviewFirst;
	public Transform uranusOverviewFirst;
	public Transform neptuneOverviewFirst;
	public Transform mercuryOverviewFirst;
	public Transform asteroidOverviewFirst;

	public Transform venusOverviewSecond;
	public Transform earthOverviewSecond;
	public Transform marsOverviewSecond;
	public Transform jupiterOverviewSecond;
	public Transform saturnOverviewSecond;
	public Transform uranusOverviewSecond;
	public Transform neptuneOverviewSecond;
	public Transform mercuryOverviewSecond;

	public Transform returnPartOne;
	public Transform returnPartTwo;
	public Transform returnPartThree;
	public Transform returnPartFour;
	public Transform startLook;
	public Transform followObject;

	public float speed = 0.3F;
	public float startTime;
	public float lastActiveAt;
	public float timeBeforeInactive = 300.0F;
	public float journeyLengthPlanet;
	public float speedAtBrake;
	public float startTimeBrake;
	public float endTimeJourney;
	public float travelSpeed;
	public float topSpeed;
	public float step;
	public float distanceLeft;
	private float ShakeDecay;
	private float ShakeIntensity;
	public float smoothTime;
	public float rotateSpeed;

	public bool testing;
	public bool rotateCamera;
	public bool ride;
	public bool land;
	public bool backTravel;
	public bool mercuryTravel;
	public bool venusTravel;
	public bool earthTravel;
	public bool marsTravel;
	public bool jupiterTravel;
	public bool saturnTravel;
	public bool uranusTravel;
	public bool neptuneTravel;
	public bool asteroidTravel;
	public bool outerSpaceTravel;
	public bool returnPartOneTravel;
	public bool returnPartTwoTravel;
	public bool returnPartThreeTravel;
	public bool cameraDone;
	public bool travelDone;
	public bool setTime;
	public bool countDone;
	public bool setRotationTime;
	public bool rotationDone;
	public bool Shaking;
	public bool audioPlayed;
	public bool buttonPress;
	public bool astronautPlayed;
	
	public Vector3 planetPosition;
	private Vector3 OriginalPos;
	private Vector3 velocity = Vector3.zero;
	private Quaternion OriginalRot;
	
	public Texture travelToMercury;
	public Texture travelToVenus;
	public Texture travelToEarth;
	public Texture travelToMars;
	public Texture travelToJupiter;
	public Texture travelToSaturn;
	public Texture travelToUranus;
	public Texture travelToNeptune;
	public Texture travelToISS;
	public Texture travelToAsteroids;

	public Texture travelToMercuryEng;
	public Texture travelToVenusEng;
	public Texture travelToEarthEng;
	public Texture travelToMarsEng;
	public Texture travelToJupiterEng;
	public Texture travelToSaturnEng;
	public Texture travelToUranusEng;
	public Texture travelToNeptuneEng;
	public Texture travelToISSEng;
	public Texture travelToAsteroidsEng;

	public Texture mercuryTexture;
	public Texture venusTexture;
	public Texture earthTexture;
	public Texture marsTexture;
	public Texture jupiterTexture;
	public Texture saturnTexture;
	public Texture uranusTexture;
	public Texture neptuneTexture;
	public Texture ISSTexture;
	public Texture asteroidTexture;

	public Texture mercuryTextureEng;
	public Texture venusTextureEng;
	public Texture earthTextureEng;
	public Texture marsTextureEng;
	public Texture jupiterTextureEng;
	public Texture saturnTextureEng;
	public Texture uranusTextureEng;
	public Texture neptuneTextureEng;
	public Texture ISSTextureEng;
	public Texture asteroidTextureEng;

	public Texture mercurySphere;
	public Texture venusSphere;
	public Texture earthSphere;
	public Texture marsSphere;
	public Texture jupiterSphere;
	public Texture saturnSphere;
	public Texture uranusSphere;
	public Texture neptuneSphere;
	public Texture ISSphere;

	public GameObject audioObject;
	public GameObject backgroundObject;
	public GameObject cubeObject;

	void Start() {
		Camera.main.stereoSeparation = 0.1F;
		Camera.main.stereoConvergence = 7F;
		journeyLengthPlanet = 0;
		topSpeed = 4.0F;
		speed = 2;
		smoothTime = 4F;
		travelSpeed = 0F;
		rotateSpeed = 30F;
		lastActiveAt = Time.time;

		audioObject = GameObject.Find ("audio");
		backgroundObject = GameObject.Find ("background");
		cubeObject = GameObject.Find ("Cube");

		ride = true;
		land = false;
		testing = false;
		backTravel = false;
		mercuryTravel = false;
		venusTravel = false;
		earthTravel = false;
		marsTravel = false;
		jupiterTravel = false;
		saturnTravel = false;
		uranusTravel = false;
		neptuneTravel = false;
		asteroidTravel = false;
		outerSpaceTravel = false;
		returnPartOneTravel = false;
		returnPartTwoTravel = false;
		returnPartThreeTravel = false;
		cameraDone = false;
		travelDone = false;
		setTime = false;
		setRotationTime = false;
		audioPlayed = true;
		buttonPress = true;	
		astronautPlayed = false;
		planetAt = spaceStation;

		transform.LookAt(startLook);

	}

	// Update is called once per frame
	void Update () {
		step = (Time.deltaTime * topSpeed) * speed;
		bool landing = Input.GetKeyDown (KeyCode.Q);
		bool gameIsInactive = lastActiveAt < Time.time - timeBeforeInactive;

		if (Input.anyKey) {
			lastActiveAt = Time.time;
		}

		if (gameIsInactive) {
			land = true;
		}

		if (ride) {
			if (cameraDone == true) {
				transform.LookAt (returnPartOne); 
			}
			PointCamera (returnPartOne);
			DoShake (topSpeed * speed * 2);
			transform.position = Vector3.MoveTowards (transform.position, returnPartOne.position, (topSpeed * speed * 2) * Time.deltaTime);
			if (transform.position == returnPartOne.position) {
				ride = false;
				backTravel = true;
				cameraDone = false;
			}
		}
	
		if (backTravel) {
			if (cameraDone == true) {
				transform.LookAt (spaceStation); 
			}
			PointCamera (spaceStation);
			DoShake (speed / 10);
			//GameObject.Find ("audio").GetComponent<AudioController>().fadeOut ();
			transform.position = Vector3.SmoothDamp (transform.position, followObject.position, ref velocity, smoothTime);
			followObject.position = Vector3.MoveTowards (followObject.position, returnPartThree.position, (topSpeed * (speed / 2)) * Time.deltaTime);
			if (followObject.position == returnPartThree.position) {
				returnPartOneTravel = true;
				backTravel = false;
				cameraDone = false;
				//GameObject.Find ("audio").GetComponent<AudioController>().stopSound ();
			}
		}
    
		if (returnPartOneTravel) {
			if (cameraDone == true) {
				transform.LookAt (spaceStation); 
			}
			PointCamera (spaceStation);
			DoShake (speed / 10);
			transform.position = Vector3.SmoothDamp (transform.position, followObject.position, ref velocity, smoothTime);
			followObject.position = Vector3.MoveTowards (followObject.position, returnPartFour.position, (topSpeed * (speed / 2)) * Time.deltaTime);
			if (followObject.position == returnPartFour.position) {
				returnPartOneTravel = false;
				returnPartTwoTravel = true;
				cameraDone = false;
			}
		}
    
		if (returnPartTwoTravel) {
			if (cameraDone == true) {
				transform.LookAt (mars); 
			}
			PointCamera (mars);
			transform.position = Vector3.SmoothDamp (transform.position, followObject.position, ref velocity, smoothTime);
			followObject.position = Vector3.MoveTowards (followObject.position, outerSpace.position, (topSpeed * (speed / 2)) * Time.deltaTime);
			if (followObject.position == outerSpace.position) {
				returnPartTwoTravel = false;
				returnPartThreeTravel = true;
				cameraDone = false;
			}
		}

		if (returnPartThreeTravel) {
			if (cameraDone == true) {
				transform.LookAt (mars); 
			}
			var distanceLeft = Vector3.Distance (transform.position, outerSpace.position);
			PointCamera (mars);
			transform.position = Vector3.SmoothDamp (transform.position, followObject.position, ref velocity, smoothTime);
			if (distanceLeft < 2.5F) {
				returnPartThreeTravel = false;
				cameraDone = false;
				audioPlayed = false;
				buttonPress = false;
				backgroundObject.GetComponent<BackgroundController>().outerSpace();
			}
		}

		if (landing && buttonPress == false) {
			land = true;
			buttonPress = true;
			backgroundObject.GetComponent<BackgroundController>().startLanding();
		}
    
		if (land) {
			if (cameraDone == false) {
				PointCamera (earth);
			} else {
				distanceLeft = Vector3.Distance (transform.position, earth.position);
				transform.position = Vector3.MoveTowards (transform.position, earth.position, 25 * Time.deltaTime); 
				DoShake(25 * Time.deltaTime);
				if (distanceLeft < 15) {
					float fadeTime = cubeObject.GetComponent<Fading> ().BeginFade (1);

					StartCoroutine (LoadScene (fadeTime));
				}
			}
			
		}	

		if (Input.GetKey (KeyCode.RightArrow)) {
			camTransform.Rotate (Vector3.up * rotateSpeed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			camTransform.Rotate (-Vector3.up * rotateSpeed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			camTransform.Rotate (Vector3.left * rotateSpeed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			camTransform.Rotate (-Vector3.left * rotateSpeed * Time.deltaTime);
		}
    
		if (!Input.anyKey) {
			camTransform.rotation = Quaternion.RotateTowards(camTransform.rotation,transform.rotation, Time.deltaTime * 10);
		}

		if (((Input.GetKeyDown ("p") && buttonPress == false) || outerSpaceTravel == true) && planetAt != spaceStation) {
			outerSpaceTravel = true;
			PlanetTravel (spaceStation, outerSpace.position, outerSpace.position, travelToISS, travelToISSEng, ISSTexture, ISSTextureEng, ISSphere);
		}

		if (((Input.GetKeyDown ("w") && buttonPress == false) || mercuryTravel == true) && planetAt != mercury) {
			mercuryTravel = true;
			PlanetTravel (mercury, mercuryOverviewFirst.position, mercuryOverviewSecond.position, travelToMercury, travelToMercuryEng, mercuryTexture, mercuryTextureEng, mercurySphere);
		}

		if (((Input.GetKeyDown ("e") && buttonPress == false) || venusTravel == true) && planetAt != venus) {
			venusTravel = true;
			PlanetTravel (venus, venusOverviewFirst.position, venusOverviewSecond.position, travelToVenus, travelToVenusEng, venusTexture, venusTextureEng, venusSphere);
		}

		if (((Input.GetKeyDown ("r") && buttonPress == false) || earthTravel == true) && planetAt != earth) {
			earthTravel = true;
			PlanetTravel (earth, earthOverviewFirst.position, earthOverviewSecond.position, travelToEarth, travelToEarthEng, earthTexture, earthTextureEng, earthSphere);
		}

		if (((Input.GetKeyDown ("t") && buttonPress == false) || marsTravel == true) && planetAt != mars) {
			marsTravel = true;
			PlanetTravel (mars, marsOverviewFirst.position, marsOverviewSecond.position, travelToMars, travelToMarsEng, marsTexture, marsTextureEng, marsSphere);
		}

		if (((Input.GetKeyDown ("y") && buttonPress == false) || jupiterTravel == true) && planetAt != jupiter) {
			jupiterTravel = true;
			PlanetTravel (jupiter, jupiterOverviewFirst.position, jupiterOverviewSecond.position, travelToJupiter, travelToJupiterEng, jupiterTexture, jupiterTextureEng, jupiterSphere);
		}

		if (((Input.GetKeyDown ("u") && buttonPress == false) || saturnTravel == true) && planetAt != saturn) {
			saturnTravel = true;
			PlanetTravel (saturn, saturnOverviewFirst.position, saturnOverviewSecond.position, travelToSaturn, travelToSaturnEng, saturnTexture, saturnTextureEng, saturnSphere);
		}

		if (((Input.GetKeyDown ("i") && buttonPress == false) || uranusTravel == true) && planetAt != uranus) {
			uranusTravel = true;
			PlanetTravel (uranus, uranusOverviewFirst.position, uranusOverviewSecond.position, travelToUranus, travelToUranusEng, uranusTexture, uranusTextureEng, uranusSphere);
		}

		if (((Input.GetKeyDown ("o") && buttonPress == false) || neptuneTravel == true) && planetAt != neptune) {
			neptuneTravel = true;
			PlanetTravel (neptune, neptuneOverviewFirst.position, neptuneOverviewSecond.position, travelToNeptune, travelToNeptuneEng, neptuneTexture, neptuneTextureEng, neptuneSphere);
		}

		if (((Input.GetKeyDown ("a") && buttonPress == false) || asteroidTravel == true) && planetAt != asteroid) {
			asteroidTravel = true;
			PlanetTravel (asteroid, asteroidOverviewFirst.position, asteroidOverviewFirst.position, travelToAsteroids, travelToAsteroidsEng, asteroidTexture, asteroidTextureEng, asteroidTexture);
		}

		if (Input.GetKeyDown ("b")) {
			Application.LoadLevel ("eartScene");
		}

		if (Input.GetKeyDown ("n")) {
			Application.LoadLevel ("startScene");
		}

		if (Input.GetKeyDown ("m")) {
			Application.LoadLevel ("landingScene");
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

	void PointCamera(Transform direction) {
		if (setRotationTime == false) {
			startTime = Time.time;
			setRotationTime = true;
		}
		camTransform.localRotation = Quaternion.RotateTowards (camTransform.localRotation, noRotation.rotation, Time.deltaTime * 20);
		var targetRotation = Quaternion.LookRotation(direction.transform.position - transform.position);
		transform.rotation = Quaternion.RotateTowards(transform.rotation,targetRotation, Time.deltaTime * 20);
		if (targetRotation == transform.rotation) {
			startTime = Time.time;
			setRotationTime = false;
			cameraDone = true;
			if (audioPlayed == false) {
				audioObject.GetComponent<AudioController> ().stopSound ();
				audioObject.GetComponent<AudioController>().playSound (3); //3 = acceleration, kolla AudioController
				audioPlayed = true;
			}
		}
	}

	void PlanetTravel(Transform planet, Vector3 planetOverviewFirst, Vector3 planetOverviewSecond, Texture travelTexture, Texture travelTextureEng, Texture planetTexture, Texture planetTextureEng, Texture planetSphere) {
		backgroundObject.GetComponent<BackgroundController> ().travel (travelTexture, travelTextureEng, true);
		buttonPress = true;
    
		if (cameraDone == true) {
			transform.LookAt (planet);
		}

		if (setTime == false) {
			if (Vector3.Distance (transform.position, planetOverviewFirst) > Vector3.Distance (transform.position, planetOverviewSecond)) {
				planetPosition = planetOverviewSecond;
			} else {
				planetPosition = planetOverviewFirst;
			}
			startTime = Time.time;
			setTime = true;
		}
    
		journeyLengthPlanet = Vector3.Distance (transform.position, planetPosition);

		if (cameraDone == false && journeyLengthPlanet > 0.01F) {
			PointCamera (planet);
		} else if (journeyLengthPlanet > 0.01F) {
      
			if (journeyLengthPlanet > 17.62F) {
				if (((Time.time - startTime) * 4) < 6.0F) {
					travelSpeed = (Time.time - startTime) * 4;
					DoShake (travelSpeed);
				} else {
					travelSpeed = 6.0F;
					DoShake (travelSpeed / 10);
				}
				transform.position = Vector3.MoveTowards (transform.position, planetPosition, (travelSpeed * speed) * Time.deltaTime);
			} else {
			
				audioObject.GetComponent<AudioController> ().fadeOut (); 
			
				travelSpeed -= (1F * speed) * Time.deltaTime;
				transform.position = Vector3.MoveTowards (transform.position, planetPosition, (travelSpeed * speed) * Time.deltaTime);
				camTransform.localRotation = Quaternion.RotateTowards(camTransform.localRotation,noRotation.rotation, Time.deltaTime * 5);
				camTransform.localPosition = Vector3.MoveTowards(camTransform.localPosition,noRotation.position, Time.deltaTime);

				if (journeyLengthPlanet < 0.5F) {
					backgroundObject.GetComponent<BackgroundController> ().arrived (planetTexture, planetTextureEng, planetSphere);
					audioObject.GetComponent<AudioController> ().stopSound ();
					planetAt = planet;
					countDone = false;
					cameraDone = false;
					countDone = false;
					mercuryTravel = false;
					venusTravel = false;
					earthTravel = false;
					marsTravel = false;
					jupiterTravel = false;
					saturnTravel = false;
					uranusTravel = false;
					neptuneTravel = false;
					asteroidTravel = false;
					setTime = false;
					audioPlayed = false;
					buttonPress = false;
          
					if (outerSpaceTravel && astronautPlayed == false) {
						audioObject.GetComponent<AudioController> ().playSound (9);
						astronautPlayed = true;
					}
					outerSpaceTravel = false;
				}
			}
		}
	}


	public void DoShake(float speed) {
		OriginalPos = camTransform.localPosition;
		OriginalRot = camTransform.localRotation;
		
		if (0.015f * speed < 0.02f) {
			ShakeIntensity = 0.015f * speed;
		}	else {
			ShakeIntensity = 0.02f;
		}
		ShakeDecay = 0.1f;
		Shaking = true;
	}

	IEnumerator LoadScene(float waitTime) {
		yield return new WaitForSeconds (waitTime);
		Application.LoadLevel ("landingScene");
	}

}


