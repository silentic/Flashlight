#define KEYBOARD

using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour 
{
	public static GameObject player;

	public static float UIScale;

	void Awake () 
	{
		Time.fixedDeltaTime = 0.02f;

		player = GameObject.FindGameObjectWithTag("Player");
		UIScale = GameObject.Find ("UI").GetComponent<Canvas>().scaleFactor;
	}

	void Update () 
	{
	
	}
}
