﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour 
{
	public float speed;
	public float turningSpeed;
	public float maxHp;
	protected float hp;

	protected SpriteRenderer enemyRenderer;

	Rigidbody2D enemyRigidbody;

	EnemyNodeDetector nodeDetector;

	int visible;

	[HideInInspector]
	public bool chasingPlayer = false;

	[HideInInspector]
	public Vector3 targetPosition;

	// Use this for initialization
	protected virtual void Start () 
	{
		hp = maxHp;
		enemyRenderer = GetComponentInChildren<SpriteRenderer>();
		enemyRenderer.enabled = false;
		enemyRigidbody = GetComponent<Rigidbody2D>();
		nodeDetector = GetComponentInChildren<EnemyNodeDetector>();


		visible = 0;

		Light2D.RegisterEventListener(LightEventListenerType.OnStay, OnLightStay);
		Light2D.RegisterEventListener(LightEventListenerType.OnEnter, OnLightEnter);
		Light2D.RegisterEventListener(LightEventListenerType.OnExit, OnLightExit);

		gotoNearbyNode();
	}

	protected virtual void OnDestroy()
	{		
		Light2D.UnregisterEventListener(LightEventListenerType.OnStay, OnLightStay);
		Light2D.UnregisterEventListener(LightEventListenerType.OnEnter, OnLightEnter);
		Light2D.UnregisterEventListener(LightEventListenerType.OnExit, OnLightExit);
	}

	protected virtual void Update () 
	{

	}
	
	void FixedUpdate()
	{
		moveToward(targetPosition);
		turn(targetPosition - transform.position);
	}
	
	#region LIGHT
	protected void OnLightEnter(Light2D light, GameObject go)
	{
		if (go.GetInstanceID() == gameObject.GetInstanceID())
		{
			if(visible <= 0)
			{
				//Debug.Log("Object Entered Light");
				enterLightEffect();

			}
			visible += 1;
		}
	}

	protected virtual void enterLightEffect()
	{
		enemyRenderer.enabled = true;
	}

	protected void OnLightExit(Light2D light, GameObject go)
	{
		if (go.GetInstanceID() == gameObject.GetInstanceID())
		{
			visible -= 1;
			if(visible <= 0)
			{
				//Debug.Log("Object Exited Light");
				exitLightEffect();
			}

		}
	}

	protected virtual void exitLightEffect()
	{
		enemyRenderer.enabled = false;
	}


	protected void OnLightStay(Light2D light, GameObject go)
	{
		if (go.GetInstanceID() == gameObject.GetInstanceID())
		{
			//seen by flashLight
			if(light.tag == "Flashlight")
			{
				stayFlashLightEffect();
			}
		}
	}

	protected virtual void stayFlashLightEffect()
	{
	}

	#endregion

	#region MOVE
	void moveToward(Vector3 position)
	{
		Vector3 direction = position - transform.position;
		move (direction);
	}

	public virtual void move (Vector2 direction)
	{
		enemyRigidbody.MovePosition((Vector2)transform.position + direction.normalized * speed * Time.deltaTime);
	}	

	public void gotoNearbyNode()
	{
		GameObject node = nodeDetector.findNode();
		if(node != null)
			targetPosition = nodeDetector.findNode().transform.position;
	}
	#endregion

	public virtual void turn (Vector2 direction)
	{
		//transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
		var newRotation = Quaternion.LookRotation(Vector3.forward,  direction);
		newRotation.x = 0.0f;
		newRotation.y = 0.0f;
		transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * turningSpeed);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.collider.tag == "Player")
		{
			Debug.Log("GameOver");
			Game.manager.gameOver();
			return;
		}

	}

	void OnCollisionStay2D(Collision2D collision)
	{
		Enemy enemy = collision.collider.GetComponent<Enemy>();
		if(!chasingPlayer)
		{
			if(enemy != null)
			{
				gotoNearbyNode();
				return;
			}
		}

	}



}

