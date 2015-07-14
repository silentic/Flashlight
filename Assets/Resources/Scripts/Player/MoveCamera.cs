using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

    private Transform player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 playerpos = player.position;
        playerpos.z = transform.position.z;
        transform.position = playerpos;
	}
}
