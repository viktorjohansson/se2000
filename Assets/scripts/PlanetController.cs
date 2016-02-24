using UnityEngine;
using System.Collections;

public class PlanetController : MonoBehaviour {
	
	public Renderer rend;
	
	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
		rend.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void hide () {
		rend.enabled = false;
	}
	
	public void show () {
		rend.enabled = true;
	}
	
	public void switchTexture (Texture planetTexture) {
		rend.material.mainTexture = planetTexture;
	}
}
