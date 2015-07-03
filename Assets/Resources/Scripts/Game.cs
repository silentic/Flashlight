#define KEYBOARD

using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour 
{
	public static GameObject player;

	void Awake () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update () 
	{
	
	}
}
