using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour 
{
	public GameObject enemyPrefabs;
	public float minimumSpawnRange = 10f;
	public int spawnNumber;

	public MazeGenerator maze;

	// Use this for initialization
	void Awake () 
	{
		//maze = transform.parent.GetComponentInChildren<MazeGenerator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void spawn(int numberToSpawn)
	{
		int i=0;
		while(i < numberToSpawn)
		{
			int randomBlockX = Random.Range(0 , maze.width);
			int randomBlockY = Random.Range(0 , maze.height);
			
			Vector3 spawnPosition = maze.getPosition(randomBlockX , randomBlockY);
			
			float playerDistance = ((Vector2)(spawnPosition - Game.player.transform.position)).magnitude;
			if(playerDistance > minimumSpawnRange)
			{
				GameObject enemy = (GameObject)Instantiate(enemyPrefabs , spawnPosition , Quaternion.identity);
				enemy.transform.SetParent(GameObject.Find("Enemies").transform);
				i++;
			}
		}

	}
}
