using UnityEngine;
using System.Collections;

public class Minimap : MonoBehaviour {

    bool visible;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("m"))
        {
            visible = !visible;
            gameObject.GetComponent<Camera>().enabled = visible;
        }
            
	}
}
