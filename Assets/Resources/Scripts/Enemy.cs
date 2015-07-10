using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour 
{
	public float speed;
	public float maxHp;
	protected float hp;

	protected SpriteRenderer enemyRenderer;
	Rigidbody2D enemyRigidbody;

	int visible;

	int wallLayer;
	int wallMask;
	int nodeLayer;
	int nodeMask;
	int layerMask;
	
	Vector3 targetPosition;
	GameObject lastVisitedNode;

	// Use this for initialization
	protected virtual void Start () 
	{
		hp = maxHp;

		enemyRenderer = GetComponent<SpriteRenderer>();
		enemyRigidbody = GetComponent<Rigidbody2D>();

		visible = 0;

		wallLayer = 9;
		wallMask = 1 << wallLayer;
		nodeLayer = 10;
		nodeMask = 1 << nodeLayer;
		
		layerMask = wallMask | nodeMask;

		Light2D.RegisterEventListener(LightEventListenerType.OnStay, OnLightStay);
		Light2D.RegisterEventListener(LightEventListenerType.OnEnter, OnLightEnter);
		Light2D.RegisterEventListener(LightEventListenerType.OnExit, OnLightExit);

		targetPosition = findNode().transform.position;
		//Debug.DrawLine(transform.position,targetPosition,Color.white,1000f);
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
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		//if(collider.get
	}

	#region light
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

	void moveToward(Vector3 position)
	{
		Vector3 direction = position - transform.position;
		move (direction);
	}

	public virtual void move (Vector2 direction)
	{
		enemyRigidbody.MovePosition((Vector2)transform.position + direction.normalized * speed * Time.deltaTime);
	}
	
	public virtual void turn (Vector2 direction)
	{
		transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
	}

	GameObject findNode()
	{
		List<GameObject> nodes = new List<GameObject>();

		//check and add to random pull
		GameObject temp;
		temp = checkNode(Vector2.up);
		if(temp != null) nodes.Add(temp);
		temp = checkNode(Vector2.down);
		if(temp != null) nodes.Add(temp);
		temp = checkNode(Vector2.left);
		if(temp != null) nodes.Add(temp);
		temp = checkNode(Vector2.right);
		if(temp != null) nodes.Add(temp);

		//return random node
		if(nodes.Count > 0)
		{
			int random = Random.Range(0,nodes.Count);
			Debug.Log(random);
			return nodes[random];
		}
		else return null;
	}

	GameObject checkNode(Vector2 direction)
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position , direction , Mathf.Infinity , layerMask);
		if(hit.collider == null) return null;
		if(hit.collider.tag == "Node")
		{
			Debug.Log("check : " + direction);
			Debug.Log("return : " + hit.collider.gameObject);
			return hit.collider.gameObject;
		}
		return null;
	}

}

