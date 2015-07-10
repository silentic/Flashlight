using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public float speed;
	public float maxHp;
	protected float hp;
	Vector3 direction;

	protected SpriteRenderer enemyRenderer;
	Rigidbody2D enemyRigidbody;
	int visible;
	bool moving;
	
	// Use this for initialization
	protected virtual void Start () 
	{
		hp = maxHp;

		enemyRenderer = GetComponent<SpriteRenderer>();
		enemyRigidbody = GetComponent<Rigidbody2D>();
		visible = 0;
		moving = false;

		Light2D.RegisterEventListener(LightEventListenerType.OnStay, OnLightStay);
		Light2D.RegisterEventListener(LightEventListenerType.OnEnter, OnLightEnter);
		Light2D.RegisterEventListener(LightEventListenerType.OnExit, OnLightExit);

		move (randomPath());
	}

	protected virtual void OnDestroy()
	{		
		Light2D.UnregisterEventListener(LightEventListenerType.OnStay, OnLightStay);
		Light2D.UnregisterEventListener(LightEventListenerType.OnEnter, OnLightEnter);
		Light2D.UnregisterEventListener(LightEventListenerType.OnExit, OnLightExit);
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{

	}

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

	public virtual void move (Vector3 direction)
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position , direction);
		if(hit != null)
		{
			Debug.Log("TEST");
			if(hit.distance < 2)
				moving = false;
			else
			{	
				moving = true;
				this.direction = direction;
				enemyRigidbody.MovePosition(transform.position + direction * speed * Time.deltaTime);
				//move (direction);
			}
		}
	}
	
	public virtual void turn (Vector3 direction)
	{
		transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
	}

	void FixedUpdate()
	{
		if(moving)
			move (direction);
		else
			move (randomPath());
	}

	Vector3 randomPath()
	{
		int r = Random.Range(0,4);
		Vector3 direction;
		switch(r)
		{
			case 0:
				return Vector2.up;
			case 1:
				return Vector2.down;
			case 2:
				return Vector2.left;			
			case 3:
				return Vector2.right;
		}
		return Vector3.zero;
	}
}

