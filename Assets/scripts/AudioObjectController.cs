using UnityEngine;
using System.Collections;

public class AudioObjectController : MonoBehaviour {

	void Awake() {
		DontDestroyOnLoad(this.gameObject);
	}
}
