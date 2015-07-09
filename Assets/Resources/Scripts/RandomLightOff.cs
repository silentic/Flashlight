using UnityEngine;
using System.Collections;

public class RandomLightOff : MonoBehaviour {

	private int num;
	public GameObject parentObject;
	public int prob;


	// Use this for initialization
	void Start () {
		
		parentObject=GameObject.Find("Player");
		InvokeRepeating("LightActive", 5, 1);
		
	}
	
	void LightActive(){
		num = Random.Range(0,100);
		Debug.Log ("TestDebug " + num);
		
		if(num < prob){		// random light off with XX% probability 
			StartCoroutine(SleepSecs ());
		}
	}
	
	void LightOff(){
		GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
		
		foreach (Transform t in parentObject.transform) {
			if (t.name == "2D-Radial"){
				//t.gameObject.active = !t.gameObject.active;
				t.gameObject.SetActive(!t.gameObject.activeSelf);
			}
			if (t.name == "2D-Cone"){
				//t.gameObject.active = !t.gameObject.active;
				t.gameObject.SetActive(!t.gameObject.activeSelf);
			}
		}
	}
	
	IEnumerator SleepSecs () { 	
		CancelInvoke ();
		LightOff ();
		yield return new WaitForSeconds(0.3f);
		LightOff ();
		yield return new WaitForSeconds(0.1f);
		LightOff ();
		yield return new WaitForSeconds(5f);
		LightOff ();
		InvokeRepeating("LightActive", 5, 1);
	}
}