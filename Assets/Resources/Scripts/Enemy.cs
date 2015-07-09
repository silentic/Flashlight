using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public float speed;
	public float maxHp;
	protected float hp;

	SpriteRenderer enemyRenderer;
	Rigidbody2D enemyRigidbody;
	int visible;
	// Use this for initialization
	protected virtual void Start () 
	{
		enemyRenderer = GetComponent<SpriteRenderer>();
		visible = 0;

		Light2D.RegisterEventListener(LightEventListenerType.OnStay, OnLightStay);
		Light2D.RegisterEventListener(LightEventListenerType.OnEnter, OnLightEnter);
		Light2D.RegisterEventListener(LightEventListenerType.OnExit, OnLightExit);
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
		enemyRigidbody.MovePosition(transform.position + direction * speed * Time.deltaTime);

	}
	
	public virtual void turn (Vector3 direction)
	{
		transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
	}
}

