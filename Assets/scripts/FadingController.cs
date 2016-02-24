using UnityEngine;
using System.Collections;

public class FadingController : MonoBehaviour {
	
	public Texture2D fadeOutTexture;
	public float fadeSpeed = 10f;
	
	private int drawDepth = -1000;
	private float alpha = 1.0f; 
	private int fadeDir = -1;
	
	void OnGUI () {
		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		alpha = Mathf.Clamp01 (alpha);
		
		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture (new Rect (1920, 0, 1920, 1080), fadeOutTexture);
	}
	public float BeginFade (int direction) {
		fadeDir = direction;
		return (fadeSpeed);	
	}
}