using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

	public AudioClip[] sounds;
	public AudioClip takeOff; //0
	public AudioClip countDown; //1
	public AudioClip landing; //2
	public AudioClip acceleration;
	public AudioClip standbyLoop;
	public AudioClip flightLoop;
	public AudioClip waterLoop;
	public AudioClip waterSplash;
	public AudioClip landed;
	public AudioClip astronaut;
	public AudioClip arrivedSpace; 

	public float audio1Volume = 1.0F;

	// Use this for initialization
	void Start () {
    
		sounds =  new AudioClip[] {
			(takeOff),
			(countDown), 
			(landing),
			(acceleration),
			(standbyLoop),
			(flightLoop),
			(waterLoop),
			(waterSplash),
			(landed),
			(astronaut),
			(arrivedSpace)
		};
    
	}

	public void playSound(int arrayNumber) {
		if (sounds [arrayNumber] == astronaut) {
			GetComponent<AudioSource> ().PlayOneShot (sounds [arrayNumber], 0.05F);
		} else {
			GetComponent<AudioSource> ().PlayOneShot (sounds [arrayNumber], 1.0F);
		}
	}

	public void stopSound() {
		audio1Volume = 1.0F;
		GetComponent<AudioSource> ().volume = 1.0F;
		GetComponent<AudioSource> ().Stop();
	}

	public void fadeOut() {
		audio1Volume -= 0.35F * Time.deltaTime;
		GetComponent<AudioSource> ().volume = audio1Volume;
	}

}
