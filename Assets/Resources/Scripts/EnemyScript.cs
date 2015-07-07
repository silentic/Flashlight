using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour 
{
	Renderer renderer;
	Collider2D collider;
	// Use this for initialization
	void Start () 
	{
		renderer = GetComponent<SpriteRenderer>();
		collider = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(renderer.isVisible);
		{
			Vector2 playerPos =	(Vector2)Game.player.transform.position;
			Vector2 direction = (Vector2)transform.position - playerPos;
			RaycastHit2D hit = Physics2D.Raycast(playerPos,direction);
			if(hit.collider != collider)
			{
				renderer.enabled = false;
			}
			else
			{
				renderer.enabled = true;
			}
		}
	}
}
