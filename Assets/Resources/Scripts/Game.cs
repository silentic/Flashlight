﻿using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour 
{
	public static GameObject player;

	public static float UIScale;
	
	[HideInInspector]
	public static int wallMask;
	[HideInInspector]
	public static int nodeMask;

	//Assign from Hierarchy
	public GameObject pauseScreen;

	void Awake () 
	{
		Time.fixedDeltaTime = 0.02f;

		player = GameObject.FindGameObjectWithTag("Player");
		UIScale = GameObject.Find ("UI").GetComponent<Canvas>().scaleFactor;

		wallMask = LayerMask.GetMask("Minimap");
		nodeMask = LayerMask.GetMask("Node");
	}

	void Update () 
	{
		
	}

	public void pause()
	{
		Time.timeScale = 0f;

		pauseScreen.SetActive(true);
	}

	public void resume()
	{
		Time.timeScale = 1f;

		pauseScreen.SetActive(false);
	}
}
