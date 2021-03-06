﻿using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {
	
	public Texture takeOff;
	public Texture landing;
	public Texture parachute;
	public Texture landed;
	public Texture startTexture;
	public Texture outerSpaceTexture;
	public Texture ISS;

	public Texture takeOffEng;
	public Texture landingEng;
	public Texture parachuteEng;
	public Texture landedEng;
	public Texture startTextureEng;
	public Texture outerSpaceTextureEng;

	public Texture ten;
	public Texture nine;
	public Texture eight;
	public Texture seven;
	public Texture six;
	public Texture five;
	public Texture four;
	public Texture three;
	public Texture two;
	public Texture one;

	public Renderer rend;
	
	public bool blinker;
	public bool fadeInCheck;
	public bool fadeOutCheck;
	public bool takeOffCheck;
	public bool coroutinesDone;
	public bool lockLanguage;

	public int language;
	
	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
		blinker = false;
		fadeInCheck = false;
		fadeOutCheck = false;
		takeOffCheck = false;
		coroutinesDone = false;		
		language = 0;
		lockLanguage = false;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (takeOffCheck) {
			blinker = true;
			if (language == 0) {
				rend.material.mainTexture = takeOff;
			} else {
				rend.material.mainTexture = takeOffEng;
			}
		}
		
		if (blinker) {
			fadeOutCheck = true;
		} else {
			fadeOutCheck = false;
			fadeInCheck = false;
		}
		
		if (fadeOutCheck) {
			StartCoroutine(blinkFadeOut());
		}
		
		if (fadeInCheck) {				
			StartCoroutine(blinkFadeIn());
		}

		if (Input.GetKeyDown ("x")) {
			lockLanguage = true;
		}

		if (Input.GetKeyDown ("c") && !lockLanguage) {
			language = 1;
			switchLanguage(language);
		}

		if (Input.GetKeyDown ("v") && !lockLanguage) {
			language = 0;
			switchLanguage(language);
		}

	}

	public void switchLanguage(int language) {
		if (language == 0) {
			rend.material.mainTexture = startTexture;
		} else {
			rend.material.mainTexture = startTextureEng;
		}
	}

	public void travel(Texture destination, Texture destinationEng, bool blink) {
		if (coroutinesDone == false) {
			StartCoroutine (fadeOut ());
			if (language == 0) {
				StartCoroutine (fadeInTravel (destination));
			} else {
				StartCoroutine (fadeInTravel (destinationEng));
			}
			coroutinesDone = true;
		} else {
			blinker = blink;
		}
	}

	public void arrived(Texture planet, Texture planetEng, Texture planetSphere) {
		blinker = false;
		coroutinesDone = false;
		StartCoroutine (fadeOut());
		if (language == 0) {
			StartCoroutine (fadeIn (planet, planetSphere));
		} else {
			StartCoroutine (fadeIn (planetEng, planetSphere));
		}
	}

	public void start() {
		StartCoroutine (fadeCountDown ());
	}

	public void outerSpace() {
		blinker = false;
		takeOffCheck = false;
		StartCoroutine(fadeOut());
		if (language == 0) {
			StartCoroutine (fadeInTravel (outerSpaceTexture));
		} else {
			StartCoroutine (fadeInTravel (outerSpaceTextureEng));
		}
	}

	public void startLanding() {
		takeOffCheck = true;
		GameObject.Find ("planet").GetComponent<PlanetController> ().hide ();
	}

	public void startParachute() {
		blinker = false;
		takeOffCheck = false;
		StartCoroutine(fadeOut());
		if (language == 0) {
			StartCoroutine (fadeInTravel (parachute));
		} else {
			StartCoroutine (fadeInTravel (parachuteEng));
		}
	}

	public void hasLanded() {
		StartCoroutine(fadeOut());
		if (language == 0) {
			StartCoroutine(fadeInTravel(landed));
		} else {
			StartCoroutine(fadeInTravel(landedEng));
		}
	}
	
	IEnumerator fadeOut() {
		GameObject.Find("background").GetComponent<FadingController>().BeginFade(1);
		yield return new WaitForSeconds (2F);
	}
	
	IEnumerator fadeIn(Texture switchTextureInfo, Texture switchTextureSphere) {
		yield return new WaitForSeconds (0.25F);
		GameObject.Find ("background").GetComponent<FadingController>().BeginFade(-1);
		GameObject.Find ("planet").GetComponent<PlanetController>().switchTexture(switchTextureSphere);
		if (switchTextureSphere != ISS) {
			GameObject.Find ("planet").GetComponent<PlanetController> ().show ();
		}
		rend.material.mainTexture = switchTextureInfo;
		yield return new WaitForSeconds (0.5F);
	}
	
	IEnumerator fadeInTravel(Texture switchTextureTravel) {
		yield return new WaitForSeconds (0.75F);
		GameObject.Find ("background").GetComponent<FadingController> ().BeginFade (-1);
		GameObject.Find ("planet").GetComponent<PlanetController> ().hide ();
		rend.material.mainTexture = switchTextureTravel;
		yield return new WaitForSeconds (2F);
	}
	
	IEnumerator fadeCountDown() {
		yield return new WaitForSeconds (2F);
		rend.material.mainTexture = ten;
		yield return new WaitForSeconds (1.3F);
		rend.material.mainTexture = nine;
		yield return new WaitForSeconds (1.3F);
		rend.material.mainTexture = eight;
		yield return new WaitForSeconds (1.3F);
		rend.material.mainTexture = seven;
		yield return new WaitForSeconds (1.3F);
		rend.material.mainTexture = six;
		yield return new WaitForSeconds (1.3F);
		rend.material.mainTexture = five;
		yield return new WaitForSeconds (1.3F);
		rend.material.mainTexture = four;
		yield return new WaitForSeconds (1.3F);
		rend.material.mainTexture = three;
		yield return new WaitForSeconds (1.3F);
		rend.material.mainTexture = two;
		yield return new WaitForSeconds (1.3F);
		rend.material.mainTexture = one;
		yield return new WaitForSeconds (2F);
		takeOffCheck = true;
	}
	
	IEnumerator blinkFadeOut() {
		yield return new WaitForSeconds (0.5F);
		GameObject.Find("background").GetComponent<FadingController>().BeginFade(1);
		fadeOutCheck = false;
		fadeInCheck = true;
	}
	
	IEnumerator blinkFadeIn() {
		yield return new WaitForSeconds (0.5F);
		GameObject.Find("background").GetComponent<FadingController>().BeginFade(-1);
		fadeInCheck = false;
		fadeOutCheck = true;
	}
}
