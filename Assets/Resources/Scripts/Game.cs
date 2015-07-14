using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour 
{
	public static Game manager;

	public static GameObject player;

	public static float UIScale;
	
	[HideInInspector]
	public static int wallMask = LayerMask.GetMask("Wall");
	[HideInInspector]
	public static int nodeMask = LayerMask.GetMask("Node");

	//Assign from Hierarchy
	public GameObject pauseScreen;
	public GameObject gameoverScreen;

	void Awake () 
	{
		Time.fixedDeltaTime = 0.01f;

		manager = GameObject.Find ("GameManager").GetComponent<Game>();
		player = GameObject.FindGameObjectWithTag("Player");
		UIScale = GameObject.Find ("UI").GetComponent<Canvas>().scaleFactor;

		startGame();
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

	public void gameOver()
	{
		Time.timeScale = 0f;
		gameoverScreen.SetActive(true);
	}

	public void restart()
	{
		Application.LoadLevel("test");
		Time.timeScale = 1f;
		gameoverScreen.SetActive(false);
		startGame();
	}

	public void startGame()
	{
		Debug.Log("start new game");

		GetComponentInChildren<MazeGenerator>().createMaze();
		EnemySpawner spawner = GetComponentInChildren<EnemySpawner>();
		spawner.spawn(spawner.spawnNumber);

	}
}
