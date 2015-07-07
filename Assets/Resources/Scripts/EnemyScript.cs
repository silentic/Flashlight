using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour 
{
	SpriteRenderer renderer;
	Collider2D collider;
	Transform player;
	// Use this for initialization
	void Start () 
	{
		renderer = GetComponent<SpriteRenderer>();
		collider = GetComponent<Collider2D>();
		player = Game.player.transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(renderer.IsVisibleFrom(Camera.main))
		{
			Vector2 playerPos =	(Vector2)player.position;
			Vector2 direction = (Vector2)transform.position - playerPos;
			float range = direction.magnitude;

			//check object block
			RaycastHit2D hit = Physics2D.Raycast(playerPos,direction);
			if(hit.collider != collider)
			{
				renderer.enabled = false;
				return;
			}
	
			//check ouf of maximum sight range
			if(range > 6)
			{
				renderer.enabled = false;
				return;
			}

			Vector2 playerLookRotation = (Vector2)(player.rotation * Vector3.up);
			float angle = Vector2.Angle(playerLookRotation,direction);

			//check facing
			if(angle > 25 && range > 2.6f)
			{
				renderer.enabled = false;
				return;
			}
			renderer.enabled = true;
			return;
		}
		renderer.enabled = false;
	}
}

