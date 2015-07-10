using UnityEngine;
using System.Collections;

public class RandomLightOff : MonoBehaviour {

	private int num;
	public int prob;

	GameObject player;
	Renderer playerRenderer;
	GameObject parentObject;
	Renderer lightRenderer;

	Light2D playerVision;
	Light light3D;

	public static bool blackOut;

	// Use this for initialization
	void Start () {
		
		player = Game.player;
		playerRenderer = player.GetComponent<Renderer>();
		InvokeRepeating("LightActive", 5, 1);
		lightRenderer = GetComponent<Renderer>();
		GameObject visionObject = GameObject.FindGameObjectWithTag("Vision");
		playerVision = visionObject.GetComponent<Light2D>();;
		light3D = visionObject.GetComponent<Light>();
		blackOut = false;
	}
	
	void LightActive(){
		num = Random.Range(0,100);
		//Debug.Log ("TestDebug " + num);
		
		if(num < prob){		// random light off with XX% probability 
			StartCoroutine(SleepSecs ());
		}
	}
	
	void LightOff(){
		//toggle player
		playerRenderer.enabled = !playerRenderer.enabled;

		//flashlight - vision
		blackOut = !blackOut;
		playerVision.LightRadius = blackOut ? 0f : 2f;

		//3d light on wall
		light3D.enabled = !light3D.enabled;
	}
	
	IEnumerator SleepSecs () { 	
		CancelInvoke ();
		LightOff ();
		yield return new WaitForSeconds(0.3f);
		LightOff ();
		yield return new WaitForSeconds(0.1f);
		LightOff ();
		yield return new WaitForSeconds(2f);
		LightOff ();
		InvokeRepeating("LightActive", 5, 1);
	}
}