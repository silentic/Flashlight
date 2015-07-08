using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour 
{
	SpriteRenderer renderer;
	Collider2D collider;
	Transform player;
	int visible;
	// Use this for initialization
	void Start () 
	{
		renderer = GetComponent<SpriteRenderer>();
		collider = GetComponent<Collider2D>();
		player = Game.player.transform;
		visible = 0;

		Light2D.RegisterEventListener(LightEventListenerType.OnEnter, OnLightEnter);
		Light2D.RegisterEventListener(LightEventListenerType.OnExit, OnLightExit);
	}
	
	// Update is called once per frame
#if test
	void Update () 
	{
		//out of camera
		if(!renderer.IsVisibleFrom(Camera.main))
		{
			renderer.enabled = false;
			return;
		}

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
#endif
	void OnLightEnter(Light2D _light, GameObject _go)
	{
		if (_go.GetInstanceID() == gameObject.GetInstanceID())
		{
			if(visible <= 0)
			{
				Debug.Log("Object Entered Light");

				renderer.enabled = true;
			}
			visible += 1;
		}
	}

	void OnLightExit(Light2D _light, GameObject _go)
	{
		if (_go.GetInstanceID() == gameObject.GetInstanceID())
		{
			visible -= 1;
			if(visible <= 0)
			{
				Debug.Log("Object Exited Light");

				renderer.enabled = false;
			}

		}
	}
}

